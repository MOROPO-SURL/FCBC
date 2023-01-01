using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.RegionConst;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class EmployeesController : AbstractEntityManagementFilteredController<Employee, Guid, EmployeeInputViewModel, EmployeeDisplayViewModel, EmployeeFilterModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<PersonIdentity, Guid> identityRepository;
        private readonly IEntityRepository<AdministrativeRole, Guid> administrativeRoleRepository;
        private readonly IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository;
        private readonly IEntityRepository<State, Guid> stateRepository;
        private readonly IEntityRepository<Municipality, Guid> municipalityRepository;

        public EmployeesController(IEntityRepository<PersonIdentity, Guid> identityRepository, IEntityRepository<Image, Guid> imageRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Employee, Guid> repository, IEntityRepository<AdministrativeRole, Guid> administrativeRoleRepository, IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository, IEntityRepository<State, Guid> stateRepository, IEntityRepository<Municipality, Guid> municipalityRepository,
        IStringLocalizer<EmployeesController> localizer, ILogger<EmployeesController> logger) : base(repository, localizer, logger)
        {
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.identityRepository = identityRepository;
            this.administrativeRoleRepository = administrativeRoleRepository;
            this.municipalityRegionRepository = municipalityRegionRepository;
            this.stateRepository = stateRepository;
            this.municipalityRepository = municipalityRepository;
        }

        [RequirePermissions(Permission.EmployeeViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.CriticViewer)]
        public override IActionResult Filter(EmployeeFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "employee-management-filter";
        }

        [RequirePermissions(Permission.EmployeeCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.EmployeeCreator)]
        public override IActionResult Create(EmployeeInputViewModel inputViewModel)
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

        [RequirePermissions(Permission.EmployeeEditor)]
        public override IActionResult Edit( System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.EmployeeEditor)]
        public override IActionResult Edit(EmployeeInputViewModel inputViewModel)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

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

        [RequirePermissions(Permission.EmployeeRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.EmployeeRemover)]
        public override IActionResult Delete(EmployeeDisplayViewModel displayViewModel)
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

        private void PopulateWithCountries(EmployeeInputViewModel modelInput)
        {
            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Name == RegionInitConstant.CMG_STATE_NAME }).OrderByDescending(en => en.Selected).ToArray();

            modelInput.Municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == RegionInitConstant.MY_OWN_SATE).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        private void PopulateWithCountriesEdit(EmployeeInputViewModel modelInput)
        {
            modelInput.StateId = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId).StateId;

            modelInput.Municipalities = municipalityRepository.GetAll().Where(s => s.StateId == modelInput.StateId).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.MunicipalityId }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.StateId }).ToArray();
        }

        protected override void PreCreate(Employee entity, EmployeeInputViewModel inputViewModel)
        {
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
                PersonId = inputViewModel.Export().Identity.Id,
                Person = inputViewModel.Export().Identity,
                ContactItemId = postalContact.Id,
                ContactItem = postalContact
            };
            entity.Identity.PersonPostalContactItemBridges.Add(personPostalContactItemBridge);

            var getRegionRegister = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            inputViewModel.Municipality = ((MunicipalityRegion)getRegionRegister).Municipality;
            inputViewModel.State = inputViewModel.Municipality.State;

            /*foreach (var admRolId in inputViewModel.SelectableRoles.Where(r => r.Selected == true).Select(r => r.Id))
            {
                AdministrativeRole admRole = this.administrativeRoleRepository.GetAll().FirstOrDefault(r => r.Id == admRolId);
                AdministrativeRoleInputViewModel administrativeRoleInputViewModel = new AdministrativeRoleInputViewModel
                {
                    Id = admRolId,
                    Name = admRole.Name,
                    Description = admRole.Description
                };
                var EmployeeAdministrativeRoleBridge = new EmployeeAdministrativeRoleBridge { EmployeeId = inputViewModel.Id, RoleId = admRolId, Role = administrativeRoleInputViewModel.Export(), Employee = inputViewModel.Export() };
                entity.AdministrativeRoleBridges.Add(EmployeeAdministrativeRoleBridge);
                
            }*/            

            if (inputViewModel.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Critic " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PreEdit(Employee entity, EmployeeInputViewModel inputViewModel)
        {
            base.PreEdit(entity, inputViewModel);

            PopulateWithCountriesEdit(inputViewModel);

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

            entity.AdministrativeRoleBridges.Clear();

            foreach (var admRolId in inputViewModel.SelectableRoles.Where(r => r.Selected == true).Select(e => e.Id))
            {
                AdministrativeRole admRole = this.administrativeRoleRepository.GetAll().FirstOrDefault(r => r.Id == admRolId);
                AdministrativeRoleInputViewModel administrativeRoleInputViewModel = new AdministrativeRoleInputViewModel
                {
                    Id = admRolId,
                    Name = admRole.Name,
                    Description = admRole.Description
                };
                var EmployeeAdministrativeRoleBridge = new EmployeeAdministrativeRoleBridge { EmployeeId = inputViewModel.Id, RoleId = admRolId, Role = administrativeRoleInputViewModel.Export(), Employee = inputViewModel.Export() };
                entity.AdministrativeRoleBridges.Add(EmployeeAdministrativeRoleBridge);

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

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Critic " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PostDelete(Employee entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.Identity, User.Identity.Name);

            if (entity.Identity.Image == null) return;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);
        }

        protected override void ConfigureCreate(EmployeeInputViewModel modelInput)
        {           
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(EmployeeInputViewModel modelInput)
        {
            
        }

        protected override void ConfigureEdit(Employee entity, EmployeeInputViewModel modelInput)
        {           
            PopulateWithCountriesEdit(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(EmployeeInputViewModel modelInput)
        {
           
        }

        [Route("edit/{key}/contacts")]
        [RequirePermissions(Permission.EmployeeEditor)]
        public IActionResult Contacts(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new EmployeeContactsManageViewModel
                {
                    Employee = new EmployeeDisplayViewModel(),
                    PhoneContacts = entity.Identity.PersonPhoneContactItemBridges.Select(bridge => new EmployeePhoneContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Number = bridge.ContactItem.PhoneNumber,
                        EmployeeId = entity.Id
                    }).ToList(),
                    EmailContacts = entity.Identity.PersonEmailContactItemBridges.Select(bridge => new EmployeeEmailContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Address = bridge.ContactItem.EmailAddress,
                        EmployeeId = entity.Id
                    }).ToList()
                };

                resultModel.Employee.Import(entity);

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/contacts/phone/add")]
        [RequirePermissions(Permission.EmployeeEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhoneContact(EmployeePhoneContactInputViewModel modelInput)
        {
            Employee entity = null;

            try
            {
                entity = Repository.Get(modelInput.EmployeeId);
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
        [RequirePermissions(Permission.EmployeeEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePhoneContact(EmployeePhoneContactDisplayViewModel modelDisplay)
        {
            Employee entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.EmployeeId);
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
        [RequirePermissions(Permission.EmployeeEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmailContact(EmployeeEmailContactInputViewModel modelInput)
        {
            Employee entity = null;

            try
            {
                entity = Repository.Get(modelInput.EmployeeId);
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
        [RequirePermissions(Permission.EmployeeEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmailContact(EmployeeEmailContactDisplayViewModel modelDisplay)
        {
            Employee entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.EmployeeId);
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


        [Route("edit/{key}/administrativeroles")]
        [RequirePermissions(Permission.EmployeeEditor)]
        public IActionResult AdministrativeRoles(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var roles = administrativeRoleRepository.GetAll().ToArray();

                var resultModel = new EmployeeAdministrativeRolesInputViewModel
                {
                    SelectableRoles = roles.Select(role => new SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<Guid>
                    {
                        Id = role.Id,
                        Label = role.Name,
                        Description = role.Description,
                        Selected = entity.EmployeeAdministrativeRoleBridges.Any(bridge => bridge.RoleId == role.Id)
                    }).ToList(),
                    EmployeeId = key

                };                

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [RequirePermissions(Permission.EmployeeEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddadministrativeRoles(EmployeeAdministrativeRolesInputViewModel modelInput)
        {
            Employee entity = null;

            try
            {
                entity = Repository.Get(modelInput.EmployeeId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }
            if (ModelState.IsValid)
            {

                entity.EmployeeAdministrativeRoleBridges.Clear();

                foreach (var admRolId in modelInput.SelectableRoles.Where(r => r.Selected == true).Select(e => e.Id))
                {
                    var admRole = this.administrativeRoleRepository.Get(admRolId);
                   
                    var EmployeeAdministrativeRoleBridge = new EmployeeAdministrativeRoleBridge { EmployeeId = entity.Id, RoleId = admRolId, Role = admRole, Employee = entity };
                    entity.EmployeeAdministrativeRoleBridges.Add(EmployeeAdministrativeRoleBridge);

                }

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

            return RedirectToAction("administrativeroles", new { key = entity.Id });
        }
    }
}
