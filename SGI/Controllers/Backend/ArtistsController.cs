using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.FCBC.SGI.RegionConst;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SmartSolucionesCuba.SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("institution")]
    public class ArtistsController : AbstractEntityManagementFilteredController<Artist, Guid, ArtistInputViewModel, ArtistDisplayViewModel, ArtistFilterModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<PersonIdentity, Guid> identityRepository;
        private readonly IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository;
        private readonly IEntityRepository<State, Guid> stateRepository;
        private readonly IEntityRepository<Municipality, Guid> municipalityRepository;
        private readonly IEntityRepository<Association, Guid> associationRepository;
        private readonly IEntityRepository<Resolution, Guid> resolutionRepository;
        private readonly IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository;
        private readonly IEntityRepository<ArtisticManifestationGroup, Guid> artisticManifestationGroupRepository;
        private readonly IEntityRepository<CraftManifestation, Guid> craftManifestationRepository;
        private readonly IEntityRepository<CraftManifestationGroup, Guid> craftManifestationGroupRepository;
        private readonly IEntityRepository<DesignManifestation, Guid> designManifestationRepository;
        private readonly IEntityRepository<DesignManifestationGroup, Guid> designManifestationGroupRepository;
        private readonly IEntityRepository<ServiceLine, Guid> serviceLineRepository;
        private readonly IEntityRepository<ServiceLineGroup, Guid> serviceLineGroupRepository;
        private readonly IEntityRepository<Award, Guid> awardRepository;
        private readonly IEntityRepository<Recognition, Guid> recognitionRepository;

        public ArtistsController(
            IEntityRepository<ServiceLineGroup, Guid> serviceLineGroupRepository,
            IEntityRepository<ServiceLine, Guid> serviceLineRepository,
            IEntityRepository<DesignManifestationGroup, Guid> designManifestationGroupRepository,
            IEntityRepository<DesignManifestation, Guid> designManifestationRepository,
            IEntityRepository<CraftManifestationGroup, Guid> craftManifestationGroupRepository,
            IEntityRepository<CraftManifestation, Guid> craftManifestationRepository,
            IEntityRepository<PersonIdentity, Guid> identityRepository, 
            IEntityRepository<ArtisticManifestationGroup, Guid> artisticManifestationGroupRepository, 
            IEntityRepository<Image, Guid> imageRepository, 
            IImageFileManagerFacade imageFileManagerFacade,
            IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository, 
            IEntityRepository<State, Guid> stateRepository, IEntityRepository<Municipality, Guid> municipalityRepository, 
            IEntityRepository<Artist, Guid> repository, 
            IEntityRepository<Association, Guid> associationRepository, 
            IEntityRepository<Resolution, Guid> resolutionRepository, 
            IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository,
            IEntityRepository<Award, Guid> awardRepository,
            IEntityRepository<Recognition, Guid> recognitionRepository,
            IStringLocalizer<ArtistsController> localizer, 
            ILogger<ArtistsController> logger) : base(repository, localizer, logger)
        {
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.identityRepository = identityRepository;
            this.municipalityRegionRepository = municipalityRegionRepository;
            this.stateRepository = stateRepository;
            this.municipalityRepository = municipalityRepository;
            this.associationRepository = associationRepository;
            this.resolutionRepository = resolutionRepository;
            this.artisticManifestationRepository = artisticManifestationRepository;
            this.artisticManifestationGroupRepository = artisticManifestationGroupRepository;
            this.craftManifestationRepository = craftManifestationRepository;
            this.craftManifestationGroupRepository = craftManifestationGroupRepository;
            this.designManifestationRepository = designManifestationRepository;
            this.designManifestationGroupRepository = designManifestationGroupRepository;
            this.serviceLineRepository = serviceLineRepository;
            this.serviceLineGroupRepository = serviceLineGroupRepository;
            this.awardRepository = awardRepository;
            this.recognitionRepository = recognitionRepository;
        }

        [RequiresRoleBasePermission(Roles.ArtistViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ArtistViewer)]
        public override IActionResult Filter(ArtistFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "artist-management-filter";
        }

        [RequiresRoleBasePermission(Roles.ArtistCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ArtistCreator)]
        public override IActionResult Create(ArtistInputViewModel inputViewModel)
        {
            if (inputViewModel.AssociationId != null)
            {
                inputViewModel.Context = "Association|" + inputViewModel.AssociationId.ToString();
            }

            if (inputViewModel.ResolutionId != null)
            {
                inputViewModel.Context = "Resolution|" + inputViewModel.ResolutionId.ToString();
            }

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

        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public override IActionResult Edit(ArtistInputViewModel inputViewModel)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");            

            if (inputViewModel.AssociationId != null)
            {
                inputViewModel.Context = "Association|" + inputViewModel.AssociationId.ToString();
            }

            if (inputViewModel.ResolutionId != null)
            {
                inputViewModel.Context = "Resolution|" + inputViewModel.ResolutionId.ToString();
            }

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    inputViewModel.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(inputViewModel);

                    updateModelTask.Wait();
                }
            }
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ArtistRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ArtistRemover)]
        public override IActionResult Delete(ArtistDisplayViewModel displayViewModel)
        {
            return base.Delete(displayViewModel);
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

        private void PopulateWithCountries(ArtistInputViewModel modelInput)
        {
            modelInput.States = stateRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Name == RegionInitConstant.CMG_STATE_NAME }).OrderByDescending(en => en.Selected).ToArray();

            modelInput.Municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == RegionInitConstant.MY_OWN_SATE).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        private void PopulateWithAssociation(ArtistInputViewModel modelInput)
        {
            modelInput.Associations = associationRepository.GetAll().Select( entity => new SelectListItem { Value= entity.Id.ToString(), Text = entity.Identity.Name}).ToArray();
        }

        private void PopulateWithAssociationEdit(ArtistInputViewModel modelInput)
        {
            bool contextHasElemet = false;
            if (modelInput.Context != null && modelInput.Context.Contains("Association"))
            {
                contextHasElemet = true;
            }
            modelInput.Associations = associationRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Identity.Name, Selected = contextHasElemet }).ToArray();
        }

        private void PopulateWithResolution(ArtistInputViewModel modelInput)
        {
            modelInput.Resolutions = resolutionRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = string.Format("{0} - {1} - {2}", entity.Number, entity.ReleaseAt.ToShortDateString(), entity.Title) }).ToArray();
        }

        private void PopulateWithResolutionEdit(ArtistInputViewModel modelInput)
        {
            bool contextHasElemet = false;
            
            if (modelInput.Context != null && modelInput.Context.Contains("Resolution"))
            {
                contextHasElemet = true;
            }

            modelInput.Resolutions = resolutionRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = string.Format("{0} - {1} - {2}", entity.Number, entity.ReleaseAt.ToShortDateString(), entity.Title), Selected = contextHasElemet }).ToArray();
        }

        private void PopulateWithCountriesEdit(ArtistInputViewModel modelInput)
        {
            modelInput.StateId = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId).StateId;

            modelInput.Municipalities = municipalityRepository.GetAll().Where(s => s.StateId == modelInput.StateId).Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.MunicipalityId }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.StateId }).ToArray();
        }

        protected override void PreCreate(Artist entity, ArtistInputViewModel inputViewModel)
        {
            base.PreCreate(entity, inputViewModel);

            var region = municipalityRegionRepository.FindOneBy(r => r.MunicipalityId == inputViewModel.MunicipalityId);

            var postalContact = new PostalContactItem
            {
                Address = inputViewModel.AddressPostal,
                RegionId = region.Id,
                Region = region
            };

            var personPostalContactItemBridge = new PersonPostalContactItemBridge
            {
                PersonId = inputViewModel.Export().Identity.Id,
                Person = inputViewModel.Export().Identity,
                ContactItemId = postalContact.Id,
                ContactItem = postalContact
            };

            entity.Identity.PersonPostalContactItemBridges.Add(personPostalContactItemBridge);

            var getRegionRegister = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            
            inputViewModel.Municipality = ((MunicipalityRegion)getRegionRegister).Municipality;
            inputViewModel.State = inputViewModel.Municipality.State;

            if (inputViewModel.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Artist " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PreEdit(Artist entity, ArtistInputViewModel inputViewModel)
        {
           
            base.PreEdit(entity, inputViewModel);

            var contact = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem;
            
            contact.Address = inputViewModel.AddressPostal;
            
            var region = municipalityRegionRepository.GetAll().FirstOrDefault(p => p.MunicipalityId == inputViewModel.MunicipalityId);
            
            var municipality = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == inputViewModel.MunicipalityId);
            
            inputViewModel.StateId = municipality.StateId;
            
            if (region.Id != entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId)
            {
                entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region = region;
                entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId = region.Id;

            }            

            if (inputViewModel.ImageFile == null)
            {
                if (inputViewModel.ResetImage && entity.Identity.Image != null)
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
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Artist " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PostDelete(Artist entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.Identity, User.Identity.Name);

            if (entity.Identity.Image == null) return;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);
        }

        protected override void ConfigureCreate(ArtistInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
            PopulateWithAssociation(modelInput);
            PopulateWithResolution(modelInput);

            if (modelInput.AssociationId != null)
            {
                modelInput.Context = "Association" + modelInput.AssociationId.ToString();
            }

            if (modelInput.AssociationId != null)
            {
                modelInput.Context = "Resolution" + modelInput.ResolutionId.ToString();
            }
        }

        protected override void ConfigurePostFailCreateValidation(ArtistInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigureEdit(Artist entity, ArtistInputViewModel modelInput)
        {
            PopulateWithCountriesEdit(modelInput);
            PopulateWithAssociationEdit(modelInput);
            PopulateWithResolutionEdit(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ArtistInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        [Route("edit/{key}/contacts")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public IActionResult Contacts(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ArtistContactsManageViewModel
                {
                    Artist = new ArtistDisplayViewModel(),
                    PhoneContacts = entity.Identity.PersonPhoneContactItemBridges.Select(bridge => new ArtistPhoneContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Number = bridge.ContactItem.PhoneNumber,
                        ArtistId = entity.Id
                    }).ToList(),
                    EmailContacts = entity.Identity.PersonEmailContactItemBridges.Select(bridge => new ArtistEmailContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Address = bridge.ContactItem.EmailAddress,
                        ArtistId = entity.Id
                    }).ToList(),
                    SocialContacts = entity.Identity.PersonSocialContactItemBridges.Select(bridge => new ArtistSocialContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        SocialId = bridge.ContactItem.SocialId,
                        ArtistId = entity.Id,
                        Icon = GetIcon(bridge.ContactItem.SocialId)
                    }).ToList()
                };

                resultModel.Artist.Import(entity);

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("artists/{key}/print")]
        [RequiresRoleBasePermission(Roles.ArtistViewer)]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public IActionResult Print(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var region = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
                var municipality = ((MunicipalityRegion)region).Municipality;
                var state = municipality.State;

                var resultModel = new ArtistContactsManageViewModel
                {
                    Artist = new ArtistDisplayViewModel
                    {
                        ImageId = (Guid)entity.Identity.ImageId,
                        ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null,
                        Name = entity.Identity.Name,
                        LastName = entity.Identity.LastName,
                        Municipality = municipality,
                        State = municipality.State,
                        CitizenId = entity.CitizenId,
                        Workplace = entity.Workplace,
                        SchoolLevel = entity.SchoolLevel,
                        SpecificEducation = entity.SpecificEducation,
                        AdmitionMode = entity.AdmitionMode,
                        AdmitionModeContext = entity.AdmitionModeContext,
                        ArtistManifestationBridges = entity.ArtisticManifestationBridges,
                        About = entity.Identity.About,
                        EmailContactItems = entity.Identity.PersonEmailContactItemBridges.Select(b => b.ContactItem).ToArray(),
                        PhoneContactItems = entity.Identity.PersonPhoneContactItemBridges.Select(b => b.ContactItem).ToArray(),
                        AddressPostal = entity.Identity.PersonPostalContactItemBridges.Select(b => b.ContactItem.Address).First(),
                    },
                    PhoneContacts = entity.Identity.PersonPhoneContactItemBridges.Select(bridge => new ArtistPhoneContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Number = bridge.ContactItem.PhoneNumber,
                        ArtistId = entity.Id
                    }).ToList(),
                    EmailContacts = entity.Identity.PersonEmailContactItemBridges.Select(bridge => new ArtistEmailContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Address = bridge.ContactItem.EmailAddress,
                        ArtistId = entity.Id
                    }).ToList()
                };

                return new Rotativa.AspNetCore.ViewAsPdf("Print", resultModel)
                {
                    FileName = $"Datos del Artista {resultModel.Artist.Name} - { resultModel.Artist.LastName}.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4
                };
                
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/contacts/phone/add")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhoneContact(ArtistPhoneContactInputViewModel modelInput)
        {
            Artist entity = null;

            try
            {
                entity = Repository.Get(modelInput.ArtistId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.PersonPhoneContactItemBridges.Add(new PersonPhoneContactItemBridge { PersonId = entity.IdentityId, ContactItem = new PhoneContactItem { Label = modelInput.Label, PhoneNumber = modelInput.Number } });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
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

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/phone/delete")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePhoneContact(ArtistPhoneContactDisplayViewModel modelDisplay)
        {
            Artist entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.ArtistId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.PersonPhoneContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.PersonPhoneContactItemBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/email/add")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmailContact(ArtistEmailContactInputViewModel modelInput)
        {
            Artist entity = null;

            try
            {
                entity = Repository.Get(modelInput.ArtistId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.PersonEmailContactItemBridges.Add(new PersonEmailContactItemBridge { PersonId = entity.IdentityId, ContactItem = new EmailContactItem { Label = modelInput.Label, EmailAddress = modelInput.Address } });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
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

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/email/delete")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmailContact(ArtistEmailContactDisplayViewModel modelDisplay)
        {
            Artist entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.ArtistId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.PersonEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.PersonEmailContactItemBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        protected override void ValidateCreate(ArtistInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            PopulateWithAssociation(modelInput);
            PopulateWithResolution(modelInput);

            switch (modelInput.AdmitionModel)
            {
                case AdmitionMode.Membership: 
                        if(modelInput.AssociationId == null)
                         ModelState.AddModelError("AssociationId", "RequiredValidationErrorMessage");break;
                case AdmitionMode.Resolution:
                        if (modelInput.ResolutionId == null)
                         ModelState.AddModelError("ResolutionId", "RequiredValidationErrorMessage"); break;
            }
            
        }

        protected override void PopulateViewModelDisplayExtras(ArtistDisplayViewModel modelDisplay, Artist entity)
        {
            if (modelDisplay.AdmitionModeContext != null)
            {
                if (modelDisplay.AdmitionModeContext.Contains("Association|"))
                {
                    var resultSplit = modelDisplay.AdmitionModeContext.Split("|");                    
                    var association = associationRepository.Get(Guid.Parse(resultSplit[1]));
                    modelDisplay.Association = new AssociationDisplayViewModel 
                    { 
                         Id = association.Id,
                         OrganizationIdentityName = association.Identity.Name
                    };
                    modelDisplay.AdmitionModeContext = "Association|" + modelDisplay.AssociationId.ToString();
                }

                if (modelDisplay.AdmitionModeContext.Contains("Resolution|"))
                {
                    var resultSplit = modelDisplay.AdmitionModeContext.Split("|");
                    var resolution = resolutionRepository.Get(Guid.Parse(resultSplit[1]));
                    modelDisplay.Resolution = new ResolutionDisplayViewModel
                    {
                        Id = resolution.Id,
                        NumberResolution = resolution.Number,
                        Title = resolution.Title,
                        Description = resolution.Description
                    } ;
                    modelDisplay.AdmitionModeContext = "Resolution|" + modelDisplay.ResolutionId.ToString();
                }
            }

            

            base.PopulateViewModelDisplayExtras(modelDisplay, entity);
        }

        protected override void ValidateEdit(ArtistInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);

            PopulateWithResolutionEdit(modelInput);
            PopulateWithAssociationEdit(modelInput);

            switch (modelInput.AdmitionModel)
            {
                case AdmitionMode.Membership:
                    if (modelInput.AssociationId == null)
                        ModelState.AddModelError("AssociationId", "RequiredValidationErrorMessage"); break;
                case AdmitionMode.Resolution:
                    if (modelInput.ResolutionId == null)
                        ModelState.AddModelError("ResolutionId", "RequiredValidationErrorMessage"); break;
            }

        }

        #region Manifestations

        [HttpGet]
        [Route("edit/{key}/artisticmanifestations")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public ActionResult EditArtisticManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var rootManisfestationGroups = artisticManifestationGroupRepository.FindBy(e => e.Parent == null).ToArray();

            var groups = new Dictionary<ArtisticManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]>();

            foreach (var group in rootManisfestationGroups)
            {
                PopulateArtisticManifestationSelectableGroups(group, entity.ArtisticManifestationBridges.Select(item => item.Manifestation).ToList(), groups);
            }

            var model = new ArtistArtisticManisfestationsInputViewModel
            {
                UngroupManifestations = artisticManifestationRepository.FindBy(item => item.GroupId == null).Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ArtisticManifestationBridges.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
                SelectGroups = groups,
                Artist = new ArtistDisplayViewModel()
            };

            model.Artist.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/craftmanifestations")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public ActionResult EditCraftManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var rootManisfestationGroups = craftManifestationGroupRepository.FindBy(e => e.Parent == null).ToArray();

            var groups = new Dictionary<CraftManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]>();

            foreach (var group in rootManisfestationGroups)
            {
                PopulateCraftManifestationSelectableGroups(group, entity.CraftManifestationBridges.Select(item => item.Manifestation).ToList(), groups);
            }

            var model = new ArtistCraftManisfestationsInputViewModel
            {
                UngroupManifestations = craftManifestationRepository.FindBy(item => item.GroupId == null).Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.CraftManifestationBridges.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
                SelectGroups = groups,
                Artist = new ArtistDisplayViewModel()
            };

            model.Artist.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/designmanifestations")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public ActionResult EditDesignManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var rootManisfestationGroups = designManifestationGroupRepository.FindBy(e => e.Parent == null).ToArray();

            var groups = new Dictionary<DesignManifestationGroup, SelectionDescriptedItemInputViewModel<Guid>[]>();

            foreach (var group in rootManisfestationGroups)
            {
                PopulateDesignManifestationSelectableGroups(group, entity.DesignManifestationBridges.Select(item => item.Manifestation).ToList(), groups);
            }

            var model = new ArtistDesignManisfestationsInputViewModel
            {
                UngroupManifestations = designManifestationRepository.FindBy(item => item.GroupId == null).Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ArtisticManifestationBridges.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
                SelectGroups = groups,
                Artist = new ArtistDisplayViewModel()
            };

            model.Artist.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/servicelines")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public ActionResult EditServiceLines(Guid key)
        {
            var entity = Repository.Get(key);

            var rootManisfestationGroups = serviceLineGroupRepository.FindBy(e => e.Parent == null).ToArray();

            var groups = new Dictionary<ServiceLineGroup, SelectionDescriptedItemInputViewModel<Guid>[]>();

            foreach (var group in rootManisfestationGroups)
            {
                PopulateServiceLineSelectableGroups(group, entity.ServiceLineBridges.Select(item => item.Line).ToList(), groups);
            }

            var model = new ArtistServiceLinesInputViewModel
            {
                UngroupManifestations = serviceLineRepository.FindBy(item => item.GroupId == null).Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ServiceLineBridges.Any(bridge => bridge.LineId == item.Id),
                    Description = item.Description
                }).ToArray(),
                SelectGroups = groups,
                Artist = new ArtistDisplayViewModel()
            };

            model.Artist.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/artisticmanifestations")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditArtisticManifestations(Guid key, ArtistArtisticManisfestationsInputViewModel modelInput)
        {
            Artist entity = null;

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

            entity.ArtisticManifestationBridges.Clear();

            foreach (var item in modelInput.UngroupManifestations)
            {
                if (!item.Selected) continue;

                entity.ArtisticManifestationBridges.Add(new ArtistArtisticManifestationBridge
                {
                    ArtistId = entity.Id,
                    ManifestationId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editartisticmanifestations", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/craftmanifestations")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditCraftManifestations(Guid key, ArtistCraftManisfestationsInputViewModel modelInput)
        {
            Artist entity = null;

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

                entity.CraftManifestationBridges.Add(new ArtistCraftManifestationBridge
                {
                    ArtistId = entity.Id,
                    ManifestationId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editcraftmanifestations", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/designmanifestations")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditDesignManifestations(Guid key, ArtistDesignManisfestationsInputViewModel modelInput)
        {
            Artist entity = null;

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

                entity.DesignManifestationBridges.Add(new ArtistDesignManifestationBridge
                {
                    ArtistId = entity.Id,
                    ManifestationId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editdesignmanifestations", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/servicelines")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult EditServiceLines(Guid key, ArtistServiceLinesInputViewModel modelInput)
        {
            Artist entity = null;

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

                entity.ServiceLineBridges.Add(new ArtistServiceLineBridge
                {
                    ArtistId = entity.Id,
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
        [Route("edit/{key}/artisticacknowledgments")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public ActionResult EditArtisticAcknowledgments(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtistInputViewModel();

            PopulateAwards(entity, model.AwardsToSelect);

            model.Import(entity);

            return View(model);
        }
        
        [HttpGet]
        [Route("edit/{key}/artisticRecognitions")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public ActionResult EditArtisticRecognitions(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtistInputViewModel();

            PopulateRecognitions(entity, model.RecognitionsToSelect);

            model.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/artisticacknowledgments")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public ActionResult EditArtisticAcknowledgments(Guid key, ArtistInputViewModel modelInput)
        {
            Artist entity = null;

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

            entity.ArtistAwardBridges.Clear();

            foreach (var item in modelInput.AwardsToSelect)
            {
                if (!item.Selected) continue;

                entity.ArtistAwardBridges.Add(new ArtistAwardBridge
                {
                    ArtistId = entity.Id,
                    AwardId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("editartisticacknowledgments", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/artisticRecognitions")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public ActionResult EditArtisticRecognitions(Guid key, ArtistInputViewModel modelInput)
        {
            Artist entity = null;

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

            entity.ArtistRecognitionBridges.Clear();

            foreach (var item in modelInput.RecognitionsToSelect)
            {
                if (!item.Selected) continue;

                entity.ArtistRecognitionBridges.Add(new ArtistRecognitionBridge
                {
                    ArtistId = entity.Id,
                    RecognitionId = item.Id
                });
            }

            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("EditArtisticRecognitions", new { key = entity.Id });
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
                    Selected = entity.ArtistAwardBridges.Any(a => a.AwardId == item.Id),
                    Description = item.Country.Name
                }));
            }
           
        }

        private void PopulateRecognitions(Artist entity, List<SelectionDescriptedItemInputViewModel<Guid>> awardsForSelecting)
        {
            var recognitions = recognitionRepository.GetAll();

            if (recognitions.Count() > 0)
            {
                awardsForSelecting.AddRange(recognitions.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ArtistRecognitionBridges.Any(a => a.RecognitionId == item.Id),
                    Description = String.IsNullOrWhiteSpace(item.Description) ? "Sin Descripción" : item.Description
                }));
            }

        }

        #endregion

        [Route("edit/contacts/social/add")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSocialContact(ArtistSocialContactInputViewModel modelInput)
        {
            Artist entity = null;

            try
            {
                entity = Repository.Get(modelInput.ArtistId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.PersonSocialContactItemBridges.Add(new PersonSocialContactItemBridge { PersonId = entity.IdentityId, ContactItem = new SocialContactItem { Label = modelInput.Label, SocialId = modelInput.SocialId } });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
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

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        [Route("edit/contacts/social/delete")]
        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSocialContact(ArtistSocialContactDisplayViewModel modelDisplay)
        {
            Artist entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.ArtistId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.PersonSocialContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.PersonSocialContactItemBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts", new { key = entity.Id });
        }

        private Microsoft.AspNetCore.Html.HtmlString GetIcon(string socialId)
        {
            switch (socialId)
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
    }
}
