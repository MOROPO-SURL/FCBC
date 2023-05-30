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
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using MOROPO.FCBC.SGI.Extensions;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Core.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Web.Frontend;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
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
        private readonly IEntityRepository<CraftManifestation, Guid> craftManifestationRepository;
        private readonly IEntityRepository<DesignManifestation, Guid> designManifestationRepository;
        private readonly IEntityRepository<ServiceLine, Guid> serviceLineRepository;
        private readonly IEntityRepository<Award, Guid> awardRepository;
        private readonly IEntityRepository<Recognition, Guid> recognitionRepository;
        private readonly IEntityRepository<Distinction, Guid> distintionsRepository;
        private readonly Managers.RelativeAttachmentFileManager attachmentFileManager;
        private readonly IEntityRepository<ArtistAttachment, Guid> attachmentRepository;
        private readonly IEntityRepository<Critic, Guid> criticRepository;
        private readonly IEntityRepository<ArtistValoration, Guid> artistValorationRepository;

        public ArtistsController(
            IEntityRepository<ArtistValoration, Guid> artistValorationRepository,
            IEntityRepository<ServiceLine, Guid> serviceLineRepository,
            IEntityRepository<DesignManifestation, Guid> designManifestationRepository,
            IEntityRepository<CraftManifestation, Guid> craftManifestationRepository,
            IEntityRepository<PersonIdentity, Guid> identityRepository, 
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
            IEntityRepository<Distinction, Guid> distintionsRepository,
            Managers.RelativeAttachmentFileManager attachmentFileManager,
            IEntityRepository<ArtistAttachment, System.Guid> attachmentRepository,
            IEntityRepository<Critic, System.Guid> criticRepository,
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
            this.craftManifestationRepository = craftManifestationRepository;
            this.designManifestationRepository = designManifestationRepository;
            this.serviceLineRepository = serviceLineRepository;
            this.awardRepository = awardRepository;
            this.recognitionRepository = recognitionRepository;
            this.distintionsRepository = distintionsRepository;
            this.attachmentFileManager = attachmentFileManager;
            this.attachmentRepository = attachmentRepository;
            this.criticRepository = criticRepository;
            this.artistValorationRepository = artistValorationRepository;
        }

        [RequirePermissions(Permission.ArtistViewer)]
        public IActionResult Index()
        { 
            return View();
        }

        [RequirePermissions(Permission.ArtistViewer)]
        public override IActionResult Filter(ArtistFilterModel filterModel)
        {           
            return base.Filter(filterModel);
        }

        [RequirePermissions(Permission.ArtistViewer)]
        protected override ArtistFilterModel GetFilterModel()
        {
            var filterModel = base.GetFilterModel();

            if (filterModel == null)
            {
                filterModel = new ArtistFilterModel();
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
            return "artist-management-filter";
        }

        [RequirePermissions(Permission.ArtistCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ArtistCreator)]
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

        [RequirePermissions(Permission.ArtistEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ArtistEditor)]
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

        [RequirePermissions(Permission.ArtistRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ArtistRemover)]
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

        private void PopulateDropDownStateWithMunicipality(ArtistFilterModel filter)
        {
           
            var states = stateRepository.GetAll();
            var municipalities = municipalityRepository.GetAll();
            var stateGroups = states.Select(s => new SelectListGroup { Name = s.Name});

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

        private void PopulateManifestations(ArtistFilterModel filter)
        {
            var artistManifestations = artisticManifestationRepository.GetAll();
            var ArtistGroup = new SelectListGroup { Name = "Artes Visuales"};

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

            // TODO En ocasiones queda null, hay que buscar inicailizar las regiones en este punto
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

        protected override void ConfigurePostFailEditValidation(Artist entity, ArtistInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        [Route("edit/{key}/contacts")]
        [RequirePermissions(Permission.ArtistEditor)]
        public IActionResult Contacts(Guid key)
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

            SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("artists/{key}/print")]
        [RequirePermissions(Permission.ArtistViewer)]
        [RequirePermissions(Permission.ArtistEditor)]
        public IActionResult Print(Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var region = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
                var municipality = ((MunicipalityRegion)region).Municipality;
                var state = municipality.State;

                var artist = new ArtistDisplayViewModel();
                artist.Import(entity);
                var phoneContacts = entity.Identity.PersonPhoneContactItemBridges.Select(bridge => new ArtistPhoneContactDisplayViewModel
                {
                    Id = bridge.ContactItemId,
                    Label = bridge.ContactItem.Label,
                    Number = bridge.ContactItem.PhoneNumber,
                    ArtistId = entity.Id
                }).ToList();

                var emailContacts = entity.Identity.PersonEmailContactItemBridges.Select(bridge => new ArtistEmailContactDisplayViewModel
                {
                    Id = bridge.ContactItemId,
                    Label = bridge.ContactItem.Label,
                    Address = bridge.ContactItem.EmailAddress,
                    ArtistId = entity.Id
                }).ToList();

                var resultModel = new ArtistContactsManageViewModel
                {
                    Artist = artist,
                    PhoneContacts = phoneContacts,
                    EmailContacts = emailContacts
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

            SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/contacts/phone/add")]
        [RequirePermissions(Permission.ArtistEditor)]
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
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.PersonPhoneContactItemBridges.Add(new PersonPhoneContactItemBridge { PersonId = entity.IdentityId, ContactItem = new PhoneContactItem { Label = modelInput.Label, PhoneNumber = modelInput.Number } });

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

        [Route("edit/contacts/phone/delete")]
        [RequirePermissions(Permission.ArtistEditor)]
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
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.PersonPhoneContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.PersonPhoneContactItemBridges.Remove(bridge);

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
        [RequirePermissions(Permission.ArtistEditor)]
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
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.PersonEmailContactItemBridges.Add(new PersonEmailContactItemBridge { PersonId = entity.IdentityId, ContactItem = new EmailContactItem { Label = modelInput.Label, EmailAddress = modelInput.Address } });

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
        [RequirePermissions(Permission.ArtistEditor)]
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
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.PersonEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.PersonEmailContactItemBridges.Remove(bridge);

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
                         Name = association.Identity.Name
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
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditArtisticManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtistArtisticManisfestationsInputViewModel
            {
                Manifestations = artisticManifestationRepository.GetAll().ToArray().Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ArtisticManifestationBridges.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
                Artist = new ArtistDisplayViewModel()
            };

            model.Artist.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/craftmanifestations")]
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditCraftManifestations(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtistCraftManisfestationsInputViewModel
            {
                Manifestations = craftManifestationRepository.GetAll().ToArray().Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.CraftManifestationBridges.Any(bridge => bridge.ManifestationId == item.Id),
                    Description = item.Description
                }).ToArray(),
                Artist = new ArtistDisplayViewModel()
            };

            model.Artist.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/servicelines")]
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditServiceLines(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtistServiceLinesInputViewModel
            {
                Manifestations = serviceLineRepository.GetAll().ToArray().Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ServiceLineBridges.Any(bridge => bridge.LineId == item.Id),
                    Description = item.Description
                }).ToArray(),
                Artist = new ArtistDisplayViewModel()
            };

            model.Artist.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/artisticmanifestations")]
        [RequirePermissions(Permission.ArtistEditor)]
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

            foreach (var item in modelInput.Manifestations)
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
        [RequirePermissions(Permission.ArtistEditor)]
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

            foreach (var item in modelInput.Manifestations)
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
        [Route("edit/{key}/servicelines")]
        [RequirePermissions(Permission.ArtistEditor)]
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

            foreach (var item in modelInput.Manifestations)
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

        #endregion

        #region Acknowledgments

        [HttpGet]
        [Route("edit/{key}/artisticacknowledgments")]
        [RequirePermissions(Permission.ArtistEditor)]
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
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditArtisticRecognitions(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtistInputViewModel();

            PopulateRecognitions(entity, model.RecognitionsToSelect);

            model.Import(entity);

            return View(model);
        }

        [HttpGet]
        [Route("edit/{key}/artisticDistintions")]
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditArtisticDistintions(Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtistInputViewModel();

            PopulateDistintions(entity, model.DistintionsToSelect);

            model.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/artisticacknowledgments")]
        [RequirePermissions(Permission.ArtistEditor)]
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

            entity.ArtistAwards.Clear();           

            foreach (var item in modelInput.AwardsToSelect)
            {
                if (!item.Selected) continue;

                entity.ArtistAwards.Add(new ArtistAward
                {
                    ArtistId = entity.Id,
                    AwardId = item.Id
                });
            }           

            
            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("EditArtisticAcknowledgments", new { key = entity.Id });
        }

        [HttpPost]
        [Route("edit/{key}/artisticRecognitions")]
        [RequirePermissions(Permission.ArtistEditor)]
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

        [HttpPost]
        [Route("edit/{key}/artisticDistintions")]
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditArtisticDistintions(Guid key, ArtistInputViewModel modelInput)
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

            entity.ArtistDistinctionBridges.Clear();

            foreach (var item in modelInput.DistintionsToSelect)
            {
                if (!item.Selected) continue;

                entity.ArtistDistinctionBridges.Add(new ArtistDistinctionBridge
                {
                    ArtistId = entity.Id,
                    DistinctionId = item.Id
                });
            }


            Repository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

            return RedirectToAction("EditArtisticDistintions", new { key = entity.Id });
        }

        [Route("edit/{key}/ArtistAwards")]
        [RequirePermissions(Permission.ArtistEditor)]
        public IActionResult ArtistAwards(Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ArtistAwardManageViewModel
                {
                    ArtistDisplay = new ArtistDisplayViewModel()  
                };

                PopulateAwards(resultModel);

                resultModel.ArtistDisplay.Import(entity);                

                return View(resultModel);
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }


        [Route("edit/Awards/add")]
        [RequirePermissions(Permission.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddArtistAwards(ArtistAwardManageViewModel modelInput)
        {
            Artist entity = null;
            Award award = null;
            try
            {
                entity = Repository.Get(modelInput.ArtistDisplay.Id);
                award = awardRepository.Get(modelInput.AwardId);
                
                modelInput.ArtistDisplay.Import(entity);
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
                entity.ArtistAwards.Add( new ArtistAward { Artist = entity, Award = award, ArtistId = entity.Id, AwardId = award.Id, Context = modelInput.Context}); 

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

            return RedirectToAction("ArtistAwards", new { key = entity.Id });
        }

        [Route("edit/Awards/delete")]
        [RequirePermissions(Permission.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteArtistAwards(ArtistAwardDisplayViewModel modelDisplay)
        {
            Artist entity = null;  
            try
            {
                entity = Repository.Get(modelDisplay.ArtistId);                
                
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                var artistAward = entity.ArtistAwards.First(a => a.Id== modelDisplay.Id);
                entity.ArtistAwards.Remove(artistAward);

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

            return RedirectToAction("ArtistAwards", new { key = entity.Id });
        }

        #region Criterion
        
        [Route("edit/{key}/criteria")]
        [RequirePermissions(Permission.ArtistEditor)]
        public IActionResult EditCriteria(Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ArtistCriterionViewModel
                {
                    Artist = new ArtistDisplayViewModel(),
                    Criterials = artistValorationRepository.FindBy(item => item.CreatorId == entity.Id).ToArray().Select(item =>
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

                resultModel.Artist.Import(entity);

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
        [RequirePermissions(Permission.ArtistEditor)]
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
                    artistValorationRepository.Add(new ArtistValoration
                    {
                        Critic = critic,
                        CriticId = modelInput.CriticId,
                        Creator = entity,
                        CreatorId = critic.Id,
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
        [RequirePermissions(Permission.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCriterion(Guid key, CriterialInputViewModel modelInput)
        {
            try
            {
                var entity = Repository.Get(key);

                var criterion = artistValorationRepository.Get(modelInput.Id);

                if (ModelState.IsValid)
                {
                    criterion.DeclaredAt = modelInput.DateOfCriterion;
                    criterion.Declaration = modelInput.Criterion;

                    artistValorationRepository.Update(criterion, User.Identity.Name);

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
        [RequirePermissions(Permission.ArtistEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCriterion(Guid key, Guid criterionId)
        {
            try
            {
                var entity = Repository.Get(key);

                var criterion = artistValorationRepository.Get(criterionId);

                if (ModelState.IsValid)
                {
                    artistValorationRepository.Delete(criterion, User.Identity.Name);

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

        private void PopulateAwards(ArtistAwardManageViewModel modelInput)
        {
            modelInput.SelectAwards = awardRepository.GetAll().ToArray().Select( item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToArray();
        }

        private void PopulateAwards(Artist entity, List<SelectionDescriptedItemInputViewModel<Guid>> awardsForSelecting)
        {
            var awads = awardRepository.GetAll().ToArray();
            

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

        private void PopulateDistintions(Artist entity, List<SelectionDescriptedItemInputViewModel<Guid>> awardsForSelecting)
        {
            var distintions = distintionsRepository.GetAll();

            if (distintions.Count() > 0)
            {
                awardsForSelecting.AddRange(distintions.Select(item => new SelectionDescriptedItemInputViewModel<Guid>
                {
                    Id = item.Id,
                    Label = item.Name,
                    Selected = entity.ArtistDistinctionBridges.Any(a => a.DistinctionId == item.Id),
                    Description = String.IsNullOrWhiteSpace(item.Description) ? "Sin Descripción" : item.Description
                }));
            }

        }

        #endregion

        #region Attachments

        [HttpGet]
        [Route("edit/{key}/attachments")]
        [RequirePermissions(Permission.ArtistEditor)]
        public ActionResult EditAttachments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ArtistAttachmentsDisplayViewModel
            {
                ArtistDisplay = new ArtistDisplayViewModel(),
                Attachments = entity.Attachments.Select(item =>
                {
                    var result = new AttachmentDisplayViewModel();

                    result.Import(item);

                    return result;
                }).ToArray()
            };

            model.ArtistDisplay.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/attachments/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ArtistEditor)]
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

                            var attachment = new ArtistAttachment
                            {
                                Name = model.Name,
                                IsPublic = model.IsPublic,
                                Description = model.Description,
                                ContentType = model.File.ContentType,
                                Length = model.File.Length,
                                ArtistId = entity.Id
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
        [RequirePermissions(Permission.ArtistEditor)]
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
        [RequirePermissions(Permission.ArtistEditor)]
        public IActionResult EditAttachment(System.Guid key, AttachmentInputViewModel modelInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ArtistAttachment val = attachmentRepository.Get(modelInput.Id);
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

        [Route("edit/contacts/social/add")]
        [RequirePermissions(Permission.ArtistEditor)]
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
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.PersonSocialContactItemBridges.Add(new PersonSocialContactItemBridge { PersonId = entity.IdentityId, ContactItem = new SocialContactItem { Label = modelInput.Label, SocialId = modelInput.SocialId } });

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
        [RequirePermissions(Permission.ArtistEditor)]
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
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Identity.PersonSocialContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.PersonSocialContactItemBridges.Remove(bridge);

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
