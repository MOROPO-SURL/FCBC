using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class ContractorsController : AbstractEntityManagementController<Contractor, Guid, ContractorInputViewModel, ContractorDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;
        private readonly IEntityRepository<ContractorKind, int> contractKindRepository;
        public ContractorsController(IEntityRepository<Contractor, Guid> repository, IStringLocalizer<ContractorsController> localizer, ILogger<ContractorsController> logger, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IEntityRepository<ContractorKind, int> contractKindRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.contractKindRepository = contractKindRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
        }

        [RequiresRoleBasePermission(Roles.ContractorViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ContractorCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ContractorCreator)]
        public override IActionResult Create(ContractorInputViewModel inputViewModel)
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

        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        public override IActionResult Edit(ContractorInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ContractorRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [Route("edit/{key}/contacts")]
        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        public IActionResult Contacts(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ContractorContactsManageViewModel
                {
                    Contractor = new ContractorDisplayViewModel(),
                    PhoneContacts = entity.OrganizationIdentity.OrganizationPhoneContactItemBridges.Select(bridge => new ContractorPhoneContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Number = bridge.ContactItem.PhoneNumber,
                        ContractorId = entity.Id
                    }).ToList(),
                    EmailContacts = entity.OrganizationIdentity.OrganizationEmailContactItemBridges.Select(bridge => new ContractorEmailContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Address = bridge.ContactItem.EmailAddress,
                        ContractorId = entity.Id
                    }).ToList()
                };

                resultModel.Contractor.Import(entity);

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }
        
        [RequiresRoleBasePermission(Roles.ContractorRemover)]
        public override IActionResult Delete(ContractorDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateWithContractKind(ContractorInputViewModel modelInput)
        {
            modelInput.ContractKinds = contractKindRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();
        }

        protected override void ConfigureCreate(ContractorInputViewModel modelInput)
        {
            PopulateWithContractKind(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ContractorInputViewModel modelInput)
        {
            PopulateWithContractKind(modelInput);
        }

        protected override void ConfigureEdit(Contractor entity, ContractorInputViewModel modelInput)
        {
            PopulateWithContractKind(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ContractorInputViewModel modelInput)
        {
            PopulateWithContractKind(modelInput);
        }

        protected override void PreCreate(Contractor entity, ContractorInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.ContractorKind = contractKindRepository.Get(modelInput.ContractKindId);

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Contractor " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PreEdit(Contractor entity, ContractorInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.ContractorKind.Id != modelInput.ContractKindId)
            {
                entity.ContractorKind = contractKindRepository.Get(modelInput.ContractKindId);
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

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Contractor " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PostDelete(Contractor entity)
        {
            base.PostDelete(entity);

            organizationIdentityRepository.Delete(entity.OrganizationIdentity, User.Identity.Name);

            if (entity.OrganizationIdentity.Image == null) return;

            imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.OrganizationIdentity.Image);
        }

        [Route("edit/contacts/phone/add")]
        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhoneContact(ContractorPhoneContactInputViewModel modelInput)
        {
            Contractor entity = null;

            try
            {
                entity = Repository.Get(modelInput.ContractorId);
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
        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePhoneContact(ContractorPhoneContactDisplayViewModel modelDisplay)
        {
            Contractor entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.ContractorId);
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
        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmailContact(ContractorEmailContactInputViewModel modelInput)
        {
            Contractor entity = null;

            try
            {
                entity = Repository.Get(modelInput.ContractorId);
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
        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmailContact(ContractorEmailContactDisplayViewModel modelDisplay)
        {
            Contractor entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.ContractorId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                //var bridge = entity.Identity.PersonEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);
                var bridge = entity.OrganizationIdentity.OrganizationEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                //entity.Identity.PersonEmailContactItemBridges.Remove(bridge);
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
