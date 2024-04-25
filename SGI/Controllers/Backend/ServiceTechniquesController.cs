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
    [Area("complements")]
    public class ServiceTechniquesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ServiceTechnique, System.Guid, ServiceTechniqueInputViewModel, ServiceTechniqueDisplayViewModel>
    {
        public ServiceTechniquesController(IEntityRepository<ServiceTechnique, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ServiceTechniquesController> localizer, ILogger<ServiceTechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ServiceTechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ServiceTechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ServiceTechniqueCreator)]
        public override IActionResult Create(ServiceTechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ServiceTechniqueEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ServiceTechniqueEditor)]
        public override IActionResult Edit(ServiceTechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ServiceTechniqueRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ServiceTechniqueRemover)]
        public override IActionResult Delete(ServiceTechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        #region Translations
        [Route("edit/{key}/translations")]
        [RequirePermissions(Security.Authorization.Permission.ServiceTechniqueEditor)]
        public ActionResult EditTranslations(System.Guid key)
        {
            var languages = new List<Persistence.Enumerations.TranslatableLanguage>();

            foreach (Persistence.Enumerations.TranslatableLanguage language in Enum.GetValues(typeof(Persistence.Enumerations.TranslatableLanguage)))
            {
                languages.Add(language);
            }

            var entity = Repository.Get(key);

            var languagesSelected = entity.Translations.Select(l => l.TranslatableLanguage).ToArray();

            var resultModel = new ServiceTechniqueTranslationManagementViewModel
            {
                ServiceTechnique = new ServiceTechniqueDisplayViewModel(),
                NotSelectedLanguages = languages.Except(languagesSelected).ToArray(),
                Translations = entity.Translations.Select(item =>
                {
                    var model = new ServiceTechniqueTranslationDisplayViewModel
                    {
                        TranslatableLanguage = item.TranslatableLanguage,
                        Name = item.Name,
                        Description = item.Description
                    };

                    return model;
                }).ToArray()
            };

            resultModel.ServiceTechnique.Import(entity);

            return View(resultModel);
        }

        [Route("edit/translations/add")]
        [RequirePermissions(Security.Authorization.Permission.ServiceTechniqueEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTranslation(ServiceTechniqueTranslationInputViewModel modelInput)
        {
            ServiceTechnique entity = null;

            try
            {
                entity = Repository.Get(modelInput.ServiceTechniqueId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }


            if (ModelState.IsValid)
            {
                entity.Translations.Add(new ServiceTechniqueTranslation
                {
                    ServiceTechniqueId = entity.Id,
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
        [RequirePermissions(Security.Authorization.Permission.ServiceTechniqueEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyTranslation(ServiceTechniqueTranslationInputViewModel modelInput)
        {
            ServiceTechnique entity = null;

            try
            {
                entity = Repository.Get(modelInput.ServiceTechniqueId);
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
        [RequirePermissions(Security.Authorization.Permission.ServiceTechniqueEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTranslation(System.Guid serviceTechniqueId, Persistence.Enumerations.TranslatableLanguage translatableLanguage)
        {
            ServiceTechnique entity = null;

            try
            {
                entity = Repository.Get(serviceTechniqueId);
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
