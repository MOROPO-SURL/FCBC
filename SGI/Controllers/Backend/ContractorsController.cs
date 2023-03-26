using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.RegionConst;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class ContractorsController : AbstractEntityManagementController<Contractor, Guid, ContractorInputViewModel, ContractorDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;
        private readonly IEntityRepository<ContractorKind, int> contractKindRepository;        
        private readonly IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository;
        private readonly IEntityRepository<State, Guid> stateRepository;
        private readonly IEntityRepository<Municipality, Guid> municipalityRepository;
        private readonly Managers.RelativeAttachmentFileManager attachmentFileManager;
        private readonly IEntityRepository<ContractorAttachment, System.Guid> attachmentRepository;

        public ContractorsController(IEntityRepository<ContractorAttachment, System.Guid> attachmentRepository, Managers.RelativeAttachmentFileManager attachmentFileManager, IEntityRepository<Contractor, Guid> repository, IStringLocalizer<ContractorsController> localizer, ILogger<ContractorsController> logger, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IEntityRepository<ContractorKind, int> contractKindRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository,
             IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository, IEntityRepository<State, Guid> stateRepository, IEntityRepository<Municipality, Guid> municipalityRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.contractKindRepository = contractKindRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;            
            this.municipalityRegionRepository = municipalityRegionRepository;
            this.stateRepository = stateRepository;
            this.municipalityRepository = municipalityRepository;
            this.attachmentFileManager = attachmentFileManager;
            this.attachmentRepository = attachmentRepository;
        }

        [RequirePermissions(Permission.ContractorViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ContractorCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ContractorCreator)]
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

        [RequirePermissions(Permission.ContractorEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ContractorEditor)]
        public override IActionResult Edit(ContractorInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ContractorRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
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

        [Route("edit/{key}/contacts")]
        [RequirePermissions(Permission.ContractorEditor)]
        public IActionResult Contacts(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new ContractorContactsManageViewModel
                {
                    Contractor = new ContractorDisplayViewModel(),
                    PhoneContacts = entity.Identity.OrganizationPhoneContactItemBridges.Select(bridge => new ContractorPhoneContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Number = bridge.ContactItem.PhoneNumber,
                        ContractorId = entity.Id
                    }).ToList(),
                    EmailContacts = entity.Identity.OrganizationEmailContactItemBridges.Select(bridge => new ContractorEmailContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        Address = bridge.ContactItem.EmailAddress,
                        ContractorId = entity.Id
                    }).ToList(),
                    SocialContacts = entity.Identity.OrganizationSocialContactItemBridges.Select(bridge => new ContractorSocialContactDisplayViewModel
                    {
                        Id = bridge.ContactItemId,
                        Label = bridge.ContactItem.Label,
                        SocialId = bridge.ContactItem.SocialId,
                        ContractorId = entity.Id,
                        Icon = GetIcon(bridge.ContactItem.SocialId)
                    }).ToList()
                };

                resultModel.Contractor.Import(entity);

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }
        
        [RequirePermissions(Permission.ContractorRemover)]
        public override IActionResult Delete(ContractorDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateWithContractKind(ContractorInputViewModel modelInput)
        {
            modelInput.ContractKinds = contractKindRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Name == RegionInitConstant.CMG_STATE_NAME }).OrderByDescending(en => en.Selected).ToArray();

            modelInput.Municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == RegionInitConstant.MY_OWN_SATE).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        private void PopulateWithContractKindEdit(ContractorInputViewModel modelInput)
        {
            modelInput.ContractKinds = contractKindRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();

            modelInput.StateId = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId).StateId;

            modelInput.Municipalities = municipalityRepository.GetAll().Where(s => s.StateId == modelInput.StateId).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.MunicipalityId }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.StateId }).ToArray();

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
            PopulateWithContractKindEdit(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Contractor entity, ContractorInputViewModel modelInput)
        {
            PopulateWithContractKind(modelInput);
        }

        protected override void PreCreate(Contractor entity, ContractorInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.ContractorKind = contractKindRepository.Get(modelInput.ContractKindId);

            var region = municipalityRegionRepository.FindOneBy(r => r.MunicipalityId == modelInput.MunicipalityId);
            PostalContactItem postalContact = new PostalContactItem
            {
                Address = modelInput.AddressPostal,
                RegionId = region.Id,
                Region = region
            };

            OrganizationPostalContactItemBridge organizationPostalContactItemBridge = new OrganizationPostalContactItemBridge
            {
                OrganizationId = modelInput.Export().Identity.Id,
                Organization = modelInput.Export().Identity,
                ContactItemId = postalContact.Id,
                ContactItem = postalContact
            };
            entity.Identity.OrganizationPostalContactItemBridges.Add(organizationPostalContactItemBridge);


            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Contractor " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PreEdit(Contractor entity, ContractorInputViewModel modelInput)
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

            if (entity.ContractorKind.Id != modelInput.ContractKindId)
            {
                entity.ContractorKind = contractKindRepository.Get(modelInput.ContractKindId);
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

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Contractor " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PostDelete(Contractor entity)
        {
            base.PostDelete(entity);

            organizationIdentityRepository.Delete(entity.Identity, User.Identity.Name);

            if (entity.Identity.Image == null) return;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);
        }

        [Route("edit/contacts/phone/add")]
        [RequirePermissions(Permission.ContractorEditor)]
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
                SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.Identity.OrganizationPhoneContactItemBridges.Add(new OrganizationPhoneContactItemBridge { OrganizationId = entity.IdentityId, ContactItem = new PhoneContactItem { Label = modelInput.Label, PhoneNumber = modelInput.Number } });

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
        [RequirePermissions(Permission.ContractorEditor)]
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
        [RequirePermissions(Permission.ContractorEditor)]
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
        [RequirePermissions(Permission.ContractorEditor)]
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
        [RequirePermissions(Permission.ContractorEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSocialContact(ContractorSocialContactInputViewModel modelInput)
        {
            Contractor entity = null;

            try
            {
                entity = Repository.Get(modelInput.ContractorId);
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
        [RequirePermissions(Permission.ContractorEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSocialContact(ContractorSocialContactDisplayViewModel modelDisplay)
        {
            Contractor entity = null;

            try
            {
                entity = Repository.Get(modelDisplay.ContractorId);
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

        #region Attachments

        [HttpGet]
        [Route("edit/{key}/attachments")]
        [RequirePermissions(Permission.ContractorEditor)]
        public ActionResult EditAttachments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ContractorAttachmentsDisplayViewModel
            {
                ContractorDisplay = new ContractorDisplayViewModel(),
                Attachments = entity.Attachments.Select(item =>
                {
                    var result = new AttachmentDisplayViewModel();

                    result.Import(item);

                    return result;
                }).ToArray()
            };

            model.ContractorDisplay.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/attachments/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ContractorEditor)]
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

                            var attachment = new ContractorAttachment
                            {
                                Name = model.Name,
                                IsPublic = model.IsPublic,
                                Description = model.Description,
                                ContentType = model.File.ContentType,
                                Length = model.File.Length,
                                ContractorId = entity.Id
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
        [RequirePermissions(Permission.ContractorEditor)]
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
        [RequirePermissions(Permission.ContractorEditor)]
        public IActionResult EditAttachment(System.Guid key, AttachmentInputViewModel modelInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContractorAttachment val = attachmentRepository.Get(modelInput.Id);
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

        [HttpPost]
        [Route("edit/{key}/{attachmentId}/attachments/download")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission.ContractorEditor)]
        public FileResult DownloadAttachment(System.Guid key, System.Guid attachmentId)
        {

            var entity = Repository.Get(key);
            var attachment = entity.Attachments.FirstOrDefault(att => att.Id == attachmentId);

            var fileBytes = attachmentFileManager.GetFileContent(attachment);
            var extension = attachment.ContentType.Split('/');
            var fileName = attachment.Name + "." + extension[1];

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        #endregion

    }
}
