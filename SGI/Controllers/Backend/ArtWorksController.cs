using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.Filter;
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
    public class ArtWorksController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementFilteredController<ArtWork, System.Guid, ArtWorkInputViewModel, ArtWorkDisplayViewModel, ArtWorkFilterModel>
    {
        private readonly IEntityRepository<ArtisticManifestation, System.Guid> manifestationRepository;
        private readonly IEntityRepository<ArtisticTechnique, System.Guid> techniqueRepository;
        private readonly IEntityRepository<Material, System.Guid> materialRepository;
        private readonly IEntityRepository<UnitMeasurement, System.Guid> unitMeasurementRepository;
        private readonly IEntityRepository<Currency, int> currencyRepository;
        private readonly IEntityRepository<Artist, System.Guid> creatorRepository;
        private readonly IEntityRepository<CreationGroup, System.Guid> creationGroupRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<ArtWorkAttachment, System.Guid> attachmentRepository;
        private readonly IImageFileManagerFacade imageFileManager;
        private readonly Managers.RelativeAttachmentFileManager attachmentFileManager;
        private readonly IEntityRepository<Award, Guid> awardRepository;
        private readonly IEntityRepository<Thematic, Guid> thematicRepository;

        public ArtWorksController(IEntityRepository<Thematic, Guid> thematicRepository, Managers.RelativeAttachmentFileManager attachmentFileManager, IImageFileManagerFacade imageFileManager, IEntityRepository<ArtWorkAttachment, System.Guid> attachmentRepository, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<CreationGroup, System.Guid> creationGroupRepository, IEntityRepository<Artist, System.Guid> creatorRepository, IEntityRepository<Currency, int> currencyRepository, IEntityRepository<UnitMeasurement, System.Guid> unitMeasurementRepository, IEntityRepository<Material, System.Guid> materialRepository, IEntityRepository<ArtisticTechnique, System.Guid> techniqueRepository, IEntityRepository<ArtisticManifestation, System.Guid> manifestationRepository, IEntityRepository<Award, Guid> awardRepository, IEntityRepository<ArtWork, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ArtWorksController> localizer, ILogger<ArtWorksController> logger) : base(repository, localizer, logger)
        {
            this.manifestationRepository = manifestationRepository;
            this.techniqueRepository = techniqueRepository;
            this.materialRepository = materialRepository;
            this.unitMeasurementRepository = unitMeasurementRepository;
            this.currencyRepository = currencyRepository;
            this.creatorRepository = creatorRepository;
            this.creationGroupRepository = creationGroupRepository;
            this.imageRepository = imageRepository;
            this.attachmentRepository = attachmentRepository;
            this.imageFileManager = imageFileManager;
            this.attachmentFileManager = attachmentFileManager;
            this.awardRepository = awardRepository;
            this.thematicRepository = thematicRepository;
        }

        [RequirePermissions(Permission.ArtWorkViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ArtWorkViewer)]
        public override IActionResult Filter(ArtWorkFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequirePermissions(Permission.ArtWorkViewer)]
        protected override ArtWorkFilterModel GetFilterModel()
        {
            var filterModel = base.GetFilterModel();

            if (filterModel == null)
            {
                filterModel = new ArtWorkFilterModel();
            }

            filterModel.Manifestation = new List<SelectListItem>
            {
                new SelectListItem("Cualquiera", "0")
            };

            filterModel.Author = new List<SelectListItem>
            {
                new SelectListItem("Cualquiera", "0")
            };

            PopulateManifestations(filterModel);
            PopulateDropDownAuthorWithCreatorsAndCreationGroup(filterModel);

            return filterModel;
        }

        protected override string GetFilterSessionID()
        {
            return "art-work-management-filter";
        }

        [RequirePermissions(Permission.ArtWorkCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ArtWorkCreator)]
        public override IActionResult Create(ArtWorkInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ArtWorkEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ArtWorkEditor)]
        public override IActionResult Edit(ArtWorkInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ArtWorkRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ArtWorkRemover)]
        public override IActionResult Delete(ArtWorkDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(ArtWorkInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableUnitMeasurements(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigureEdit(ArtWork entity, ArtWorkInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableUnitMeasurements(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ArtWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableUnitMeasurements(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ArtWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableUnitMeasurements(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void PreCreate(ArtWork entity, ArtWorkInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Manifestation = manifestationRepository.Get(modelInput.ManifestationId.Value);
        }

        protected override void PreEdit(ArtWork entity, ArtWorkInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.ManifestationId == modelInput.ManifestationId) return;

            entity.Manifestation = manifestationRepository.Get(modelInput.ManifestationId.Value);
        }

        private void PopulateInputModelForAvailableManifestations(ArtWorkInputViewModel model)
        {
            model.AvailableManifestations = manifestationRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateInputModelForAvailableUnitMeasurements(ArtWorkInputViewModel model)
        {
            model.AvaiableUnitMeasurements = unitMeasurementRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateInputModelForAvailableCurrencies(ArtWorkInputViewModel model)
        {
            model.AvailableCurrencies = currencyRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateManifestations(ArtWorkFilterModel filter)
        {
            var artManifestations = manifestationRepository.GetAll();

            foreach (var artManifestation in artManifestations)
            {
                filter.Manifestation.Add(new SelectListItem { Value = artManifestation.Id.ToString(), Text = artManifestation.Name });
            }
        }

        private void PopulateDropDownAuthorWithCreatorsAndCreationGroup(ArtWorkFilterModel filter)
        {
            var creators = creatorRepository.GetAll();
            var creationGroups = creationGroupRepository.GetAll();
            var creatorsGroup = new SelectListGroup { Name = "Creadores" };
            var creationGroupsGroup = new SelectListGroup { Name = "Grupos de Ceación" };

            foreach (var creator in creators)
            {
                filter.Author.Add(new SelectListItem { Value = creator.Id.ToString(), Text = creator.Identity.Name, Group = creatorsGroup });
            }
            foreach (var creationGroup in creationGroups)
            {
                filter.Author.Add(new SelectListItem { Value = creationGroup.Id.ToString(), Text = creationGroup.Identity.Name, Group = creationGroupsGroup });
            }
        }

        #region Elaboration

        [HttpGet]
        [Route("edit/{key}/elaboration")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public ActionResult EditElaboration(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtWorkElaborationInputViewModel
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
                Work = new ArtWorkDisplayViewModel()
            };

            model.Work.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/elaboration")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditElaboration(System.Guid key, ArtWorkElaborationInputViewModel modelInput)
        {
            ArtWork entity = null;

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

                entity.TechniqueBridges.Add(new ArtWorkTechniqueBridge
                {
                    WorkId = entity.Id,
                    TechniqueId = item.Id
                });
            }

            entity.MaterialBridges.Clear();

            foreach (var item in modelInput.MaterialSelectors)
            {
                if (!item.Selected) continue;

                entity.MaterialBridges.Add(new ArtWorkMaterialBridge
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

        #region Authors

        [HttpGet]
        [Route("edit/{key}/authors")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public ActionResult EditAuthors(System.Guid key)
        {
            var entity = Repository.Get(key);

            var creators = entity.CreatorBridges.Select(bridge => bridge.Creator).ToArray();
            var creationGroups = entity.CreationGroupBridges.Select(bridge => bridge.CreationGroup).ToArray();

            var model = new ArtWorkAuthorsDisplayViewModel
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
                Work = new ArtWorkDisplayViewModel(),
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
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult AddCreator(System.Guid key, System.Guid creatorId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        entity.CreatorBridges.Add(new ArtWorkCreatorBridge
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
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult AddCreationGroup(System.Guid key, System.Guid creationGroupId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        entity.CreationGroupBridges.Add(new ArtWorkCreationGroupBridge
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
        [RequirePermissions(Permission.ArtWorkEditor)]
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
        [RequirePermissions(Permission.ArtWorkEditor)]
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

        #region Images

        [HttpGet]
        [Route("edit/{key}/images")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public ActionResult EditImages(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtWorkImagesDisplayViewModel
            {
                Work = new ArtWorkDisplayViewModel(),
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
        [RequirePermissions(Permission.ArtWorkEditor)]
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

                            var image = new Image { ContentType = imageFile.ContentType, Length = imageFile.Length, About = "For ArtWork " + entity.Id };

                            imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), imageFile.ContentType);

                            entity.ImageBridges.Add(new ArtWorkImageBridge
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
        [RequirePermissions(Permission.ArtWorkEditor)]
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

        #region Attachments

        [HttpGet]
        [Route("edit/{key}/attachments")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public ActionResult EditAttachments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtWorkAttachmentsDisplayViewModel
            {
                Work = new ArtWorkDisplayViewModel(),
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
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult AddAttachment(System.Guid key, AttachmentInputViewModel model)
        {
            try
            {
                var entity = Repository.Get(key);

                ModelState.ClearValidationState("ImageFile");

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

                            var attachment = new ArtWorkAttachment
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
        [RequirePermissions(Permission.ArtWorkEditor)]
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


        [HttpPost]
        [Route("edit/{key}/{attachmentId}/attachments/download")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public FileResult DownloadAttachment(System.Guid key, System.Guid attachmentId)
        {

            var entity = Repository.Get(key);
            var attachment = entity.Attachments.FirstOrDefault(att => att.Id == attachmentId);

            var fileBytes = attachmentFileManager.GetFileContent(attachment);
            var extension = attachment.ContentType.Split('/');
            var fileName = attachment.Name + "." + extension[1];

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        [HttpPost]
        [Route("edit/{key}/attachments/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult EditAttachment(System.Guid key, AttachmentInputViewModel modelInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ArtWorkAttachment val = attachmentRepository.Get(modelInput.Id);
                    try
                    {
                        modelInput.Mergue(val);

                        attachmentRepository.Update(val, base.User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());
                        return RedirectToAction("editattachments", new { key });
                    }
                    catch (Exception ex2)
                    {
                        logger.LogError(ex2.InnerException, ex2.Message);
                        SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());
                        return RedirectToAction("editattachments", new { key });
                    }
                }
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }
            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #endregion

        #region Awards
        [Route("edit/{key}/ArtWorkAwards")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult ArtWorkAwards(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ArtWorkAwardManageViewModel
                {
                    ArtWorkDisplay = new ArtWorkDisplayViewModel()
                };
                PopulateAwards(resultModel);
                resultModel.ArtWorkDisplay.Import(entity);

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
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddArtWorkAwards(ArtWorkAwardManageViewModel modelInput)
        {
            ArtWork entity = null;
            Award award = null;
            try
            {
                entity = Repository.Get(modelInput.ArtWorkDisplay.Id);
                award = awardRepository.Get(modelInput.AwardId);

                modelInput.ArtWorkDisplay.Import(entity);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Awards.Add(new ArtWorkAward { Work = entity, Award = award, WorkId = entity.Id, AwardId = award.Id, Context = modelInput.Context, Year = modelInput.Year });

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

            return RedirectToAction("ArtWorkAwards", new { key = entity.Id });
        }

        [Route("edit/Awards/delete")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteArtWorkAwards(ArtWorkAwardDisplayViewModel modelDisplay)
        {
            ArtWork entity = null;
            try
            {
                entity = Repository.Get(modelDisplay.ArtWorkId);

            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                var artWorkAward = entity.Awards.First(a => a.Id == modelDisplay.Id);
                entity.Awards.Remove(artWorkAward);

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

            return RedirectToAction("ArtWorkAwards", new { key = entity.Id });
        }

        private void PopulateAwards(ArtWorkAwardManageViewModel modelInput)
        {
            var awads = awardRepository.GetAll();

            if (awads.Count() > 0)
            {
                modelInput.SelectAwards = awardRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name + " - " + item.Country.Name }).ToArray();
            }
        }

        #endregion

        #region Thematic

        [HttpGet]
        [Route("edit/{key}/thematic")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public ActionResult EditThematic(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtWorkThematicInputViewModel
            {
                ThematicSelectors = thematicRepository.GetAll().Select(item => new SelectionDescriptedItemInputViewModel<System.Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ThematicBridges.Any(bridge => bridge.ThematicId == item.Id),
                    Description = item.Description
                }).ToArray(),

                Work = new ArtWorkDisplayViewModel()
            };

            model.Work.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/thematic")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditThematic(System.Guid key, ArtWorkThematicInputViewModel modelInput)
        {
            ArtWork entity = null;

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

            entity.ThematicBridges.Clear();

            foreach (var item in modelInput.ThematicSelectors)
            {
                if (!item.Selected) continue;

                entity.ThematicBridges.Add(new ArtWorkThematicBridge
                {
                    WorkId = entity.Id,
                    ThematicId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("EditThematic", new { key = entity.Id });
        }

        #endregion
    }
}
