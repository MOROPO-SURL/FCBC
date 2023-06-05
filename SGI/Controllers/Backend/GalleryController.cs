using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Route("management/[area]/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[area]/[controller]")]
    [Area("institution")]
    public class GalleryController : SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Persistence.Entities.Gallery, Persistence.Enumerations.GalleryId> galleryRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;
        private readonly IEntityRepository<Persistence.Entities.Employee, System.Guid> employeeRepository;

        public GalleryController(IEntityRepository<Persistence.Entities.Employee, System.Guid> employeeRepository, IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Persistence.Entities.Gallery, Persistence.Enumerations.GalleryId> galleryRepository, Microsoft.Extensions.Localization.IStringLocalizer<GalleryController> localizer, ILogger<GalleryController> logger) : base(localizer, logger)
        {
            this.galleryRepository = galleryRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
            this.employeeRepository = employeeRepository;
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryViewer)]
        [HttpGet]
        public virtual IActionResult Index()
        {
            var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

            var modelInput = new Models.View.GalleryDisplayViewModel();

            modelInput.Import(entity);

            return View(modelInput);
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpGet]
        [Route("[action]")]
        public virtual IActionResult Edit()
        {
            var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

            var modelInput = new Models.View.GalleryInputViewModel();

            modelInput.Import(entity);

            return View(modelInput);
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(Models.View.GalleryInputViewModel model)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    model.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(model);

                    updateModelTask.Wait();
                }
            }

            try
            {
                var entity = galleryRepository.Get(model.Id);

                if (ModelState.IsValid)
                {
                    try
                    {
                        model.Mergue(entity);

                        try
                        {
                            PreEdit(entity, model);
                        }
                        catch (SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                        {
                            ModelState.AddModelError(e.ModelKey, e.ErrorMessage);

                            throw new System.InvalidOperationException(e.ErrorMessage, e);
                        }

                        galleryRepository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, null, "EditionSuccessMessage", null);

                        return RedirectToAction("edit");
                    }

                    catch (System.Exception e)
                    {
                        if (!(e.InnerException is SAPRESSC.Core.Web.Frontend.InvalidModelException))
                        {
                            logger.LogError(e.InnerException, e.Message);

                            SetDefaultNotification(NotificationType.Danger, null, "NotFoundMessage", null);

                            return RedirectToAction("index");
                        }
                    }
                }

                SetDefaultNotification(NotificationType.Danger, null, "InvalidMessage", null);

                return View(model);
            }
            catch (SAPRESSC.Core.Web.Common.Exceptions.NoContinuableActionException e)
            {
                SetDefaultNotification(e.Type, e.Title, e.Message, null);

                return RedirectToAction("index");
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        protected void PreEdit(Persistence.Entities.Gallery entity, Models.View.GalleryInputViewModel modelInput)
        {
            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.Identity.Image != null)
                {
                    imageFileManager.DeleteImageFile(entity.Identity.Image);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.Identity.Image != null)
            {
                imageFileManager.DeleteImageFile(entity.Identity.Image);

                imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Collaborator Organization " + entity.Id };

                imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        #region Contacts

        [Route("edit/contacts")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        public IActionResult Contacts()
        {
            var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

            var resultModel = new ArtistContactsManageViewModel
            {
                PhoneContacts = entity.Identity.OrganizationPhoneContactItemBridges.Select(bridge => new ArtistPhoneContactDisplayViewModel
                {
                    Id = bridge.ContactItemId,
                    Label = bridge.ContactItem.Label,
                    Number = bridge.ContactItem.PhoneNumber
                }).ToList(),
                EmailContacts = entity.Identity.OrganizationEmailContactItemBridges.Select(bridge => new ArtistEmailContactDisplayViewModel
                {
                    Id = bridge.ContactItemId,
                    Label = bridge.ContactItem.Label,
                    Address = bridge.ContactItem.EmailAddress
                }).ToList(),
                SocialContacts = entity.Identity.OrganizationSocialContactItemBridges.Select(bridge => new ArtistSocialContactDisplayViewModel
                {
                    Id = bridge.ContactItemId,
                    Label = bridge.ContactItem.Label,
                    SocialId = bridge.ContactItem.SocialId,
                    Icon = GetIcon(bridge.ContactItem.SocialId)
                }).ToList()
            };

            return View(resultModel);
        }

        [Route("edit/contacts/phone/add")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPhoneContact(ArtistPhoneContactInputViewModel modelInput)
        {
            if (ModelState.IsValid)
            {
                var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

                entity.Identity.OrganizationPhoneContactItemBridges.Add(new OrganizationPhoneContactItemBridge { OrganizationId = entity.IdentityId.Value, ContactItem = new PhoneContactItem { Label = modelInput.Label, PhoneNumber = modelInput.Number } });

                galleryRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "Cambios aplicados con éxito.", null);
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

            return RedirectToAction("contacts");
        }

        [Route("edit/contacts/phone/delete")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePhoneContact(ArtistPhoneContactDisplayViewModel modelDisplay)
        {
            try
            {
                var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

                var bridge = entity.Identity.OrganizationPhoneContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationPhoneContactItemBridges.Remove(bridge);

                galleryRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "Cambios aplicados con éxito.", null);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts");
        }

        [Route("edit/contacts/email/add")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmailContact(ArtistEmailContactInputViewModel modelInput)
        {
            if (ModelState.IsValid)
            {
                var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

                entity.Identity.OrganizationEmailContactItemBridges.Add(new OrganizationEmailContactItemBridge { OrganizationId = entity.IdentityId.Value, ContactItem = new EmailContactItem { Label = modelInput.Label, EmailAddress = modelInput.Address } });

                galleryRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "Cambios aplicados con éxito.", null);
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

            return RedirectToAction("contacts");
        }

        [Route("edit/contacts/email/delete")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmailContact(ArtistEmailContactDisplayViewModel modelDisplay)
        {
            try
            {
                var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

                var bridge = entity.Identity.OrganizationEmailContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationEmailContactItemBridges.Remove(bridge);

                galleryRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "Cambios aplicados con éxito.", null);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts");
        }

        [Route("edit/contacts/social/add")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSocialContact(ArtistSocialContactInputViewModel modelInput)
        {
            if (ModelState.IsValid)
            {
                var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

                entity.Identity.OrganizationSocialContactItemBridges.Add(new OrganizationSocialContactItemBridge { OrganizationId = entity.IdentityId.Value, ContactItem = new SocialContactItem { Label = modelInput.Label, SocialId = modelInput.SocialId } });

                galleryRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "Cambios aplicados con éxito.", null);
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

            return RedirectToAction("contacts");
        }

        [Route("edit/contacts/social/delete")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSocialContact(ArtistSocialContactDisplayViewModel modelDisplay)
        {
            try
            {
                var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

                var bridge = entity.Identity.OrganizationSocialContactItemBridges.First(b => b.ContactItemId == modelDisplay.Id);

                entity.Identity.OrganizationSocialContactItemBridges.Remove(bridge);

                galleryRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "Cambios aplicados con éxito.", null);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("contacts");
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

        #endregion

        #region Employees

        [Route("edit/employees")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        public IActionResult Employees()
        {
            var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

            var members = entity.EmployeeBridges.Select(b => b.Employee).ToArray();

            var notMemberArtist = employeeRepository.GetAll().ToArray();

            if (members.Count() != 0)
            {
                notMemberArtist = notMemberArtist.Except(members).ToArray();
            }

            var resultModel = new GalleryEmployeesManageViewModel
            {
                NotMemberSelectListItems = notMemberArtist.Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = n.Id.ToString(), Text = n.Identity.Name + " " + n.Identity.LastName }).ToArray(),
                Members = members.Select(item =>
                {
                    var model = new EmployeeDisplayViewModel();

                    model.Import(item);

                    return model;
                }).ToArray()
            };

            return View(resultModel);
        }

        [Route("edit/employees/add")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMember(System.Guid employeeId)
        {
            var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

            entity.EmployeeBridges.Add(new Persistence.Entities.GalleryEmployeeBridge { GalleryId = entity.Id, EmployeeId = employeeId });

            galleryRepository.Update(entity, User.Identity.Name);

            SetDefaultNotification(NotificationType.Success, null, "Cambios aplicados con éxito.", null);

            return RedirectToAction("employees");
        }

        [Route("edit/employees/remove")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveMember(System.Guid employeeId)
        {
            try
            {
                var entity = galleryRepository.Get(Persistence.Enumerations.GalleryId.Amalia);

                var bridge = entity.EmployeeBridges.First(b => b.EmployeeId == employeeId);

                entity.EmployeeBridges.Remove(bridge);

                galleryRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "Cambios aplicados con éxito.", null);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("employees");
        }

        #endregion
    }
}
