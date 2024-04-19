using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("manifestations")]
    public class ServiceLinesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ServiceLine, Guid, ServiceLineInputViewModel, ServiceLineDisplayViewModel>
    {
        public ServiceLinesController(IEntityRepository<ServiceLine, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ServiceLinesController> localizer, ILogger<ServiceLinesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ServiceLineViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ServiceLineCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ServiceLineCreator)]
        public override IActionResult Create(ServiceLineInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ServiceLineEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ServiceLineEditor)]
        public override IActionResult Edit(ServiceLineInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ServiceLineRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ServiceLineRemover)]
        public override IActionResult Delete(ServiceLineDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        #region Translations
        [Route("edit/{key}/translations")]
        [RequirePermissions(Security.Authorization.Permission.ServiceLineEditor)]
        public ActionResult EditTranslations(System.Guid key)
        {
            var languages = new List<Persistence.Enumerations.TranslatableLanguage>();

            foreach (Persistence.Enumerations.TranslatableLanguage language in Enum.GetValues(typeof(Persistence.Enumerations.TranslatableLanguage)))
            {
                languages.Add(language);
            }

            var entity = Repository.Get(key);

            var languagesSelected = entity.Translations.Select(l => l.TranslatableLanguage).ToArray();

            var resultModel = new ServiceLineTranslationsManagementViewModel
            {
                ServiceLine = new ServiceLineDisplayViewModel(),
                NotSelectedLanguages = languages.Except(languagesSelected).ToArray(),
                Translations = entity.Translations.Select(item =>
                {
                    var model = new ServiceLineTranslationDisplayViewModel
                    {
                        TranslatableLanguage = item.TranslatableLanguage,
                        Name = item.Name,
                        Description = item.Description
                    };

                    return model;
                }).ToArray()
            };

            resultModel.ServiceLine.Import(entity);

            return View(resultModel);
        }

        [Route("edit/translations/add")]
        [RequirePermissions(Security.Authorization.Permission.ServiceLineEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTranslation(ServiceLineTranslationInputViewModel modelInput)
        {
            ServiceLine entity = null;

            try
            {
                entity = Repository.Get(modelInput.ServiceLineId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }


            if (ModelState.IsValid)
            {
                entity.Translations.Add(new ServiceLineTranslation
                {
                    ServiceLineId = entity.Id,
                    TranslatableLanguage = modelInput.TranslatableLanguage,
                    Name = modelInput.Name,
                    Description = modelInput.Description
                });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "EditionSuccessMessage", null);
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

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        [Route("edit/translations/modify")]
        [RequirePermissions(Security.Authorization.Permission.ServiceLineEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyTranslation(ServiceLineTranslationInputViewModel modelInput)
        {
            ServiceLine entity = null;

            try
            {
                entity = Repository.Get(modelInput.ServiceLineId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }


            if (ModelState.IsValid)
            {
                var translation = entity.Translations.First(item => item.TranslatableLanguage == modelInput.TranslatableLanguage);

                translation.Name = modelInput.Name;
                translation.Description = modelInput.Description;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "EditionSuccessMessage", null);
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

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        [Route("edit/translations/remove")]
        [RequirePermissions(Security.Authorization.Permission.ServiceLineEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTranslation(System.Guid serviceLineId, Persistence.Enumerations.TranslatableLanguage translatableLanguage)
        {
            ServiceLine entity = null;

            try
            {
                entity = Repository.Get(serviceLineId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }

            try
            {
                var bridge = entity.Translations.First(item => item.TranslatableLanguage == translatableLanguage);

                entity.Translations.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, null, "EditionSuccessMessage", null);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, null, "EditionWarningMessage", null);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        #endregion
    }
}
