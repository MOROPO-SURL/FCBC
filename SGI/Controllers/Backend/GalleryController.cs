using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Route("management/[area]/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[area]/[controller]")]
    [Area("institution")]
    public class GalleryController : SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Persistence.Entities.Gallery, Persistence.Enumerations.GalleryId> galleryRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public GalleryController(IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Persistence.Entities.Gallery, Persistence.Enumerations.GalleryId> galleryRepository, Microsoft.Extensions.Localization.IStringLocalizer<GalleryController> localizer, ILogger<GalleryController> logger) : base(localizer, logger)
        {
            this.galleryRepository = galleryRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryViewer)]
        [HttpGet]
        public virtual IActionResult Index()
        {
            var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

            var modelInput = new Models.View.GalleryDisplayViewModel();

            modelInput.Import(entity);

            return View(modelInput);
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpGet]
        [Route("[action]")]
        public virtual IActionResult Edit()
        {
            var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

            var modelInput = new Models.View.GalleryInputViewModel();

            modelInput.Import(entity);

            return View(modelInput);
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(Models.View.GalleryInputViewModel model)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    model.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(model);

                    updateModelTask.Wait();
                }
            }

            try
            {
                var entity = galleryRepository.Get(model.Id);

                if (ModelState.IsValid)
                {
                    try
                    {
                        model.Mergue(entity);

                        try
                        {
                            PreEdit(entity, model);
                        }
                        catch (SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                        {
                            ModelState.AddModelError(e.ModelKey, e.ErrorMessage);

                            throw new System.InvalidOperationException(e.ErrorMessage, e);
                        }

                        galleryRepository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, null, "EditionSuccessMessage", null);

                        return RedirectToAction("edit");
                    }

                    catch (System.Exception e)
                    {
                        if (!(e.InnerException is SAPRESSC.Core.Web.Frontend.InvalidModelException))
                        {
                            logger.LogError(e.InnerException, e.Message);

                            SetDefaultNotification(NotificationType.Danger, null, "NotFoundMessage", null);

                            return RedirectToAction("index");
                        }
                    }
                }

                SetDefaultNotification(NotificationType.Danger, null, "InvalidMessage", null);

                return View(model);
            }
            catch (SAPRESSC.Core.Web.Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);

                return RedirectToAction("index");
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        protected void PreEdit(Persistence.Entities.Gallery entity, Models.View.GalleryInputViewModel modelInput)
        {
            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.Identity.Image != null)
                {
                    imageFileManager.DeleteImageFile(entity.Identity.Image);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.Identity.Image != null)
            {
                imageFileManager.DeleteImageFile(entity.Identity.Image);

                imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Collaborator Organization " + entity.Id };

                imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }
    }
}
