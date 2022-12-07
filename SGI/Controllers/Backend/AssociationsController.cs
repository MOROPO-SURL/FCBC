using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.RegionConst;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.EF.Repositories;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class AssociationsController : AbstractEntityManagementController<Association, Guid, AssociationInputViewModel, AssociationDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;        
        private readonly IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository;
        private readonly IEntityRepository<State, Guid> stateRepository;
        private readonly IEntityRepository<Municipality, Guid> municipalityRepository;
        public AssociationsController(IEntityRepository<Association, Guid> repository, IStringLocalizer<AssociationsController> localizer, ILogger<AssociationsController> logger, IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<State, Guid> stateRepository, IEntityRepository<Municipality, Guid> municipalityRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;            
            this.municipalityRegionRepository = municipalityRegionRepository;
            this.stateRepository = stateRepository;
            this.municipalityRepository = municipalityRepository;
        }

        [RequiresRoleBasePermission(Roles.AssociationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.AssociationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.AssociationCreator)]
        public override IActionResult Create(AssociationInputViewModel inputViewModel)
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

        [RequiresRoleBasePermission(Roles.AssociationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.AssociationEditor)]
        public override IActionResult Edit(AssociationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.AssociationRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.AssociationRemover)]
        public override IActionResult Delete(AssociationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
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

        private void PopulateWithCountries(AssociationInputViewModel modelInput)
        { 
            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Name == RegionInitConstant.CMG_STATE_NAME }).OrderByDescending(en => en.Selected).ToArray();
            
            modelInput.Municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == RegionInitConstant.MY_OWN_SATE).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        private void PopulateWithCountriesEdit(AssociationInputViewModel modelInput)
        {            
            modelInput.StateId = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId).StateId;
            
            modelInput.Municipalities = municipalityRepository.GetAll().Where(s => s.StateId == modelInput.StateId).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.MunicipalityId }).ToArray();
            
            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.StateId }).ToArray();           
        }

        protected override void ConfigureCreate(AssociationInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(AssociationInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigureEdit(Association entity, AssociationInputViewModel modelInput)
        {
            PopulateWithCountriesEdit(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(AssociationInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void PreCreate(Association entity, AssociationInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            var region = municipalityRegionRepository.FindOneBy(r => r.MunicipalityId == modelInput.MunicipalityId);
            PostalContactItem postalContact = new PostalContactItem
            {
                Address = modelInput.AddressPostal,
                RegionId = region.Id,
                Region = region
            };

            OrganizationPostalContactItemBridge organizationPostalContactItemBridge = new OrganizationPostalContactItemBridge
            {
                OrganizationId = modelInput.Export().OrganizationIdentity.Id,
                Organization = modelInput.Export().OrganizationIdentity,
                ContactItemId = postalContact.Id,
                ContactItem = postalContact
            };
            entity.OrganizationIdentity.OrganizationPostalContactItemBridges.Add(organizationPostalContactItemBridge);

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Association " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PreEdit(Association entity, AssociationInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            var contact = entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem;
            contact.Address = modelInput.AddressPostal;
            var region = municipalityRegionRepository.GetAll().FirstOrDefault(p => p.MunicipalityId == modelInput.MunicipalityId);
            var municipality = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId);
            modelInput.StateId = municipality.StateId;
            if (region.Id != entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId)
            {
                entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.Region = region;
                entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId = region.Id;

            }
            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.OrganizationIdentity.Image != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.OrganizationIdentity.Image);

                    imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.OrganizationIdentity.Image != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.OrganizationIdentity.Image);

                imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Association " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PostDelete(Association entity)
        {
            base.PostDelete(entity);

            organizationIdentityRepository.Delete(entity.OrganizationIdentity, User.Identity.Name);

            if (entity.OrganizationIdentity.Image == null) return;

            imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.OrganizationIdentity.Image);
        }

        [Route("edit/{key}/contacts")]
        [RequiresRoleBasePermission(Roles.AssociationEditor)]
        public IActionResult Contacts(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new AssociationContactsManageViewModel
                {
                    Association = new AssociationDisplayViewModel(),
                    PhoneContacts = entity.OrganizationIdentity.OrganizationPhoneContactItemBridges.Select(bridge => new AssociationPhoneContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Number = bridge.ContactItem.PhoneNumber,
                        AssociationId = entity.Id
                    }).ToList(),
                    EmailContacts = entity.OrganizationIdentity.OrganizationEmailContactItemBridges.Select(bridge => new AssociationEmailContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Address = bridge.ContactItem.EmailAddress,
                        AssociationId = entity.Id
                    }).ToList()
                };

                resultModel.Association.Import(entity);

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
        [RequiresRoleBasePermission(Roles.AssociationEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhoneContact(AssociationPhoneContactInputViewModel modelInput)
        {
            Association entity = null;

            try
            {
                entity = Repository.Get(modelInput.AssociationId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.OrganizationIdentity.OrganizationPhoneContactItemBridges.Add(new OrganizationPhoneContactItemBridge { OrganizationId = entity.OrganizationIdentityId, ContactItem = new PhoneContactItem { Label = modelInput.Label, PhoneNumber = modelInput.Number } });

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
        [RequiresRoleBasePermission(Roles.AssociationEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePhoneContact(AssociationPhoneContactDisplayViewModel modelDisplay)
        {
            Association entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.AssociationId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.OrganizationIdentity.OrganizationPhoneContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.OrganizationIdentity.OrganizationPhoneContactItemBridges.Remove(bridge);

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
        [RequiresRoleBasePermission(Roles.AssociationEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmailContact(AssociationEmailContactInputViewModel modelInput)
        {
            Association entity = null;

            try
            {
                entity = Repository.Get(modelInput.AssociationId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.OrganizationIdentity.OrganizationEmailContactItemBridges.Add(new OrganizationEmailContactItemBridge { OrganizationId = entity.OrganizationIdentityId, ContactItem = new EmailContactItem { Label = modelInput.Label, EmailAddress = modelInput.Address } });

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
        [RequiresRoleBasePermission(Roles.AssociationEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmailContact(AssociationEmailContactDisplayViewModel modelDisplay)
        {
            Association entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.AssociationId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.OrganizationIdentity.OrganizationEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.OrganizationIdentity.OrganizationEmailContactItemBridges.Remove(bridge);

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
    }
}
