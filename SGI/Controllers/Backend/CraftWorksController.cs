using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("works")]
    public class CraftWorksController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<CraftWork, System.Guid, CraftWorkInputViewModel, CraftWorkDisplayViewModel>
    {
        private readonly IEntityRepository<CraftManifestation, System.Guid> manifestationRepository;
        private readonly IEntityRepository<Currency, int> currencyRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;
        private readonly IEntityRepository<Award, Guid> awardRepository;
        private readonly IEntityRepository<Artist, Guid> creatorRepository;
        private readonly IEntityRepository<CreationGroup, Guid> creationGroupRepository;
        private readonly IEntityRepository<CraftTechnique, Guid> techniqueRepository;
        private readonly IEntityRepository<Material, Guid> materialRepository;        
        private readonly Managers.RelativeAttachmentFileManager attachmentFileManager;
        private readonly IEntityRepository<CraftWorkAttachment, Guid> attachmentRepository;

        public CraftWorksController(IEntityRepository<CraftWorkAttachment, Guid> attachmentRepository, Managers.RelativeAttachmentFileManager attachmentFileManager, IEntityRepository<CraftManifestation, System.Guid> manifestationRepository, IEntityRepository<Currency, int> currencyRepository, IEntityRepository<Image, System.Guid> imageRepository, IImageFileManagerFacade imageFileManager, IEntityRepository<Award, Guid> awardRepository, IEntityRepository<Artist, Guid> creatorRepository, IEntityRepository<CreationGroup, Guid> creationGroupRepository, IEntityRepository<CraftTechnique, Guid> techniqueRepository, IEntityRepository<Material, Guid> materialRepository,
            IEntityRepository<CraftWork, System.Guid> repository, IStringLocalizer<CraftWorksController> localizer, ILogger<CraftWorksController> logger) : base(repository, localizer, logger)
        {
            this.manifestationRepository = manifestationRepository;
            this.currencyRepository = currencyRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
            this.awardRepository = awardRepository;
            this.creatorRepository = creatorRepository;
            this.creationGroupRepository = creationGroupRepository;
            this.techniqueRepository = techniqueRepository;
            this.materialRepository = materialRepository;
            this.attachmentFileManager = attachmentFileManager;
            this.attachmentRepository = attachmentRepository;
        }

        [RequirePermissions(Permission.CraftWorkViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.CraftWorkCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.CraftWorkCreator)]
        public override IActionResult Create(CraftWorkInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.CraftWorkEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.CraftWorkEditor)]
        public override IActionResult Edit(CraftWorkInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.CraftWorkRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.CraftWorkRemover)]
        public override IActionResult Delete(CraftWorkDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(CraftWorkInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);            
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigureEdit(CraftWork entity, CraftWorkInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(CraftWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(CraftWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void PreCreate(CraftWork entity, CraftWorkInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Manifestation = manifestationRepository.Get(modelInput.ManifestationId.Value);
        }

        protected override void PreEdit(CraftWork entity, CraftWorkInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.ManifestationId == modelInput.ManifestationId) return;

            entity.Manifestation = manifestationRepository.Get(modelInput.ManifestationId.Value);
        }

        private void PopulateInputModelForAvailableManifestations(CraftWorkInputViewModel model)
        {
            model.AvailableManifestations = manifestationRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateInputModelForAvailableCurrencies(CraftWorkInputViewModel model)
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
        [RequirePermissions(Permission.CraftWorkEditor)]
        public ActionResult EditImages(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CraftWorkImagesDisplayViewModel
            {
                Work = new CraftWorkDisplayViewModel(),
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
        [RequirePermissions(Permission.CraftWorkEditor)]
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

                            entity.ImageBridges.Add(new CraftWorkImageBridge
                            {
                                WorkId = entity.Id,
                                Image = image
                            });
                        }

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editimages", new { key = entity.Id });
                    }
                    catch (MOROPO.SAPRESSC.Core.Web.Frontend.InvalidModelException e)
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
        [RequirePermissions(Permission.CraftWorkEditor)]
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

        #region Awards
        [Route("edit/{key}/CraftWorkAwards")]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public IActionResult CraftWorkAwards(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new CraftWorkAwardManageViewModel
                {
                    CraftWorkDisplay = new CraftWorkDisplayViewModel()
                };
                PopulateAwards(resultModel);
                resultModel.CraftWorkDisplay.Import(entity);

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }


        [Route("edit/Awards/add")]
        [RequirePermissions(Permission.CraftWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCraftWorkAwards(CraftWorkAwardManageViewModel modelInput)
        {
            CraftWork entity = null;
            Award award = null;
            try
            {
                entity = Repository.Get(modelInput.CraftWorkDisplay.Id);
                award = awardRepository.Get(modelInput.AwardId);

                modelInput.CraftWorkDisplay.Import(entity);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Awards.Add(new CraftWorkAward { Work = entity, Award = award, WorkId = entity.Id, AwardId = award.Id, Context = modelInput.Context, Year = modelInput.Year });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
            }
            else
            {
                var messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        messages.Add(error.ErrorMessage);
                    }
                }

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("CraftWorkAwards", new { key = entity.Id });
        }

        [Route("edit/Awards/delete")]
        [RequirePermissions(Permission.CraftWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCraftWorkAwards(CraftWorkAwardDisplayViewModel modelDisplay)
        {
            CraftWork entity = null;
            try
            {
                entity = Repository.Get(modelDisplay.CraftWorkId);

            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                var craftWorkAward = entity.Awards.First(a => a.Id == modelDisplay.Id);
                entity.Awards.Remove(craftWorkAward);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
            }
            else
            {
                var messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        messages.Add(error.ErrorMessage);
                    }
                }

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("CraftWorkAwards", new { key = entity.Id });
        }

        private void PopulateAwards(CraftWorkAwardManageViewModel modelInput)
        {
            var awads = awardRepository.GetAll();

            if (awads.Count() > 0)
            {
                modelInput.SelectAwards = awardRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name + " - " + item.Country.Name }).ToArray();
            }
        }

        #endregion

        #region Authors

        [HttpGet]
        [Route("edit/{key}/authors")]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public ActionResult EditAuthors(System.Guid key)
        {
            var entity = Repository.Get(key);

            var creators = entity.CreatorBridges.Select(bridge => bridge.Creator).ToArray();
            var creationGroups = entity.CreationGroupBridges.Select(bridge => bridge.CreationGroup).ToArray();

            var model = new CraftWorkAuthorsDisplayViewModel
            {
                AvailableCreators = creatorRepository.GetAll().ToArray().Except(creators).ToArray().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = string.IsNullOrEmpty(item.Identity.LastName) ? string.Format("{0} {1}", item.Identity.Name, item.Identity.LastName) : item.Identity.Name
                }).ToArray(),
                AvailableCreationGroups = creationGroupRepository.GetAll().ToArray().Except(creationGroups).ToArray().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Identity.Name
                }).ToArray(),
                Work = new CraftWorkDisplayViewModel(),
                Creators = creators.Select(item =>
                {
                    var result = new ArtistDisplayViewModel();

                    result.Import(item);

                    return result;
                }).ToArray(),
                CreationGroups = creationGroups.Select(item =>
                {
                    var result = new CreationGroupDisplayViewModel();

                    result.Import(item);

                    return result;
                }).ToArray()
            };

            model.Work.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/authors/addcreator")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public IActionResult AddCreator(System.Guid key, System.Guid creatorId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        entity.CreatorBridges.Add(new CraftWorkCreatorBridge
                        {
                            WorkId = entity.Id,
                            CreatorId = creatorId
                        });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editauthors", new { key = entity.Id });
                    }
                    catch (MOROPO.SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editauthors", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/authors/addcreationgroup")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public IActionResult AddCreationGroup(System.Guid key, System.Guid creationGroupId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        entity.CreationGroupBridges.Add(new CraftWorkCreationGroupBridge
                        {
                            WorkId = entity.Id,
                            CreationGroupId = creationGroupId
                        });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editauthors", new { key = entity.Id });
                    }
                    catch (MOROPO.SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editauthors", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/authors/deletecreator")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public IActionResult DeleteCreator(System.Guid key, System.Guid creatorId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.CreatorBridges.First(b => b.CreatorId == creatorId);

                entity.CreatorBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editauthors", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editauthors", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/authors/deletecreationgroup")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public IActionResult DeleteCreationGroup(System.Guid key, System.Guid creationGroupId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.CreationGroupBridges.First(b => b.CreationGroupId == creationGroupId);

                entity.CreationGroupBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editauthors", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editauthors", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region Elaboration

        [HttpGet]
        [Route("edit/{key}/elaboration")]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public ActionResult EditElaboration(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CraftWorkElaborationInputViewModel
            {
                TechniqueSelectors = techniqueRepository.GetAll().Select(item => new SelectionDescriptedItemInputViewModel<System.Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.TechniqueBridges.Any(bridge => bridge.TechniqueId == item.Id),
                    Description = item.Description
                }).ToArray(),
                MaterialSelectors = materialRepository.GetAll().Select(item => new SelectionDescriptedItemInputViewModel<System.Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.MaterialBridges.Any(bridge => bridge.MaterialId == item.Id),
                    Description = item.Description
                }).ToArray(),
                Work = new CraftWorkDisplayViewModel()
            };

            model.Work.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/elaboration")]
        [RequirePermissions(Permission.CraftWorkEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditElaboration(System.Guid key, CraftWorkElaborationInputViewModel modelInput)
        {
            CraftWork entity = null;

            try
            {
                entity = Repository.Get(key);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            entity.TechniqueBridges.Clear();

            foreach (var item in modelInput.TechniqueSelectors)
            {
                if (!item.Selected) continue;

                entity.TechniqueBridges.Add(new CraftWorkTechniqueBridge
                {
                    WorkId = entity.Id,
                    TechniqueId = item.Id
                });
            }

            entity.MaterialBridges.Clear();

            foreach (var item in modelInput.MaterialSelectors)
            {
                if (!item.Selected) continue;

                entity.MaterialBridges.Add(new CraftWorkMaterialBridge
                {
                    WorkId = entity.Id,
                    MaterialId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editelaboration", new { key = entity.Id });
        }

        #endregion

        #region Attachments

        [HttpGet]
        [Route("edit/{key}/attachments")]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public ActionResult EditAttachments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CraftWorkAttachmentsDisplayViewModel
            {
                Work = new CraftWorkDisplayViewModel(),
                Attachments = entity.Attachments.Select(item =>
                {
                    var result = new AttachmentDisplayViewModel();

                    result.Import(item);

                    return result;
                }).ToArray()
            };

            model.Work.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/attachments/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public IActionResult AddAttachment(System.Guid key, AttachmentInputViewModel model)
        {
            try
            {
                var entity = Repository.Get(key);

                ModelState.ClearValidationState("File");

                model.File = Request.Form.Files[0];                
               
                var updateModelTask = TryUpdateModelAsync(model);

                updateModelTask.Wait();

                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            model.File.CopyTo(memoryStream);

                            var attachment = new CraftWorkAttachment
                            {
                                Name = model.Name,
                                IsPublic = model.IsPublic,
                                Description = model.Description,
                                ContentType = model.File.ContentType,
                                Length = model.File.Length,
                                WorkId = entity.Id
                            };

                            attachmentFileManager.SaveFileContent(attachment.Id, memoryStream.ToArray(), attachment.ContentType);

                            attachmentRepository.Add(attachment, User.Identity.Name);
                        }

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editattachments", new { key = entity.Id });
                    }
                    catch (MOROPO.SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editattachments", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/attachments/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.CraftWorkEditor)]
        public IActionResult DeleteAttachment(System.Guid key, System.Guid attachmentId)
        {
            try
            {
                var entity = Repository.Get(key);

                var attachment = entity.Attachments.First(e => e.Id == attachmentId);

                attachmentRepository.Delete(attachment, User.Identity.Name);

                attachmentFileManager.DeleteFile(attachment);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editattachments", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editattachments", new { key });
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
