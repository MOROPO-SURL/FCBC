using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class CreationGroupsController : AbstractEntityManagementController<CreationGroup, Guid, CreationGroupInputViewModel, CreationGroupDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;
        private readonly IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository;
        private readonly IEntityRepository<Artist, Guid> artistRepository;
        private readonly IEntityRepository<ArtisticManifestationGroup, Guid> artisticManifestationGroupRepository;
        private readonly IEntityRepository<CraftManifestation, Guid> craftManifestationRepository;
        private readonly IEntityRepository<CraftManifestationGroup, Guid> craftManifestationGroupRepository;
        private readonly IEntityRepository<DesignManifestation, Guid> designManifestationRepository;
        private readonly IEntityRepository<DesignManifestationGroup, Guid> designManifestationGroupRepository;
        private readonly IEntityRepository<ServiceLine, Guid> serviceLineRepository;
        private readonly IEntityRepository<ServiceLineGroup, Guid> serviceLineGroupRepository;
        private readonly IEntityRepository<Resolution, Guid> resolutionRepository;
        private readonly IEntityRepository<Award, Guid> awardRepository;
        private readonly IEntityRepository<Recognition, Guid> recognitionRepository;
        private readonly IEntityRepository<Distinction, Guid> distintionsRepository;
        public CreationGroupsController(IEntityRepository<Artist, Guid> artistRepository, IEntityRepository<CreationGroup, Guid> repository, IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository, IEntityRepository<CraftManifestation, Guid> craftManifestationRepository, IEntityRepository<CraftManifestationGroup, Guid> craftManifestationGroupRepository, IEntityRepository<DesignManifestation, Guid> designManifestationRepository, IEntityRepository<ServiceLine, Guid> serviceLineRepository, IEntityRepository<DesignManifestationGroup, Guid> designManifestationGroupRepository, IEntityRepository<ServiceLineGroup, Guid> serviceLineGroupRepository, IEntityRepository<Award, Guid> awardRepository,
            IEntityRepository<Recognition, Guid> recognitionRepository, IEntityRepository<Distinction, Guid> distintionsRepository, IStringLocalizer<CreationGroupsController> localizer, IEntityRepository<Resolution, Guid> resolutionRepository, ILogger<CreationGroupsController> logger, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<ArtisticManifestationGroup, Guid> artisticManifestationGroupRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.artisticManifestationRepository = artisticManifestationRepository;
            this.artistRepository = artistRepository;
            this.artisticManifestationGroupRepository = artisticManifestationGroupRepository;
            this.craftManifestationRepository = craftManifestationRepository;
            this.craftManifestationGroupRepository = craftManifestationGroupRepository;
            this.designManifestationRepository = designManifestationRepository;
            this.designManifestationGroupRepository = designManifestationGroupRepository;
            this.serviceLineRepository = serviceLineRepository;
            this.serviceLineGroupRepository = serviceLineGroupRepository;
            this.resolutionRepository = resolutionRepository;
            this.awardRepository = awardRepository;
            this.distintionsRepository = distintionsRepository;
            this.recognitionRepository = recognitionRepository;
        }

        [RequirePermissions(Permission.CreationGroupViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.CreationGroupCreator)]
        public override IActionResult Create()
        {            
            return base.Create();
        }

        [RequirePermissions(Permission.CreationGroupCreator)]
        public override IActionResult Create(CreationGroupInputViewModel inputViewModel)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("ImageFile");

                    inputViewModel.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(inputViewModel);

                    updateModelTask.Wait();
                }
            }

            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.CreationGroupEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.CreationGroupEditor)]
        public override IActionResult Edit(CreationGroupInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.CreationGroupRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.CreationGroupRemover)]
        public override IActionResult Delete(CreationGroupDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
             

        protected override void PreCreate(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.ResolutionId != null)
            {
                entity.Resolution = resolutionRepository.Get(modelInput.ResolutionId.Value);
                entity.ResolutionId = modelInput.ResolutionId;
            }

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "CreationGroup " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PreEdit(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (modelInput.ResolutionId != null)
            {
                if (modelInput.ResolutionId != entity.ResolutionId)
                {
                    entity.Resolution = resolutionRepository.Get(modelInput.ResolutionId.Value);
                    entity.ResolutionId = modelInput.ResolutionId;
                }
            }
            else
            {
                entity.Resolution = null;
                entity.ResolutionId = null;
            }

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.Identity.Image != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.Identity.Image != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);

                imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "CreationGroup " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void ConfigureCreate(CreationGroupInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateAvailableResolutions(modelInput);
        }

        protected override void ConfigureEdit(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateAvailableResolutions(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(CreationGroupInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateAvailableResolutions(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(CreationGroupInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateAvailableResolutions(modelInput);
        }

        protected override void PostDelete(CreationGroup entity)
        {
            base.PostDelete(entity);

            organizationIdentityRepository.Delete(entity.Identity, User.Identity.Name);

            if (entity.Identity.Image == null) return;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);
        }

        [Route("edit/{key}/members")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public IActionResult Members(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);
                var members = entity.ArtistBridges.Select(b => b.Artist).ToArray();
                var notMemberArtist = artistRepository.GetAll().ToArray();
                if (members.Count() != 0)
                {
                    notMemberArtist = notMemberArtist.Except(members).ToArray();
                }
                
                var resultModel = new CreationGroupInputViewModel ();
                
                resultModel.Import(entity);
                
                resultModel.NotMembers = notMemberArtist.Select(n => new SelectListItem { Value = n.Id.ToString(), Text = n.Identity.Name+" "+n.Identity.LastName }).ToArray();
                
                resultModel.Members = members.Select(item =>
                {
                    var model = new ArtistDisplayViewModel { 
                        CreationGroupMember =  new CreationGroupMemberDisplayViewModel
                        {
                            CreationGroupId = key,
                            Name = item.Identity.Name+" "+ item.Identity.LastName,
                            Alias = item.Identity.Alias,
                            ArtistId = item.Id,
                        }
                    };

                    model.Import(item);

                    return model;
                }).ToArray();

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/members/add")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMember(CreationGroupMembersInputViewModel modelInput)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelInput.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.ArtistBridges.Add(new CreationGroupArtistBridge { CreationGroupId = entity.Id, ArtistId = modelInput.ArtistId });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
            }
            else
            {
                var messages = new System.Collections.Generic.List<string>();

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

            return RedirectToAction("members", new { key = entity.Id });
        }

        private void PopulateSelectableManifestationGroups(ArtisticManifestationGroup group, List<ArtisticManifestation> selectedArtisticManifestations, Dictionary<ArtisticManifestationGroup, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>[]> groups)
        {
            var artisticManifestations = artisticManifestationRepository.FindBy(item => item.GroupId == group.Id).ToArray();

            if (artisticManifestations.Length > 0)
            {
                groups.Add(group, artisticManifestations.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = selectedArtisticManifestations.Any(entity => entity.Id == item.Id),
                    Description = null
                }).ToArray());
            }

            var children = artisticManifestationGroupRepository.FindBy(item => item.ParentId == group.Id);

            foreach (var child in children)
            {
                PopulateSelectableManifestationGroups(child, selectedArtisticManifestations, groups);
            }

        }

        [Route("edit/members/remove")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveMember(CreationGroupMemberDisplayViewModel modelDisplay)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.ArtistBridges.First(b => b.ArtistId == modelDisplay.ArtistId);

                entity.ArtistBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("members", new { key = entity.Id });
        }

        [Route("edit/{key}/contacts")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public IActionResult Contacts(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new CreationGroupContactsManageViewModel
                {
                    CreationGroup = new CreationGroupDisplayViewModel(),
                    PhoneContacts = entity.Identity.OrganizationPhoneContactItemBridges.Select(bridge => new CreationGroupPhoneContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Number = bridge.ContactItem.PhoneNumber,
                        CreationGroupId = entity.Id
                    }).ToList(),
                    EmailContacts = entity.Identity.OrganizationEmailContactItemBridges.Select(bridge => new CreationGroupEmailContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Address = bridge.ContactItem.EmailAddress,
                        CreationGroupId = entity.Id
                    }).ToList(),
                    SocialContacts = entity.Identity.OrganizationSocialContactItemBridges.Select(bridge => new CreationGroupSocialContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        SocialId = bridge.ContactItem.SocialId,
                        CreationGroupId = entity.Id,
                        Icon = GetIcon(bridge.ContactItem.SocialId)
                    }).ToList()
                };

                resultModel.CreationGroup.Import(entity);

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/contacts/phone/add")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhoneContact(CreationGroupPhoneContactInputViewModel modelInput)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelInput.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.OrganizationPhoneContactItemBridges.Add(new OrganizationPhoneContactItemBridge { OrganizationId = entity.IdentityId, ContactItem = new PhoneContactItem { Label = modelInput.Label, PhoneNumber = modelInput.Number } });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
            }
            else
            {
                var messages = new System.Collections.Generic.List<string>();

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

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/phone/delete")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePhoneContact(CreationGroupPhoneContactDisplayViewModel modelDisplay)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.OrganizationPhoneContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationPhoneContactItemBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/email/add")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmailContact(CreationGroupEmailContactInputViewModel modelInput)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelInput.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.OrganizationEmailContactItemBridges.Add(new OrganizationEmailContactItemBridge { OrganizationId = entity.IdentityId, ContactItem = new EmailContactItem { Label = modelInput.Label, EmailAddress = modelInput.Address } });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
            }
            else
            {
                var messages = new System.Collections.Generic.List<string>();

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

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/email/delete")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmailContact(CreationGroupEmailContactDisplayViewModel modelDisplay)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.OrganizationEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationEmailContactItemBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/social/add")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSocialContact(CreationGroupSocialContactInputViewModel modelInput)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelInput.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.OrganizationSocialContactItemBridges.Add(new OrganizationSocialContactItemBridge { OrganizationId = entity.IdentityId, ContactItem = new SocialContactItem { Label = modelInput.Label, SocialId = modelInput.SocialId } });

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

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/social/delete")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSocialContact(CreationGroupSocialContactDisplayViewModel modelDisplay)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.OrganizationSocialContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationSocialContactItemBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        private Microsoft.AspNetCore.Html.HtmlString GetIcon(string socialId)
        {
            switch(socialId)
            {
                case string x when x.Contains("facebook.com"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"social-icon mdi mdi-facebook\"></span>");
                case string x when x.Contains("twitter.com"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"social-icon mdi mdi-twitter\"></span>");
                case string x when x.Contains("instagram.com"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"social-icon mdi mdi-instagram\"></span>");
                case string x when x.Contains("linkedin.com"):
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"social-icon mdi mdi-linkedin\"></span>");
                default:
                    return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"social-icon mdi mdi-cloud-question\"></span>");
            }
        }

        private void PopulateAvailableResolutions(CreationGroupInputViewModel modelInput)
        {
            modelInput.Resolutions = resolutionRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = string.Format("{0} - {1} - {2}", entity.Number, entity.ReleaseAt.ToShortDateString(), entity.Title) }).ToArray();
        }

        #region Manifestations

        [HttpGet]
        [Route("edit/{key}/artisticmanifestations")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditArtisticManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var rootManisfestationGroups = artisticManifestationGroupRepository.FindBy(e => e.Parent == null).ToArray();

            var groups = new Dictionary<ArtisticManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]>();

            foreach (var group in rootManisfestationGroups)
            {
                PopulateArtisticManifestationSelectableGroups(group, entity.ArtisticManifestationBrigdes.Select(item => item.Manifestation).ToList(), groups);
            }

            var model = new GroupArtisticManisfestationsInputViewModel
            {
                UngroupManifestations = artisticManifestationRepository.FindBy(item => item.GroupId == null).Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ArtisticManifestationBrigdes.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
                SelectGroups = groups,
                Group = new CreationGroupDisplayViewModel()
            };

            model.Group.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/craftmanifestations")]
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditCraftManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var rootManisfestationGroups = craftManifestationGroupRepository.FindBy(e => e.Parent == null).ToArray();

            var groups = new Dictionary<CraftManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]>();

            foreach (var group in rootManisfestationGroups)
            {
                PopulateCraftManifestationSelectableGroups(group, entity.CraftManifestationBridges.Select(item => item.Manifestation).ToList(), groups);
            }

            var model = new GroupCraftManisfestationsInputViewModel
            {
                UngroupManifestations = craftManifestationRepository.FindBy(item => item.GroupId == null).Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.CraftManifestationBridges.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
                SelectGroups = groups,
                Group = new CreationGroupDisplayViewModel()
            };

            model.Group.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/designmanifestations")]
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditDesignManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var rootManisfestationGroups = designManifestationGroupRepository.FindBy(e => e.Parent == null).ToArray();

            var groups = new Dictionary<DesignManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]>();

            foreach (var group in rootManisfestationGroups)
            {
                PopulateDesignManifestationSelectableGroups(group, entity.DesignManifestationBridges.Select(item => item.Manifestation).ToList(), groups);
            }

            var model = new GroupDesignManisfestationsInputViewModel
            {
                UngroupManifestations = designManifestationRepository.FindBy(item => item.GroupId == null).Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.DesignManifestationBridges.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
                SelectGroups = groups,
                Group = new CreationGroupDisplayViewModel()
            };

            model.Group.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/servicelines")]
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditServiceLines(Guid key)
        {
            var entity = Repository.Get(key);

            var rootManisfestationGroups = serviceLineGroupRepository.FindBy(e => e.Parent == null).ToArray();

            var groups = new Dictionary<ServiceLineGroup, SelectionDescriptedItemInputViewModel<Guid>[]>();

            foreach (var group in rootManisfestationGroups)
            {
                PopulateServiceLineSelectableGroups(group, entity.ServiceLineBridges.Select(item => item.Line).ToList(), groups);
            }

            var model = new GrouptServiceLinesInputViewModel
            {
                UngroupManifestations = serviceLineRepository.FindBy(item => item.GroupId == null).Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ServiceLineBridges.Any(bridge => bridge.LineId == item.Id),
                    Description = item.Description
                }).ToArray(),
                SelectGroups = groups,
                Group = new CreationGroupDisplayViewModel()
            };

            model.Group.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/artisticmanifestations")]
        [RequirePermissions(Permission.ArtistEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditArtisticManifestations(Guid key, GroupArtisticManisfestationsInputViewModel modelInput)
        {
            CreationGroup entity = null;

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

            entity.ArtisticManifestationBrigdes.Clear();

            foreach (var item in modelInput.UngroupManifestations)
            {
                if (!item.Selected) continue;

                entity.ArtisticManifestationBrigdes.Add(new CreationGroupArtisticManifestationBridge
                {
                    GroupId = entity.Id,
                    ManifestationId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editartisticmanifestations", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/craftmanifestations")]
        [RequirePermissions(Permission.ArtistEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditCraftManifestations(Guid key, GroupCraftManisfestationsInputViewModel modelInput)
        {
            CreationGroup entity = null;

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

            entity.CraftManifestationBridges.Clear();

            foreach (var item in modelInput.UngroupManifestations)
            {
                if (!item.Selected) continue;

                entity.CraftManifestationBridges.Add(new CreationGroupCraftManifestationBridge
                {
                    GroupId = entity.Id,
                    ManifestationId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editcraftmanifestations", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/designmanifestations")]
        [RequirePermissions(Permission.ArtistEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditDesignManifestations(Guid key, GroupDesignManisfestationsInputViewModel modelInput)
        {
            CreationGroup entity = null;

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

            entity.DesignManifestationBridges.Clear();

            foreach (var item in modelInput.UngroupManifestations)
            {
                if (!item.Selected) continue;

                entity.DesignManifestationBridges.Add(new CreationGroupDesignManifestationBridge
                {
                    GroupId = entity.Id,
                    ManifestationId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editdesignmanifestations", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/servicelines")]
        [RequirePermissions(Permission.ArtistEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditServiceLines(Guid key, GroupServiceLinesInputViewModel modelInput)
        {
            CreationGroup entity = null;

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

            entity.ServiceLineBridges.Clear();

            foreach (var item in modelInput.UngroupManifestations)
            {
                if (!item.Selected) continue;

                entity.ServiceLineBridges.Add(new CreationGroupServiceLineBridge
                {
                    GroupId = entity.Id,
                    LineId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editservicelines", new { key = entity.Id });
        }

        private void PopulateArtisticManifestationSelectableGroups(ArtisticManifestationGroup group, List<ArtisticManifestation> selectedItems, Dictionary<ArtisticManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]> groups)
        {
            var manifestations = artisticManifestationRepository.FindBy(item => item.GroupId == group.Id).ToArray();

            if (manifestations.Length > 0)
            {
                groups.Add(group, manifestations.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = selectedItems.Any(entity => entity.Id == item.Id),
                    Description = item.Description
                }).ToArray());
            }

            var children = artisticManifestationGroupRepository.FindBy(item => item.ParentId == group.Id);

            foreach (var child in children)
            {
                PopulateArtisticManifestationSelectableGroups(child, selectedItems, groups);
            }
        }

        private void PopulateCraftManifestationSelectableGroups(CraftManifestationGroup group, List<CraftManifestation> selectedItems, Dictionary<CraftManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]> groups)
        {
            var manifestations = craftManifestationRepository.FindBy(item => item.GroupId == group.Id).ToArray();

            if (manifestations.Length > 0)
            {
                groups.Add(group, manifestations.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = selectedItems.Any(entity => entity.Id == item.Id),
                    Description = item.Description
                }).ToArray());
            }

            var children = craftManifestationGroupRepository.FindBy(item => item.ParentId == group.Id);

            foreach (var child in children)
            {
                PopulateCraftManifestationSelectableGroups(child, selectedItems, groups);
            }
        }

        private void PopulateDesignManifestationSelectableGroups(DesignManifestationGroup group, List<DesignManifestation> selectedItems, Dictionary<DesignManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]> groups)
        {
            var manifestations = designManifestationRepository.FindBy(item => item.GroupId == group.Id).ToArray();

            if (manifestations.Length > 0)
            {
                groups.Add(group, manifestations.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = selectedItems.Any(entity => entity.Id == item.Id),
                    Description = item.Description
                }).ToArray());
            }

            var children = designManifestationGroupRepository.FindBy(item => item.ParentId == group.Id);

            foreach (var child in children)
            {
                PopulateDesignManifestationSelectableGroups(child, selectedItems, groups);
            }
        }

        private void PopulateServiceLineSelectableGroups(ServiceLineGroup group, List<ServiceLine> selectedItems, Dictionary<ServiceLineGroup, SelectionDescriptedItemInputViewModel<Guid>[]> groups)
        {
            var manifestations = serviceLineRepository.FindBy(item => item.GroupId == group.Id).ToArray();

            if (manifestations.Length > 0)
            {
                groups.Add(group, manifestations.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = selectedItems.Any(entity => entity.Id == item.Id),
                    Description = item.Description
                }).ToArray());
            }

            var children = serviceLineGroupRepository.FindBy(item => item.ParentId == group.Id);

            foreach (var child in children)
            {
                PopulateServiceLineSelectableGroups(child, selectedItems, groups);
            }
        }

        #endregion

        #region Acknowledgments

        [HttpGet]
        [Route("edit/{key}/groupacknowledgments")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupAcknowledgments(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CreationGroupInputViewModel();

            

            model.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/groupRecognitions")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupRecognitions(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CreationGroupInputViewModel();

            PopulateRecognitions(entity, model.RecognitionsToSelect);

            model.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/groupDistintions")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupDistintions(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CreationGroupInputViewModel();

            PopulateDistintions(entity, model.DistintionsToSelect);

            model.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/groupacknowledgments")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupAcknowledgments(Guid key, CreationGroupInputViewModel modelInput)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(key);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return (ActionResult)GetDefaultActionResult();
            }

           

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("EditGroupAcknowledgments", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/groupRecognitions")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupRecognitions(Guid key, CreationGroupInputViewModel modelInput)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(key);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return (ActionResult)GetDefaultActionResult();
            }

            entity.CreationGroupRecognitionBridges.Clear();

            foreach (var item in modelInput.RecognitionsToSelect)
            {
                if (!item.Selected) continue;

                entity.CreationGroupRecognitionBridges.Add(new CreationGroupRecognitionBridge
                {
                    GroupId = entity.Id,
                    RecognitionId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("EditGroupRecognitions", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/groupDistintions")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupDistintions(Guid key, CreationGroupInputViewModel modelInput)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(key);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return (ActionResult)GetDefaultActionResult();
            }

            entity.CreationGroupDistinctionBridges.Clear();

            foreach (var item in modelInput.DistintionsToSelect)
            {
                if (!item.Selected) continue;

                entity.CreationGroupDistinctionBridges.Add(new CreationGroupDistinctionBridge
                {
                    GroupId = entity.Id,
                    DistinctionId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("EditGroupDistintions", new { key = entity.Id });
        }

        [Route("edit/{key}/CreationGroupAwards")]
        [RequirePermissions(Permission.ArtistEditor)]
        public IActionResult CreationGroupAwards(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new CreationGroupAwardManageViewModel
                {
                    GroupDisplay = new CreationGroupDisplayViewModel()
                };
                PopulateAwards(resultModel);
                resultModel.GroupDisplay.Import(entity);

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
        [RequirePermissions(Permission.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCreationGroupAwards(CreationGroupAwardManageViewModel modelInput)
        {
            CreationGroup entity = null;
            Award award = null;
            try
            {
                entity = Repository.Get(modelInput.GroupDisplay.Id);
                award = awardRepository.Get(modelInput.AwardId);

                modelInput.GroupDisplay.Import(entity);
                ;
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.CreationGroupAwards.Add(new CreationGroupAward { Group = entity, Award = award, GroupId = entity.Id, AwardId = award.Id, Context = modelInput.Context });

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

            return RedirectToAction("CreationGroupAwards", new { key = entity.Id });
        }

        [Route("edit/Awards/delete")]
        [RequirePermissions(Permission.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCreationGroupAwards(CreationGroupAwardDisplayViewModel modelDisplay)
        {
            CreationGroup entity = null;
            try
            {
                entity = Repository.Get(modelDisplay.GroupId);

            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                var groupAward = entity.CreationGroupAwards.First(a => a.Id == modelDisplay.Id);
                entity.CreationGroupAwards.Remove(groupAward);

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

            return RedirectToAction("CreationGroupAwards", new { key = entity.Id });
        }

        private void PopulateAwards(CreationGroupAwardManageViewModel modelInput)
        {
            var awads = awardRepository.GetAll();

            if (awads.Count() > 0)
            {
                modelInput.SelectAwards = awardRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name + " - " + item.Country.Name }).ToArray();
            }
        }

        private void PopulateAwards(Artist entity, List<SelectionDescriptedItemInputViewModel<Guid>> awardsForSelecting)
        {
            var awads = awardRepository.GetAll();


            if (awads.Count() > 0)
            {
                awardsForSelecting.AddRange(awads.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ArtistAwards.Any(a => a.AwardId == item.Id),
                    Description = item.Country.Name
                }));
            }

        }

        

        private void PopulateRecognitions(CreationGroup entity, List<SelectionDescriptedItemInputViewModel<Guid>> awardsForSelecting)
        {
            var recognitions = recognitionRepository.GetAll();

            if (recognitions.Count() > 0)
            {
                awardsForSelecting.AddRange(recognitions.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.CreationGroupRecognitionBridges.Any(a => a.RecognitionId == item.Id),
                    Description = String.IsNullOrWhiteSpace(item.Description) ? "Sin Descripción" : item.Description
                }));
            }

        }

        private void PopulateDistintions(CreationGroup entity, List<SelectionDescriptedItemInputViewModel<Guid>> awardsForSelecting)
        {
            var distintions = distintionsRepository.GetAll();

            if (distintions.Count() > 0)
            {
                awardsForSelecting.AddRange(distintions.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.CreationGroupDistinctionBridges.Any(a => a.DistinctionId == item.Id),
                    Description = String.IsNullOrWhiteSpace(item.Description) ? "Sin Descripción" : item.Description
                }));
            }

        }

        #endregion
    }
}
