using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System.Linq;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
using System.Collections.Generic;
using System;
using MOROPO.FCBC.SGI.Models.Filter;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("works")]
    public class DesignWorksController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementFilteredController<DesignWork, System.Guid, DesignWorkInputViewModel, DesignWorkDisplayViewModel, DesignWorkFilterModel>
    {

        private readonly IEntityRepository<DesignManifestation, System.Guid> manifestationRepository;
        private readonly IEntityRepository<Currency, int> currencyRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;
        private readonly IEntityRepository<Award, System.Guid> awardRepository;
        private readonly IEntityRepository<Artist, System.Guid> creatorRepository;
        private readonly IEntityRepository<CreationGroup, System.Guid> creationGroupRepository;
        private readonly IEntityRepository<DesignTechnique, System.Guid> techniqueRepository;
        private readonly IEntityRepository<Material, System.Guid> materialRepository;
        private readonly IEntityRepository<DesignWorkAttachment, System.Guid> attachmentRepository;
        private readonly Managers.RelativeAttachmentFileManager attachmentFileManager;
        private readonly IEntityRepository<Thematic, Guid> thematicRepository;
        private readonly IEntityRepository<Critic, Guid> criticRepository;

        public DesignWorksController(IEntityRepository<Critic, Guid> criticRepository, IEntityRepository<Thematic, Guid> thematicRepository, IEntityRepository<DesignWorkAttachment, System.Guid> attachmentRepository, Managers.RelativeAttachmentFileManager attachmentFileManager, IEntityRepository<Material, System.Guid> materialRepository, IEntityRepository<DesignTechnique, System.Guid> techniqueRepository, IEntityRepository<CreationGroup, System.Guid> creationGroupRepository, IEntityRepository<Artist, System.Guid> creatorRepository, IEntityRepository<DesignManifestation, System.Guid> manifestationRepository, IEntityRepository<Currency, int> currencyRepository, IEntityRepository<Image, System.Guid> imageRepository, IImageFileManagerFacade imageFileManager, IEntityRepository<Award, System.Guid> awardRepository, IEntityRepository<DesignWork, System.Guid> repository, IStringLocalizer<DesignWorksController> localizer, ILogger<DesignWorksController> logger) : base(repository, localizer, logger)
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
            this.attachmentRepository = attachmentRepository;
            this.attachmentFileManager = attachmentFileManager;
            this.thematicRepository = thematicRepository;
            this.criticRepository = criticRepository;
        }

        [RequirePermissions(Permission.DesignWorkViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.DesignWorkViewer)]
        public override IActionResult Filter(DesignWorkFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequirePermissions(Permission.DesignWorkViewer)]
        protected override DesignWorkFilterModel GetFilterModel()
        {
            var filterModel = base.GetFilterModel();

            if (filterModel == null)
            {
                filterModel = new DesignWorkFilterModel();
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
            return "Design-work-management-filter";
        }

        [RequirePermissions(Permission.DesignWorkCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.DesignWorkCreator)]
        public override IActionResult Create(DesignWorkInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.DesignWorkEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.DesignWorkEditor)]
        public override IActionResult Edit(DesignWorkInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.DesignWorkRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.DesignWorkRemover)]
        public override IActionResult Delete(DesignWorkDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(DesignWorkInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigureEdit(DesignWork entity, DesignWorkInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(DesignWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(DesignWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
            PopulateInputModelForAvailableCurrencies(modelInput);
        }

        protected override void PreCreate(DesignWork entity, DesignWorkInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Manifestation = manifestationRepository.Get(modelInput.ManifestationId.Value);
        }

        protected override void PreEdit(DesignWork entity, DesignWorkInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.ManifestationId == modelInput.ManifestationId) return;

            entity.Manifestation = manifestationRepository.Get(modelInput.ManifestationId.Value);
        }

        private void PopulateInputModelForAvailableManifestations(DesignWorkInputViewModel model)
        {
            model.AvailableManifestations = manifestationRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateInputModelForAvailableCurrencies(DesignWorkInputViewModel model)
        {
            model.AvailableCurrencies = currencyRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }

        private void PopulateManifestations(DesignWorkFilterModel filter)
        {
            var designManifestations = manifestationRepository.GetAll();

            foreach (var designManifestation in designManifestations)
            {
                filter.Manifestation.Add(new SelectListItem { Value = designManifestation.Id.ToString(), Text = designManifestation.Name });
            }
        }

        private void PopulateDropDownAuthorWithCreatorsAndCreationGroup(DesignWorkFilterModel filter)
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

        #region Authors

        [HttpGet]
        [Route("edit/{key}/authors")]
        [RequirePermissions(Permission.DesignWorkEditor)]
        public ActionResult EditAuthors(System.Guid key)
        {
            var entity = Repository.Get(key);

            var creators = entity.CreatorBridges.Select(bridge => bridge.Creator).ToArray();
            var creationGroups = entity.CreationGroupBridges.Select(bridge => bridge.CreationGroup).ToArray();

            var model = new DesignWorkAuthorsDisplayViewModel
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
                Work = new DesignWorkDisplayViewModel(),
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
        [RequirePermissions(Permission.DesignWorkEditor)]
        public IActionResult AddCreator(System.Guid key, System.Guid creatorId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        entity.CreatorBridges.Add(new DesignWorkCreatorBridge
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
        [RequirePermissions(Permission.DesignWorkEditor)]
        public IActionResult AddCreationGroup(System.Guid key, System.Guid creationGroupId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        entity.CreationGroupBridges.Add(new DesignWorkCreationGroupBridge
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
        [RequirePermissions(Permission.DesignWorkEditor)]
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
        [RequirePermissions(Permission.DesignWorkEditor)]
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
        [RequirePermissions(Permission.DesignWorkEditor)]
        public ActionResult EditImages(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new DesignWorkImagesDisplayViewModel
            {
                Work = new DesignWorkDisplayViewModel(),
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
        [RequirePermissions(Permission.DesignWorkEditor)]
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

                            var image = new Image { ContentType = imageFile.ContentType, Length = imageFile.Length, About = "For DesignWork " + entity.Id };

                            imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), imageFile.ContentType);

                            entity.ImageBridges.Add(new DesignWorkImageBridge
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
        [RequirePermissions(Permission.DesignWorkEditor)]
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
        [Route("edit/{key}/DesignWorkAwards")]
        [RequirePermissions(Permission.DesignWorkEditor)]
        public IActionResult DesignWorkAwards(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new DesignWorkAwardManageViewModel
                {
                    DesignWorkDisplay = new DesignWorkDisplayViewModel()
                };
                PopulateAwards(resultModel);
                resultModel.DesignWorkDisplay.Import(entity);

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
        [RequirePermissions(Permission.DesignWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDesignWorkAwards(DesignWorkAwardManageViewModel modelInput)
        {
            DesignWork entity = null;
            Award award = null;
            try
            {
                entity = Repository.Get(modelInput.DesignWorkDisplay.Id);
                award = awardRepository.Get(modelInput.AwardId);

                modelInput.DesignWorkDisplay.Import(entity);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Awards.Add(new DesignWorkAward { Work = entity, Award = award, WorkId = entity.Id, AwardId = award.Id, Context = modelInput.Context, Year = modelInput.Year });

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

            return RedirectToAction("DesignWorkAwards", new { key = entity.Id });
        }

        [Route("edit/Awards/delete")]
        [RequirePermissions(Permission.DesignWorkEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDesignWorkAwards(DesignWorkAwardDisplayViewModel modelDisplay)
        {
            DesignWork entity = null;
            try
            {
                entity = Repository.Get(modelDisplay.DesignWorkId);

            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                var DesignWorkAward = entity.Awards.First(a => a.Id == modelDisplay.Id);
                entity.Awards.Remove(DesignWorkAward);

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

            return RedirectToAction("DesignWorkAwards", new { key = entity.Id });
        }

        private void PopulateAwards(DesignWorkAwardManageViewModel modelInput)
        {
            var awads = awardRepository.GetAll();

            if (awads.Count() > 0)
            {
                modelInput.SelectAwards = awardRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name + " - " + item.Country.Name }).ToArray();
            }
        }

        #endregion

        #region Elaboration

        [HttpGet]
        [Route("edit/{key}/elaboration")]
        [RequirePermissions(Permission.DesignWorkEditor)]
        public ActionResult EditElaboration(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new DesignWorkElaborationInputViewModel
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
                Work = new DesignWorkDisplayViewModel()
            };

            model.Work.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/elaboration")]
        [RequirePermissions(Permission.DesignWorkEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditElaboration(System.Guid key, DesignWorkElaborationInputViewModel modelInput)
        {
            DesignWork entity = null;

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

                entity.TechniqueBridges.Add(new DesignWorkTechniqueBridge
                {
                    WorkId = entity.Id,
                    TechniqueId = item.Id
                });
            }

            entity.MaterialBridges.Clear();

            foreach (var item in modelInput.MaterialSelectors)
            {
                if (!item.Selected) continue;

                entity.MaterialBridges.Add(new DesignWorkMaterialBridge
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
        [RequirePermissions(Permission.DesignWorkEditor)]
        public ActionResult EditAttachments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new DesignWorkAttachmentsDisplayViewModel
            {
                Work = new DesignWorkDisplayViewModel(),
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
        [RequirePermissions(Permission.DesignWorkEditor)]
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

                            var attachment = new DesignWorkAttachment
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
        [RequirePermissions(Permission.DesignWorkEditor)]
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
        [RequirePermissions(Permission.DesignWorkEditor)]
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
        [RequirePermissions(Permission.DesignWorkEditor)]
        public IActionResult EditAttachment(System.Guid key, AttachmentInputViewModel modelInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DesignWorkAttachment val = attachmentRepository.Get(modelInput.Id);
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

        #region Thematic

        [HttpGet]
        [Route("edit/{key}/thematic")]
        [RequirePermissions(Permission.DesignWorkEditor)]
        public ActionResult EditThematic(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new DesignWorkThematicInputViewModel
            {
                ThematicSelectors = thematicRepository.GetAll().Select(item => new SelectionDescriptedItemInputViewModel<System.Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ThematicBridges.Any(bridge => bridge.ThematicId == item.Id),
                    Description = item.Description
                }).ToArray(),

                Work = new DesignWorkDisplayViewModel()
            };

            model.Work.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/thematic")]
        [RequirePermissions(Permission.DesignWorkEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditThematic(System.Guid key, DesignWorkThematicInputViewModel modelInput)
        {
            DesignWork entity = null;

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

                entity.ThematicBridges.Add(new DesignWorkThematicBridge
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
        //[Route("edit/{key}/EditWorkCriterion")]
        //[RequirePermissions(Permission.DesignWorkEditor)]
        //public IActionResult EditWorkCriterion(System.Guid key)
        //{
        //    try
        //    {
        //        var entity = Repository.Get(key);

        //        var resultModel = new DesignWorkCriterionManageViewModel
        //        {
        //            WorkDisplay = new DesignWorkDisplayViewModel()
        //        };
        //        PopulateCritics(resultModel);
        //        resultModel.WorkDisplay.Import(entity);

        //        return View(resultModel);
        //    }
        //    catch (System.Exception e)
        //    {
        //        logger.LogError(e.InnerException, e.Message);
        //    }

        //    SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

        //    return GetDefaultActionResult();
        //}

        //[Route("edit/Criterion/add")]
        //[RequirePermissions(Permission.DesignWorkEditor)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddWorkCriterion(DesignWorkCriterionManageViewModel modelInput)
        //{
        //    DesignWork entity = null;
        //    Critic critic = null;
        //    try
        //    {
        //        entity = Repository.Get(modelInput.WorkDisplay.Id);
        //        critic = criticRepository.Get(modelInput.CriticId);

        //        modelInput.WorkDisplay.Import(entity);
        //    }
        //    catch { }

        //    if (entity == null)
        //    {
        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

        //        return GetDefaultActionResult();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        entity.WorkAssessmentBridges.Add(new DesignWorkAssessmentBridge { Work = entity, Critic = critic, WorkId = entity.Id, CriticId = critic.Id, Criterion = modelInput.Criterion, DateOfAssessment = modelInput.DateOfCriterion });

        //        Repository.Update(entity, User.Identity.Name);

        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
        //    }
        //    else
        //    {
        //        var messages = new List<string>();

        //        foreach (var item in ModelState)
        //        {
        //            if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

        //            foreach (var error in item.Value.Errors)
        //            {
        //                messages.Add(error.ErrorMessage);
        //            }
        //        }

        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
        //    }

        //    return RedirectToAction("EditWorkCriterion", new { key = entity.Id });
        //}

        //[Route("edit/Criterion/update")]
        //[RequirePermissions(Permission.DesignWorkEditor)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult UpdateWorkCriterion(DesignWorkAssessmentBridgeDisplayViewModel modelInput)
        //{
        //    DesignWork entity = null;
        //    Critic critic = null;
        //    try
        //    {
        //        entity = Repository.Get(modelInput.WorkId);
        //        critic = criticRepository.Get(modelInput.CriticUpdateId);

        //    }
        //    catch { }

        //    if (entity == null)
        //    {
        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

        //        return GetDefaultActionResult();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var workAssessmentBridge = entity.WorkAssessmentBridges.FirstOrDefault(a => a.WorkId == modelInput.WorkId && a.CriticId == modelInput.CriticId);
        //        entity.WorkAssessmentBridges.Remove(workAssessmentBridge);

        //        entity.WorkAssessmentBridges.Add(new DesignWorkAssessmentBridge { Work = entity, Critic = critic, WorkId = entity.Id, CriticId = critic.Id, Criterion = modelInput.Criterion, DateOfAssessment = modelInput.DateOfAssessment });

        //        Repository.Update(entity, User.Identity.Name);

        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
        //    }
        //    else
        //    {
        //        var messages = new List<string>();

        //        foreach (var item in ModelState)
        //        {
        //            if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

        //            foreach (var error in item.Value.Errors)
        //            {
        //                messages.Add(error.ErrorMessage);
        //            }
        //        }

        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
        //    }

        //    return RedirectToAction("EditWorkCriterion", new { key = entity.Id });
        //}

        //[Route("edit/Criterion/delete")]
        //[RequirePermissions(Permission.DesignWorkEditor)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteWorkCriterion(ValorationDisplayViewModel modelDisplay)
        //{
        //    DesignWork entity = null;
        //    try
        //    {
        //        entity = Repository.Get(modelDisplay.Id);

        //    }
        //    catch { }

        //    if (entity == null)
        //    {
        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

        //        return GetDefaultActionResult();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var workCriterion = entity.WorkAssessmentBridges.First(a => a.WorkId == modelDisplay.Id && a.CriticId == modelDisplay.CriticId);
        //        entity.WorkAssessmentBridges.Remove(workCriterion);

        //        Repository.Update(entity, User.Identity.Name);

        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
        //    }
        //    else
        //    {
        //        var messages = new List<string>();

        //        foreach (var item in ModelState)
        //        {
        //            if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

        //            foreach (var error in item.Value.Errors)
        //            {
        //                messages.Add(error.ErrorMessage);
        //            }
        //        }

        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
        //    }

        //    return RedirectToAction("EditWorkCriterion", new { key = entity.Id });
        //}

        #endregion

        private void PopulateCritics(DesignWorkCriterionManageViewModel modelInput)
        {
            var awads = criticRepository.GetAll();

            if (awads.Count() > 0)
            {
                modelInput.SelectCritics = criticRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Identity.Name + " " + item.Identity.LastName }).ToArray();
            }
        }
    }
}
