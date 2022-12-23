using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SmartSolucionesCuba.SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("works")]
    public class ServiceWorksController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ServiceWork, System.Guid, ServiceWorkInputViewModel, ServiceWorkDisplayViewModel>
    {
        private readonly IEntityRepository<ServiceLine, System.Guid> serviceLineRepository;
        private readonly IEntityRepository<Currency, int> currencyRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public ServiceWorksController(IEntityRepository<ServiceLine, System.Guid> serviceLineRepository, IEntityRepository<Currency, int> currencyRepository, IEntityRepository<Image, System.Guid> imageRepository, IImageFileManagerFacade imageFileManager, IEntityRepository<ServiceWork, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ServiceWorksController> localizer, ILogger<ServiceWorksController> logger) : base(repository, localizer, logger)
        {
            this.serviceLineRepository = serviceLineRepository;
            this.currencyRepository = currencyRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequiresRoleBasePermission(Roles.ServiceWorkViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.CraftWorkCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ServiceWorkCreator)]
        public override IActionResult Create(ServiceWorkInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ServiceWorkEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ServiceWorkEditor)]
        public override IActionResult Edit(ServiceWorkInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ServiceWorkRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ServiceWorkRemover)]
        public override IActionResult Delete(ServiceWorkDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(ServiceWorkInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigureEdit(ServiceWork entity, ServiceWorkInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ServiceWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ServiceWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void PreCreate(ServiceWork entity, ServiceWorkInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Line = serviceLineRepository.Get(modelInput.LineId.Value);
        }

        protected override void PreEdit(ServiceWork entity, ServiceWorkInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.LineId == modelInput.LineId) return;

            entity.Line = serviceLineRepository.Get(modelInput.LineId.Value);
        }

        private void PopulateInputModelForAvailableManifestations(ServiceWorkInputViewModel model)
        {
            model.AvailableLines = serviceLineRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateInputModelForAvailableCurrencies(ServiceWorkInputViewModel model)
        {
            model.AvailableCurrencies = currencyRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        #region Images

        [HttpGet]
        [Route("edit/{key}/images")]
        [RequiresRoleBasePermission(Roles.ServiceWorkEditor)]
        public ActionResult EditImages(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ServiceWorkImagesDisplayViewModel
            {
                Work = new ServiceWorkDisplayViewModel(),
                FeatureImage = entity.FeatureImageId == null ? null : new ImageDisplayViewModel(),
                Images = entity.ImageBridges.Select(bridge =>
                {
                    var result = new ImageDisplayViewModel();

                    result.Import(bridge.Image);

                    return result;
                }).ToArray()
            };

            model.Work.Import(entity);

            model.FeatureImage?.Import(entity.FeatureImage);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/images/add")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(Roles.ServiceWorkEditor)]
        public IActionResult AddImage(System.Guid key, Microsoft.AspNetCore.Http.IFormFile imageFile)
        {
            try
            {
                imageFile = Request.Form.Files[0];

                var entity = Repository.Get(key);

                // TODO Validate imageFile

                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            imageFile.CopyTo(memoryStream);

                            var image = new Image { ContentType = imageFile.ContentType, Length = imageFile.Length, About = "For CraftWork " + entity.Id };

                            imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), imageFile.ContentType);

                            entity.ImageBridges.Add(new ServiceWorkImageBridge
                            {
                                WorkId = entity.Id,
                                Image = image
                            });
                        }

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editimages", new { key = entity.Id });
                    }
                    catch (SmartSolucionesCuba.SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editimages", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/images/delete")]
        [ValidateAntiForgeryToken]
        [RequiresRoleBasePermission(Roles.ServiceWorkEditor)]
        public IActionResult DeleteImage(System.Guid key, System.Guid imageId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.ImageBridges.First(b => b.ImageId == imageId);

                entity.ImageBridges.Remove(bridge);

                if (entity.FeatureImageId == imageId)
                {
                    entity.FeatureImage = null;
                    entity.FeatureImageId = null;
                }

                Repository.Update(entity, User.Identity.Name);

                imageRepository.Delete(bridge.Image, User.Identity.Name);

                imageFileManager.DeleteImageFile(bridge.Image);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editimages", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editimages", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion
    }
}
