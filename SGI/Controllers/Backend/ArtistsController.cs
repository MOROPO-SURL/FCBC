using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
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

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class ArtistsController : AbstractEntityManagementFilteredController<Artist, System.Guid, ArtistInputViewModel, ArtistDisplayViewModel, ArtistFilterModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<PersonIdentity, System.Guid> identityRepository;
        private readonly IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository;
        private readonly IEntityRepository<State, Guid> stateRepository;
        private readonly IEntityRepository<Municipality, Guid> municipalityRepository;
        private readonly IEntityRepository<Association, Guid> associationRepository;
        private readonly IEntityRepository<Resolution, Guid> resolutionRepository;
        private readonly IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository;
        public ArtistsController(IEntityRepository<PersonIdentity, System.Guid> identityRepository, IEntityRepository<Image, System.Guid> imageRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository, IEntityRepository<State, Guid> stateRepository, IEntityRepository<Municipality, Guid> municipalityRepository, IEntityRepository<Artist, Guid> repository, IEntityRepository<Association, Guid> associationRepository, IEntityRepository<Resolution, Guid> resolutionRepository, IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository, IStringLocalizer<ArtistsController> localizer, ILogger<ArtistsController> logger) : base(repository, localizer, logger)
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
            PopulateWithArtisticManifestation(inputViewModel);
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
            PopulateWithArtisticManifestationEdit(inputViewModel);

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
            var stateIdParse = System.Guid.Parse(stateId);

            var municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == stateIdParse).ToList();
            return Ok(municipalities);
        }

        [HttpGet("edit/PopulateStateToMunicipalityEdit/{stateId}")]
        public IActionResult PopulateStateToMunicipalityEdit(string stateId)
        {
            var stateIdParse = System.Guid.Parse(stateId);

            var municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == stateIdParse).ToList();
            return Ok(municipalities);
        }

        private void PopulateWithCountries(ArtistInputViewModel modelInput)
        {
            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Name == RegionInitConstant.CMG_STATE_NAME }).OrderByDescending(en => en.Selected).ToArray();

            modelInput.Municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == RegionInitConstant.MY_OWN_SATE).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        private void PopulateWithAssociation(ArtistInputViewModel modelInput)
        {
            modelInput.Associations = associationRepository.GetAll().Select( entity => new SelectListItem { Value= entity.Id.ToString(), Text = entity.OrganizationIdentity.Name}).ToArray();
        }

        private void PopulateWithAssociationEdit(ArtistInputViewModel modelInput)
        {
            bool contextHasElemet = false;
            if (modelInput.Context != null && modelInput.Context.Contains("Association"))
            {
                contextHasElemet = true;
            }
            modelInput.Associations = associationRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.OrganizationIdentity.Name, Selected = contextHasElemet }).ToArray();
        }

        private void PopulateWithResolution(ArtistInputViewModel modelInput)
        {
            modelInput.Resolutions = resolutionRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text =entity.NumberResolution +" - "+entity.Title }).ToArray();
        }

        private void PopulateWithResolutionEdit(ArtistInputViewModel modelInput)
        {
            bool contextHasElemet = false;
            if (modelInput.Context != null && modelInput.Context.Contains("Resolution"))
            {
                contextHasElemet = true;
            }

            modelInput.Resolutions = resolutionRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.NumberResolution + " - " + entity.Title, Selected = contextHasElemet }).ToArray();
        }

        private void PopulateWithCountriesEdit(ArtistInputViewModel modelInput)
        {
            modelInput.StateId = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId).StateId;

            modelInput.Municipalities = municipalityRepository.GetAll().Where(s => s.StateId == modelInput.StateId).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.MunicipalityId }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.StateId }).ToArray();
        }

        private void PopulateWithArtisticManifestation(ArtistInputViewModel modelInput)
        {            
            List<SelectListItem> artisticManifestationItems = artisticManifestationRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToList();
            modelInput.ArtisticManifestations = new MultiSelectList(artisticManifestationItems.OrderBy(i => i.Text), "Value", "Text", modelInput.SelectedValues);
        }

        private void PopulateWithArtisticManifestationEdit(ArtistInputViewModel modelInput)
        {
            var SelectedValues = modelInput.SelectedValues;

            List<SelectListItem> artisticManifestationItems = artisticManifestationRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = SelectedValues.Any(s => s == entity.Id) }).ToList();
            modelInput.ArtisticManifestations = new MultiSelectList(artisticManifestationItems.OrderBy(i => i.Text), "Value", "Text", modelInput.SelectedValues);
        }


        protected override void PreCreate(Artist entity, ArtistInputViewModel inputViewModel)
        {
            PopulateWithArtisticManifestation(inputViewModel);

            base.PreCreate(entity, inputViewModel);

           

            var region = municipalityRegionRepository.FindOneBy(r => r.MunicipalityId == inputViewModel.MunicipalityId);
            PostalContactItem postalContact = new PostalContactItem
            {
                Address = inputViewModel.AddressPostal,
                RegionId = region.Id,
                Region = region
            };

            PersonPostalContactItemBridge personPostalContactItemBridge = new PersonPostalContactItemBridge
            {
                PersonId = inputViewModel.Export().PersonIdentity.Id,
                Person = inputViewModel.Export().PersonIdentity,
                ContactItemId = postalContact.Id,
                ContactItem = postalContact
            };
            entity.PersonIdentity.PersonPostalContactItemBridges.Add(personPostalContactItemBridge);

            var getRegionRegister = entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            inputViewModel.Municipality = ((MunicipalityRegion)getRegionRegister).Municipality;
            inputViewModel.State = inputViewModel.Municipality.State;

            inputViewModel.ArtistManifestationBridges.Clear();

            foreach (var artisticManifestationId in inputViewModel.SelectedValues)
            {
                ArtisticManifestation artisticMani = this.artisticManifestationRepository.GetAll().FirstOrDefault(r => r.Id == artisticManifestationId);
                ArtisticManifestationInputViewModel artisticManifestationInputViewModel = new ArtisticManifestationInputViewModel
                {
                    Id = artisticManifestationId,
                    Name = artisticMani.Name,
                    
                };
                var ArtistManifestationBridge = new ArtistManifestationBridge { ArtistId = inputViewModel.Id, ArtisticManifestationId = artisticManifestationId, ArtisticManifestation = artisticManifestationInputViewModel.Export(), Artist = inputViewModel.Export() };
                entity.ArtistManifestationBridges.Add(ArtistManifestationBridge);

            }

            if (inputViewModel.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Critic " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.PersonIdentity.Image = image;
            }
        }

        protected override void PreEdit(Artist entity, ArtistInputViewModel inputViewModel)
        {
           
            base.PreEdit(entity, inputViewModel);

            var contact = entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem;
            contact.Address = inputViewModel.AddressPostal;
            var region = municipalityRegionRepository.GetAll().FirstOrDefault(p => p.MunicipalityId == inputViewModel.MunicipalityId);
            var municipality = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == inputViewModel.MunicipalityId);
            inputViewModel.StateId = municipality.StateId;
            if (region.Id != entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId)
            {
                entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region = region;
                entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId = region.Id;

            }

            inputViewModel.ArtistManifestationBridges.Clear();
            
            foreach (var artisticManifestationId in inputViewModel.SelectedValues)
            {
                ArtisticManifestation artisticMani = this.artisticManifestationRepository.GetAll().FirstOrDefault(r => r.Id == artisticManifestationId);
                ArtisticManifestationInputViewModel artisticManifestationInputViewModel = new ArtisticManifestationInputViewModel
                {
                    Id = artisticManifestationId,
                    Name = artisticMani.Name,

                };
                var ArtistManifestationBridge = new ArtistManifestationBridge { ArtistId = inputViewModel.Id, ArtisticManifestationId = artisticManifestationId, ArtisticManifestation = artisticManifestationInputViewModel.Export(), Artist = inputViewModel.Export() };
                
                entity.ArtistManifestationBridges.Add(ArtistManifestationBridge);

            }

            if (inputViewModel.ImageFile == null)
            {
                if (inputViewModel.ResetImage && entity.PersonIdentity.Image != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.PersonIdentity.Image);

                    imageRepository.Delete(entity.PersonIdentity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.PersonIdentity.Image != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.PersonIdentity.Image);

                imageRepository.Delete(entity.PersonIdentity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Critic " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.PersonIdentity.Image = image;
            }
        }

        protected override void PostDelete(Artist entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.PersonIdentity, User.Identity.Name);

            if (entity.PersonIdentity.Image == null) return;

            imageRepository.Delete(entity.PersonIdentity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.PersonIdentity.Image);
        }

        protected override void ConfigureCreate(ArtistInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
            PopulateWithAssociation(modelInput);
            PopulateWithResolution(modelInput);
            PopulateWithArtisticManifestation(modelInput);

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
            PopulateWithArtisticManifestationEdit(modelInput);
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
                    PhoneContacts = entity.PersonIdentity.PersonPhoneContactItemBridges.Select(bridge => new ArtistPhoneContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Number = bridge.ContactItem.PhoneNumber,
                        ArtistId = entity.Id
                    }).ToList(),
                    EmailContacts = entity.PersonIdentity.PersonEmailContactItemBridges.Select(bridge => new ArtistEmailContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Address = bridge.ContactItem.EmailAddress,
                        ArtistId = entity.Id
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
                entity.PersonIdentity.PersonPhoneContactItemBridges.Add(new PersonPhoneContactItemBridge { PersonId = entity.PersonIdentityId, ContactItem = new PhoneContactItem { Label = modelInput.Label, PhoneNumber = modelInput.Number } });

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
                var bridge = entity.PersonIdentity.PersonPhoneContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.PersonIdentity.PersonPhoneContactItemBridges.Remove(bridge);

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
                entity.PersonIdentity.PersonEmailContactItemBridges.Add(new PersonEmailContactItemBridge { PersonId = entity.PersonIdentityId, ContactItem = new EmailContactItem { Label = modelInput.Label, EmailAddress = modelInput.Address } });

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
                var bridge = entity.PersonIdentity.PersonEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.PersonIdentity.PersonEmailContactItemBridges.Remove(bridge);

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
                case FBCB.SGI.Persistence.Enumerations.ModeEntry.Membership: 
                        if(modelInput.AssociationId == null)
                         ModelState.AddModelError("AssociationId", "RequiredValidationErrorMessage");break;
                case FBCB.SGI.Persistence.Enumerations.ModeEntry.Resolution:
                        if (modelInput.ResolutionId == null)
                         ModelState.AddModelError("ResolutionId", "RequiredValidationErrorMessage"); break;
            }
            
        }

        protected override void PopulateViewModelDisplayExtras(ArtistDisplayViewModel modelDisplay, Artist entity)
        {
            if (modelDisplay.Context != null)
            {
                if (modelDisplay.Context.Contains("Association|"))
                {
                    var resultSplit = modelDisplay.Context.Split("|");                    
                    var association = associationRepository.Get(System.Guid.Parse(resultSplit[1]));
                    modelDisplay.Association = new AssociationDisplayViewModel 
                    { 
                         Id = association.Id,
                         OrganizationIdentityName = association.OrganizationIdentity.Name
                    };
                    modelDisplay.Context = "Association|" + modelDisplay.AssociationId.ToString();
                }

                if (modelDisplay.Context.Contains("Resolution|"))
                {
                    var resultSplit = modelDisplay.Context.Split("|");
                    var resolution = resolutionRepository.Get(System.Guid.Parse(resultSplit[1]));
                    modelDisplay.Resolution = new ResolutionDisplayViewModel
                    {
                        Id = resolution.Id,
                        NumberResolution = resolution.NumberResolution,
                        Title = resolution.Title,
                        Description = resolution.Description
                    } ;
                    modelDisplay.Context = "Resolution|" + modelDisplay.ResolutionId.ToString();
                }
            }

            

            base.PopulateViewModelDisplayExtras(modelDisplay, entity);
        }

        protected override void ValidateEdit(ArtistInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);

            PopulateWithResolutionEdit(modelInput);
            PopulateWithAssociationEdit(modelInput);
            PopulateWithArtisticManifestationEdit(modelInput);

            switch (modelInput.AdmitionModel)
            {
                case FBCB.SGI.Persistence.Enumerations.ModeEntry.Membership:
                    if (modelInput.AssociationId == null)
                        ModelState.AddModelError("AssociationId", "RequiredValidationErrorMessage"); break;
                case FBCB.SGI.Persistence.Enumerations.ModeEntry.Resolution:
                    if (modelInput.ResolutionId == null)
                        ModelState.AddModelError("ResolutionId", "RequiredValidationErrorMessage"); break;
            }

        }
    }
}
