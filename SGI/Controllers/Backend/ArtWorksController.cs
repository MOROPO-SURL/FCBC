using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
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
        private readonly IEntityRepository<ArtWorkVideo, System.Guid> videoRepository;
        private readonly IImageFileManagerFacade imageFileManager;
        private readonly Managers.RelativeAttachmentFileManager attachmentFileManager;
        private readonly IEntityRepository<Award, Guid> awardRepository;
        private readonly IEntityRepository<Thematic, Guid> thematicRepository;
        private readonly IEntityRepository<Critic, Guid> criticRepository;
        private readonly IEntityRepository<ArtWorkValoration, Guid> valorationRepository;

        public ArtWorksController(IEntityRepository<ArtWorkValoration, Guid> valorationRepository, IEntityRepository<Critic, Guid> criticRepository, IEntityRepository<Thematic, Guid> thematicRepository, Managers.RelativeAttachmentFileManager attachmentFileManager, IImageFileManagerFacade imageFileManager, IEntityRepository<ArtWorkAttachment, System.Guid> attachmentRepository, IEntityRepository<ArtWorkVideo, System.Guid> videoRepository, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<CreationGroup, System.Guid> creationGroupRepository, IEntityRepository<Artist, System.Guid> creatorRepository, IEntityRepository<Currency, int> currencyRepository, IEntityRepository<UnitMeasurement, System.Guid> unitMeasurementRepository, IEntityRepository<Material, System.Guid> materialRepository, IEntityRepository<ArtisticTechnique, System.Guid> techniqueRepository, IEntityRepository<ArtisticManifestation, System.Guid> manifestationRepository, IEntityRepository<Award, Guid> awardRepository, IEntityRepository<ArtWork, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ArtWorksController> localizer, ILogger<ArtWorksController> logger) : base(repository, localizer, logger)
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
            this.videoRepository = videoRepository;
            this.imageFileManager = imageFileManager;
            this.attachmentFileManager = attachmentFileManager;
            this.awardRepository = awardRepository;
            this.thematicRepository = thematicRepository;
            this.criticRepository = criticRepository;
            this.valorationRepository = valorationRepository;
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

        protected override void ConfigurePostFailEditValidation(ArtWork entity, ArtWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

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
            model.AvailableManifestations = manifestationRepository.GetAll().Select(entity => new SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateInputModelForAvailableUnitMeasurements(ArtWorkInputViewModel model)
        {
            model.AvaiableDimensionsUnitMeasurements = unitMeasurementRepository.FindBy(entity => entity.MilimetersRatio != null).Select(entity => new SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
            model.AvaiableWeightDimensionsUnitMeasurements = unitMeasurementRepository.FindBy(entity => entity.GramsRatio != null).Select(entity => new SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateInputModelForAvailableCurrencies(ArtWorkInputViewModel model)
        {
            model.AvailableCurrencies = currencyRepository.GetAll().Select(entity => new SelectListItem
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
        public ActionResult EditImages(Guid key)
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
        public IActionResult AddImage(Guid key, Microsoft.AspNetCore.Http.IFormFile imageFile)
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
            catch (Exception e)
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
        public IActionResult DeleteImage(Guid key, Guid imageId)
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

        [HttpPost]
        [Route("edit/{key}/images/featured")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult FeaturedImage(Guid key, Guid imageId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.ImageBridges.First(b => b.ImageId == imageId);

                entity.FeatureImageId = bridge.ImageId;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                return RedirectToAction("editimages", new { key });
            }
            catch (Exception e)
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
        [Route("edit/{key}/artworkawards")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult ArtWorkAwards(Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ArtWorkAwardManageViewModel
                {
                    ArtWork = new ArtWorkDisplayViewModel()
                };

                PopulateAwards(resultModel);

                resultModel.ArtWork.Import(entity);

                return View(resultModel);
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/artworkawards/add")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddArtWorkAward(Guid key, Guid awardId, string context)
        {
            try
            {
                var entity = Repository.Get(key);

                var award = awardRepository.Get(awardId);

                if (ModelState.IsValid)
                {
                    entity.Awards.Add(new ArtWorkAward { Work = entity, Award = award, WorkId = entity.Id, AwardId = award.Id, Context = context });

                    Repository.Update(entity, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
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

                    SetDefaultNotification(NotificationType.Danger, messages.ToArray());
                }

                return RedirectToAction("artworkawards", new { key = entity.Id });
            }
            catch
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }
        }

        [Route("edit/{key}/artworkawards/delete")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteArtWorkAward(Guid key, Guid id)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    var artWorkAward = entity.Awards.First(a => a.Id == id);

                    entity.Awards.Remove(artWorkAward);

                    Repository.Update(entity, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
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

                    SetDefaultNotification(NotificationType.Danger, messages.ToArray());
                }

                return RedirectToAction("artworkawards", new { key = entity.Id });
            }
            catch
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }
        }

        private void PopulateAwards(ArtWorkAwardManageViewModel modelInput)
        {
            modelInput.SelectAwards = awardRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToArray();
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

        #region Criterion

        [Route("edit/{key}/criteria")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult EditCriteria(Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ArtWorkCriteriaViewModel
                {
                    Work = new ArtWorkDisplayViewModel(),
                    Criterials = valorationRepository.FindBy(item => item.WorkId == entity.Id).ToArray().Select(item =>
                    {
                        var _model = new CriterialDisplayViewModel
                        {
                            Id = item.Id,
                            Declaration = item.Declaration,
                            DeclaredAt = item.DeclaredAt,
                            Critic = new CriticDisplayViewModel()
                        };

                        _model.Critic.Import(item.Critic);

                        return _model;
                    }).ToArray(),
                    CriticSelectListItems = criticRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Identity.Name + " " + item.Identity.LastName }).ToArray()
                };

                resultModel.Work.Import(entity);

                return View(resultModel);
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/criteria/add")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCriterion(Guid key, CriterialInputViewModel modelInput)
        {
            try
            {
                var entity = Repository.Get(key);

                var critic = criticRepository.Get(modelInput.CriticId);

                if (ModelState.IsValid)
                {
                    valorationRepository.Add(new ArtWorkValoration
                    {
                        Critic = critic,
                        CriticId = modelInput.CriticId,
                        Work = entity,
                        WorkId = entity.Id,
                        DeclaredAt = modelInput.DateOfCriterion,
                        Declaration = modelInput.Criterion
                    }, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
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

                    SetDefaultNotification(NotificationType.Danger, messages.ToArray());
                }

                return RedirectToAction("EditCriteria", new { key = entity.Id });
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }


        [Route("edit/{key}/criteria/edit")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCriterion(Guid key, CriterialInputViewModel modelInput)
        {
            try
            {
                var entity = Repository.Get(key);

                var criterion = valorationRepository.Get(modelInput.Id);

                if (ModelState.IsValid)
                {
                    criterion.DeclaredAt = modelInput.DateOfCriterion;
                    criterion.Declaration = modelInput.Criterion;

                    valorationRepository.Update(criterion, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());
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

                    SetDefaultNotification(NotificationType.Danger, messages.ToArray());
                }

                return RedirectToAction("EditCriteria", new { key = entity.Id });
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/criteria/delete")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCriterion(Guid key, Guid criterionId)
        {
            try
            {
                var entity = Repository.Get(key);

                var criterion = valorationRepository.Get(criterionId);

                if (ModelState.IsValid)
                {
                    valorationRepository.Delete(criterion, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());
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

                    SetDefaultNotification(NotificationType.Danger, messages.ToArray());
                }

                return RedirectToAction("EditCriteria", new { key = entity.Id });
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #endregion

        #region Videos

        [HttpGet]
        [Route("edit/{key}/videos")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public ActionResult EditVideos(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtWorkVideosDisplayViewModel
            {
                Work = new ArtWorkDisplayViewModel(),
                Videos = entity.Videos.Select(item =>
                {
                    var result = new VideoDisplayViewModel();

                    result.Import(item);

                    return result;
                }).ToArray()
            };

            model.Work.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/videos/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult AddVideo(System.Guid key, VideoInputViewModel model)
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

                            var video = new ArtWorkVideo
                            {
                                Name = model.Name,
                                IsPublic = model.IsPublic,
                                Description = model.Description,
                                ContentType = model.File.ContentType,
                                Length = model.File.Length,
                                WorkId = entity.Id
                            };

                            attachmentFileManager.SaveFileContent(video.Id, memoryStream.ToArray(), video.ContentType);

                            videoRepository.Add(video, User.Identity.Name);
                        }

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditVideos", new { key = entity.Id });
                    }
                    catch (MOROPO.SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditVideos", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/videos/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult DeleteVideo(System.Guid key, System.Guid videoId)
        {
            try
            {
                var entity = Repository.Get(key);

                var video = entity.Videos.First(e => e.Id == videoId);

                videoRepository.Delete(video, User.Identity.Name);

                attachmentFileManager.DeleteFile(video);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("EditVideos", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("EditVideos", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }


        [HttpPost]
        [Route("edit/{key}/{videoId}/videos/download")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public FileResult DownloadVideo(System.Guid key, System.Guid videoId)
        {

            var entity = Repository.Get(key);
            var video = entity.Videos.FirstOrDefault(att => att.Id == videoId);

            var fileBytes = attachmentFileManager.GetFileContent(video);
            var extension = video.ContentType.Split('/');
            var fileName = video.Name + "." + extension[1];

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        [HttpPost]
        [Route("edit/{key}/videos/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public IActionResult EditVideo(System.Guid key, VideoInputViewModel modelInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ArtWorkVideo val = videoRepository.Get(modelInput.Id);
                    try
                    {
                        modelInput.Mergue(val);

                        videoRepository.Update(val, base.User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());
                        return RedirectToAction("EditVideos", new { key });
                    }
                    catch (Exception ex2)
                    {
                        logger.LogError(ex2.InnerException, ex2.Message);
                        SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());
                        return RedirectToAction("EditVideos", new { key });
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

        #region Translations
        [Route("edit/{key}/translations")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        public ActionResult EditTranslations(System.Guid key)
        {
            List<Persistence.Enumerations.TranslatableLanguage> languages = new List<Persistence.Enumerations.TranslatableLanguage>();

            foreach (Persistence.Enumerations.TranslatableLanguage language in Enum.GetValues(typeof(Persistence.Enumerations.TranslatableLanguage)))
            {
                languages.Add(language);
            }

            var entity = Repository.Get(key);

            var languagesSelected = entity.Translations.Select(l => l.TranslatableLanguage).ToArray();            

            var resultModel = new ArtWorkTranslationsManagementViewModel
            {
                Work = new ArtWorkDisplayViewModel(),
                NotSelectedLanguage = languages.Except(languagesSelected).ToArray(),
                Translations = entity.Translations.Select(item =>
                {
                    var model = new ArtWorkTranslationDisplayViewModel
                    {
                        TranslatableLanguage = item.TranslatableLanguage,
                        Title = item.Title,
                        Collection = item.Collection,
                        Location = item.Collection,
                        About = item.About
                    };
                    return model;
                }).ToArray()
            };

            resultModel.Work.Import(entity);                       

            return View(resultModel);
        }

        [Route("edit/translations/add")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTranslation(WorkLanguagesInputViewModel modelInput)
        {
            ArtWork entity = null;

            try
            {
                entity = Repository.Get(modelInput.WorkId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }


            if (ModelState.IsValid)
            {
                entity.Translations.Add(new ArtWorkTranslation
                {
                    ArtWorkId = entity.Id,
                    TranslatableLanguage = modelInput.TranslatableLanguage,
                    Title = modelInput.Title,
                    Collection = modelInput.Collection,
                    Location = modelInput.Location,
                    About = modelInput.About
                });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
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

                SetDefaultNotification(NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        [Route("edit/translations/modify")]
        [RequirePermissions(Security.Authorization.Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyTranslation(WorkLanguagesInputViewModel modelInput)
        {
            ArtWork entity = null;

            try
            {
                entity = Repository.Get(modelInput.WorkId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }


            if (ModelState.IsValid)
            {
                var translation = entity.Translations.First(item => item.TranslatableLanguage == modelInput.TranslatableLanguage);

                translation.Title = modelInput.Title;
                translation.Collection = modelInput.Collection;
                translation.Location = modelInput.Location;
                translation.About = modelInput.About;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "EditionSuccessMessage", null);
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

                SetDefaultNotification(NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        [Route("edit/translations/remove")]
        [RequirePermissions(Permission.ArtWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTranslation(System.Guid ArtWorkId, TranslatableLanguage translatableLanguage)
        {
            ArtWork entity = null;

            try
            {
                entity = Repository.Get(ArtWorkId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Translations.First(b => b.ArtWorkId == ArtWorkId && b.TranslatableLanguage == translatableLanguage);

                entity.Translations.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        #endregion

    }
}
