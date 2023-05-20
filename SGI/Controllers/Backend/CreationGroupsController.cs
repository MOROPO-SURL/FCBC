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
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using MOROPO.FCBC.SGI.RegionConst;
using Microsoft.EntityFrameworkCore;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class CreationGroupsController : AbstractEntityManagementFilteredController<CreationGroup, Guid, CreationGroupInputViewModel, CreationGroupDisplayViewModel, CreationGroupFilterModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;
        private readonly IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository;
        private readonly IEntityRepository<Artist, Guid> artistRepository;
        private readonly IEntityRepository<CraftManifestation, Guid> craftManifestationRepository;
        private readonly IEntityRepository<ServiceLine, Guid> serviceLineRepository;
        private readonly IEntityRepository<Agreement, Guid> agreementRepository;
        private readonly IEntityRepository<Award, Guid> awardRepository;
        private readonly IEntityRepository<Recognition, Guid> recognitionRepository;
        private readonly IEntityRepository<Distinction, Guid> distintionsRepository;
        private readonly Managers.RelativeAttachmentFileManager attachmentFileManager;
        private readonly IEntityRepository<CreationGroupAttachment, Guid> attachmentRepository;
        private readonly IEntityRepository<State, Guid> stateRepository;
        private readonly IEntityRepository<Municipality, Guid> municipalityRepository;
        private readonly IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository;
        private readonly IEntityRepository<Critic, Guid> criticRepository;

        public CreationGroupsController(IEntityRepository<Critic, Guid> criticRepository, IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository, IEntityRepository<State, Guid> stateRepository, IEntityRepository<Municipality, Guid> municipalityRepository, IEntityRepository<CreationGroupAttachment, System.Guid> attachmentRepository, Managers.RelativeAttachmentFileManager attachmentFileManager, IEntityRepository<Artist, Guid> artistRepository, IEntityRepository<CreationGroup, Guid> repository, IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository, IEntityRepository<CraftManifestation, Guid> craftManifestationRepository, IEntityRepository<ServiceLine, Guid> serviceLineRepository, IEntityRepository<Award, Guid> awardRepository,
            IEntityRepository<Recognition, Guid> recognitionRepository, IEntityRepository<Distinction, Guid> distintionsRepository, IStringLocalizer<CreationGroupsController> localizer, IEntityRepository<Agreement, Guid> agreementRepository, ILogger<CreationGroupsController> logger, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, Guid> imageRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.artisticManifestationRepository = artisticManifestationRepository;
            this.artistRepository = artistRepository;
            this.craftManifestationRepository = craftManifestationRepository;
            this.serviceLineRepository = serviceLineRepository;
            this.agreementRepository = agreementRepository;
            this.awardRepository = awardRepository;
            this.distintionsRepository = distintionsRepository;
            this.recognitionRepository = recognitionRepository;
            this.attachmentFileManager = attachmentFileManager;
            this.attachmentRepository = attachmentRepository;
            this.stateRepository = stateRepository;
            this.municipalityRepository = municipalityRepository;
            this.municipalityRegionRepository = municipalityRegionRepository;
            this.criticRepository = criticRepository;
        }

        [RequirePermissions(Permission.CreationGroupViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ArtistViewer)]
        public override IActionResult Filter(CreationGroupFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequirePermissions(Permission.ArtistViewer)]
        protected override CreationGroupFilterModel GetFilterModel()
        {
            var filterModel = base.GetFilterModel();

            if (filterModel == null)
            {
                filterModel = new CreationGroupFilterModel();
            }

            filterModel.States = new List<SelectListItem>
            {
                new SelectListItem("Cualquiera", "0")
            };

            filterModel.Manisfestations = new List<SelectListItem>
            {
                new SelectListItem("Cualquiera", "0")
            };

            PopulateDropDownStateWithMunicipality(filterModel);
            PopulateManifestations(filterModel);

            return filterModel;
        }

        protected override string GetFilterSessionID()
        {
            return "creation-group-management-filter";
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
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    inputModel.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(inputModel);

                    updateModelTask.Wait();
                }
            }

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

        [HttpGet("PopulateStateToMunicipality/{stateId}")]
        public IActionResult PopulateStateToMunicipality(string stateId)
        {
            var stateIdParse = Guid.Parse(stateId);

            var municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == stateIdParse).ToList();
            return Ok(municipalities);
        }

        [HttpGet("edit/PopulateStateToMunicipalityEdit/{stateId}")]
        public IActionResult PopulateStateToMunicipalityEdit(string stateId)
        {
            var stateIdParse = Guid.Parse(stateId);

            var municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == stateIdParse).ToList();
            return Ok(municipalities);
        }

        private void PopulateWithCountries(CreationGroupInputViewModel modelInput)
        {
            modelInput.States = stateRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Name == RegionInitConstant.CMG_STATE_NAME }).OrderByDescending(en => en.Selected).ToArray();

            modelInput.Municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == RegionInitConstant.MY_OWN_SATE).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
            //modelInput.States = stateRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();            
        }

        private void PopulateWithCountriesEdit(CreationGroupInputViewModel modelInput)
        {           
            modelInput.StateId = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId).StateId;

            modelInput.Municipalities = municipalityRepository.GetAll().Where(s => s.StateId == modelInput.StateId).Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.MunicipalityId }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.StateId }).ToArray();
        }


        protected override async void PreCreate(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {


            // TODO En ocasiones queda null, hay que buscar inicailizar las regiones en este punto
            var regions = municipalityRegionRepository.GetAll();
            var region = await regions.FirstOrDefaultAsync(b => b.MunicipalityId == modelInput.MunicipalityId);
            

            var postalContact = new PostalContactItem
            {
                Address = modelInput.AddressPostal,
                RegionId = region.Id,
                Region = region
            };

            var organizationPostalContactItemBridge = new OrganizationPostalContactItemBridge
            {
                OrganizationId = modelInput.Export().Identity.Id,
                Organization = modelInput.Export().Identity,
                ContactItemId = postalContact.Id,
                ContactItem = postalContact
            };

            entity.Identity.OrganizationPostalContactItemBridges.Add(organizationPostalContactItemBridge);

            var getRegionRegister = entity.Identity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.Region;

            modelInput.Municipality = ((MunicipalityRegion)getRegionRegister).Municipality;
            modelInput.State = modelInput.Municipality.State;

            if (modelInput.AgreementId != null)
            {
                entity.Agreement = agreementRepository.Get(modelInput.AgreementId.Value);
                entity.AgreementId = modelInput.AgreementId;
            }

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "CreationGroup " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }

            base.PreCreate(entity, modelInput);
        }

        protected override void PreEdit(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            var contact = entity.Identity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem;

            contact.Address = modelInput.AddressPostal;

            var region = municipalityRegionRepository.GetAll().FirstOrDefault(p => p.MunicipalityId == modelInput.MunicipalityId);

            var municipality = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId);

            modelInput.StateId = municipality.StateId;

            if (region.Id != entity.Identity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId)
            {
                entity.Identity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.Region = region;
                entity.Identity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId = region.Id;

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
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigureEdit(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateAvailableResolutions(modelInput);
            PopulateWithCountriesEdit(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(CreationGroupInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateAvailableResolutions(modelInput);
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            PopulateAvailableResolutions(modelInput);
            PopulateWithCountriesEdit(modelInput);
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
        public IActionResult Members(Guid key)
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
                
                var resultModel = new CreationGroupMemberManageViewModel
                {
                    Group = new CreationGroupDisplayViewModel()
                };
                
                resultModel.Group.Import(entity);
                
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
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

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
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.ArtistBridges.Add(new CreationGroupArtistBridge { CreationGroupId = entity.Id, ArtistId = modelInput.ArtistId });

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

            return RedirectToAction("members", new { key = entity.Id });
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
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.ArtistBridges.First(b => b.ArtistId == modelDisplay.ArtistId);

                entity.ArtistBridges.Remove(bridge);

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

            return RedirectToAction("members", new { key = entity.Id });
        }

        [Route("edit/{key}/contacts")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public IActionResult Contacts(Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new CreationGroupContactsManageViewModel
                {
                    Group = new CreationGroupDisplayViewModel(),
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

                resultModel.Group.Import(entity);

                return View(resultModel);
            }
            catch (Exception e)
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
                var messages = new List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        messages.Add(error.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, "Agregar Contacto (Teléfono)", messages.ToArray());
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
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.OrganizationPhoneContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationPhoneContactItemBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (Exception e)
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
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.OrganizationEmailContactItemBridges.Add(new OrganizationEmailContactItemBridge { OrganizationId = entity.IdentityId, ContactItem = new EmailContactItem { Label = modelInput.Label, EmailAddress = modelInput.Address } });

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

                SetDefaultNotification(NotificationType.Danger, "Agregar Contacto (Correo Electrónico)", messages.ToArray());
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
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.OrganizationEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationEmailContactItemBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (Exception e)
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
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.OrganizationSocialContactItemBridges.Add(new OrganizationSocialContactItemBridge { OrganizationId = entity.IdentityId, ContactItem = new SocialContactItem { Label = modelInput.Label, SocialId = modelInput.SocialId } });

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

                SetDefaultNotification(NotificationType.Danger, "Agregar Contacto (Redes Sociales)", messages.ToArray());
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
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.OrganizationSocialContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationSocialContactItemBridges.Remove(bridge);

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
            modelInput.AvailableAgreements = agreementRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = string.Format("{0} - {1} - {2}", entity.Number, entity.ReleaseAt.ToShortDateString(), entity.Title) }).ToArray();
        }

        private void PopulateDropDownStateWithMunicipality(CreationGroupFilterModel filter)
        {

            var states = stateRepository.GetAll();
            var municipalities = municipalityRepository.GetAll();
            var stateGroups = states.Select(s => new SelectListGroup { Name = s.Name });

            foreach (var state in states)
            {
                var stateGroup = stateGroups.FirstOrDefault(s => s.Name == state.Name);


                foreach (var municipality in municipalities)
                {
                    if (municipality.StateId == state.Id)
                    {
                        filter.States.Add(new SelectListItem { Value = municipality.Id.ToString(), Text = municipality.Name, Group = stateGroup });
                    }
                }
            }
        }

        private void PopulateManifestations(CreationGroupFilterModel filter)
        {
            var artistManifestations = artisticManifestationRepository.GetAll();
            var ArtistGroup = new SelectListGroup { Name = "Artes Visuales" };

            var craftManifestations = craftManifestationRepository.GetAll();
            var CraftGroup = new SelectListGroup { Name = "Artes Aplicadas" };

            var serviceManifestations = serviceLineRepository.GetAll();
            var ServiceGroup = new SelectListGroup { Name = "Servicios" };

            foreach (var artisticManifestation in artistManifestations)
            {
                filter.Manisfestations.Add(new SelectListItem { Value = artisticManifestation.Id.ToString(), Text = artisticManifestation.Name, Group = ArtistGroup });
            }

            foreach (var craftManifestation in craftManifestations)
            {
                filter.Manisfestations.Add(new SelectListItem { Value = craftManifestation.Id.ToString(), Text = craftManifestation.Name, Group = CraftGroup });
            }

            foreach (var serviceManifestation in serviceManifestations)
            {
                filter.Manisfestations.Add(new SelectListItem { Value = serviceManifestation.Id.ToString(), Text = serviceManifestation.Name, Group = ServiceGroup });
            }


        }


        #region Manifestations

        [HttpGet]
        [Route("edit/{key}/artisticmanifestations")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditArtisticManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new GroupArtisticManisfestationsInputViewModel
            {
                Manifestations = artisticManifestationRepository.GetAll().ToArray().Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ArtisticManifestationBrigdes.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
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

            var model = new GroupCraftManisfestationsInputViewModel
            {
                Manifestations = craftManifestationRepository.GetAll().Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.CraftManifestationBridges.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
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

            var model = new GrouptServiceLinesInputViewModel
            {
                Manifestations = serviceLineRepository.GetAll().ToArray().Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ServiceLineBridges.Any(bridge => bridge.LineId == item.Id),
                    Description = item.Description
                }).ToArray(),
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

            foreach (var item in modelInput.Manifestations)
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

            foreach (var item in modelInput.Manifestations)
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

            foreach (var item in modelInput.Manifestations)
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

        #region Criterion
        //[Route("edit/{key}/EditGroupCriterion")]
        //[RequirePermissions(Permission.CreationGroupEditor)]
        //public IActionResult EditGroupCriterion(System.Guid key)
        //{
        //    try
        //    {
        //        var entity = Repository.Get(key);

        //        var resultModel = new GroupCriterionManageViewModel
        //        {
        //            GroupDisplay = new CreationGroupDisplayViewModel()
        //        };
        //        PopulateCritics(resultModel);
        //        resultModel.GroupDisplay.Import(entity);

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
        //[RequirePermissions(Permission.CreationGroupEditor)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddGroupCriterion(GroupCriterionManageViewModel modelInput)
        //{
        //    CreationGroup entity = null;
        //    Critic critic = null;
        //    try
        //    {
        //        entity = Repository.Get(modelInput.GroupDisplay.Id);
        //        critic = criticRepository.Get(modelInput.CriticId);

        //        modelInput.GroupDisplay.Import(entity);
        //    }
        //    catch { }

        //    if (entity == null)
        //    {
        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

        //        return GetDefaultActionResult();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        entity.GroupAssessmentBridges.Add(new GroupAssessmentBridge { CreationGroup = entity, Critic = critic, GroupId = entity.Id, CriticId = critic.Id, Criterion = modelInput.Criterion, DateOfAssessment = modelInput.DateOfCriterion });

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

        //    return RedirectToAction("EditGroupCriterion", new { key = entity.Id });
        //}

        //[Route("edit/Criterion/update")]
        //[RequirePermissions(Permission.CreationGroupEditor)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult UpdateGroupCriterion(GroupAssessmentBridgeDisplayViewModel modelInput)
        //{
        //    CreationGroup entity = null;
        //    Critic critic = null;
        //    try
        //    {
        //        entity = Repository.Get(modelInput.GroupId);
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
        //        var groupAssessmentBridge = entity.GroupAssessmentBridges.FirstOrDefault(a => a.GroupId == modelInput.GroupId && a.CriticId == modelInput.CriticId);
        //        entity.GroupAssessmentBridges.Remove(groupAssessmentBridge);

        //        entity.GroupAssessmentBridges.Add(new GroupAssessmentBridge { CreationGroup = entity, Critic = critic, GroupId = entity.Id, CriticId = critic.Id, Criterion = modelInput.Criterion, DateOfAssessment = modelInput.DateOfAssessment });

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

        //    return RedirectToAction("EditGroupCriterion", new { key = entity.Id });
        //}

        //[Route("edit/Criterion/delete")]
        //[RequirePermissions(Permission.CreationGroupEditor)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteGroupCriterion(GroupCriterionDisplayViewModel modelDisplay)
        //{
        //    CreationGroup entity = null;
        //    try
        //    {
        //        entity = Repository.Get(modelDisplay.GroupId);

        //    }
        //    catch { }

        //    if (entity == null)
        //    {
        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

        //        return GetDefaultActionResult();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var groupCriterion = entity.GroupAssessmentBridges.First(a => a.GroupId == modelDisplay.GroupId && a.CriticId == modelDisplay.CriticId);
        //        entity.GroupAssessmentBridges.Remove(groupCriterion);

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

        //    return RedirectToAction("EditGroupCriterion", new { key = entity.Id });
        //}

        #endregion

        private void PopulateCritics(GroupCriterionManageViewModel modelInput)
        {
            var awads = criticRepository.GetAll();

            if (awads.Count() > 0)
            {
                modelInput.SelectCritics = criticRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Identity.Name + " " + item.Identity.LastName, Selected = item.Id == modelInput.CriticId }).ToArray();
            }
        }

        private void PopulateCriticsEditModal(GroupCriterionManageViewModel modelInput)
        {
            var awads = criticRepository.GetAll();

            if (awads.Count() > 0)
            {
                modelInput.SelectCritics = criticRepository.GetAll().Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Identity.Name + " " + item.Identity.LastName }).ToArray();
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
        [Route("edit/{key}/recognitions")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupRecognitions(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CreationGroupRecognitionManageViewModel
            {
                Group = new CreationGroupDisplayViewModel()
            };

            PopulateRecognitions(entity, model.RecognitionsToSelect);

            model.Group.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/distintions")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupDistintions(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CreationGroupDistintionManageViewModel
            {
                Group = new CreationGroupDisplayViewModel()
            };

            PopulateDistintions(entity, model.DistintionsToSelect);

            model.Group.Import(entity);

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
        [Route("edit/{key}/recognitions")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupRecognitions(Guid key, List<SelectionDescriptedItemInputViewModel<Guid>> modelInput)
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

            foreach (var item in modelInput)
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

            return RedirectToAction("editgrouprecognitions", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/distintions")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditGroupDistintions(Guid key, List<SelectionDescriptedItemInputViewModel<Guid>> modelInput)
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

            foreach (var item in modelInput)
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

            return RedirectToAction("editgroupdistintions", new { key = entity.Id });
        }

        [Route("edit/{key}/awards")]
        [RequirePermissions(Permission.ArtistEditor)]
        public IActionResult CreationGroupAwards(Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new CreationGroupAwardManageViewModel
                {
                    Group = new CreationGroupDisplayViewModel()
                };
                PopulateAwards(resultModel);
                resultModel.Group.Import(entity);

                return View(resultModel);
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }


        [Route("edit/awards/add")]
        [RequirePermissions(Permission.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCreationGroupAwards(CreationGroupAwardManageViewModel modelInput)
        {
            CreationGroup entity = null;
            Award award = null;
            try
            {
                entity = Repository.Get(modelInput.Group.Id);
                award = awardRepository.Get(modelInput.AwardId);

                modelInput.Group.Import(entity);
                ;
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.CreationGroupAwards.Add(new CreationGroupAward { Group = entity, Award = award, GroupId = entity.Id, AwardId = award.Id, Context = modelInput.Context });

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

            return RedirectToAction("creationgroupawards", new { key = entity.Id });
        }

        [Route("edit/awards/delete")]
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
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                var groupAward = entity.CreationGroupAwards.First(a => a.Id == modelDisplay.Id);
                entity.CreationGroupAwards.Remove(groupAward);

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

            return RedirectToAction("creationgroupawards", new { key = entity.Id });
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

        #region Attachments

        [HttpGet]
        [Route("edit/{key}/attachments")]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public ActionResult EditAttachments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new CreationGroupAttachmentsDisplayViewModel
            {
                CreationGroupDisplay = new CreationGroupDisplayViewModel(),
                Attachments = entity.Attachments.Select(item =>
                {
                    var result = new AttachmentDisplayViewModel();

                    result.Import(item);

                    return result;
                }).ToArray()
            };

            model.CreationGroupDisplay.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/attachments/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.CreationGroupEditor)]
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

                            var attachment = new CreationGroupAttachment
                            {
                                Name = model.Name,
                                IsPublic = model.IsPublic,
                                Description = model.Description,
                                ContentType = model.File.ContentType,
                                Length = model.File.Length,
                                GroupId = entity.Id
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
        [RequirePermissions(Permission.CreationGroupEditor)]
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
        [Route("edit/{key}/attachments/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.CreationGroupEditor)]
        public IActionResult EditAttachment(System.Guid key, AttachmentInputViewModel modelInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CreationGroupAttachment val = attachmentRepository.Get(modelInput.Id);
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

    }
}
