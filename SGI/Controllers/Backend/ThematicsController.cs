using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class ThematicsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Thematic, System.Guid, ThematicInputViewModel, ThematicDisplayViewModel>
    {
        public ThematicsController(IEntityRepository<Thematic, System.Guid> repository, IStringLocalizer<ThematicsController> localizer, ILogger<ThematicsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ThematicViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ThematicCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ThematicCreator)]
        public override IActionResult Create(ThematicInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ThematicEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ThematicEditor)]
        public override IActionResult Edit(ThematicInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.ThematicRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ThematicRemover)]
        public override IActionResult Delete(ThematicDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        #region Translations
        [Route("edit/{key}/translations")]
        [RequirePermissions(Permission.ThematicEditor)]
        public ActionResult EditTranslations(System.Guid key)
        {
            List<Persistence.Enumerations.TranslatableLanguage> languages = new List<Persistence.Enumerations.TranslatableLanguage>();

            foreach (Persistence.Enumerations.TranslatableLanguage language in Enum.GetValues(typeof(Persistence.Enumerations.TranslatableLanguage)))
            {
                languages.Add(language);
            }

            var entity = Repository.Get(key);

            var languagesSelected = entity.Translations.Select(l => l.TranslatableLanguage).ToArray();

            var resultModel = new ThematicLanguageManagerViewModel
            {
                Thematic = new ThematicDisplayViewModel(),
                NotSelectedLanguage = languages.Except(languagesSelected).ToArray(),
                Translations = entity.Translations.Select(item =>
                {
                    var model = new ThematicTranslationDisplayViewModel
                    {
                        TranslatableLanguage = item.TranslatableLanguage,
                        Name = item.Name,
                        Description = item.Description
                    };
                    return model;
                }).ToArray()
            };

            resultModel.Thematic.Import(entity);

            return View(resultModel);
        }

        [Route("edit/translations/add")]
        [RequirePermissions(Permission.ThematicEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTranslation(ThematicLanguagesInputViewModel modelInput)
        {
            Thematic entity = null;

            try
            {
                entity = Repository.Get(modelInput.WorkId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }


            if (ModelState.IsValid)
            {
                entity.Translations.Add(new ThematicTranslation
                {
                    ThematicId = entity.Id,
                    TranslatableLanguage = modelInput.TranslatableLanguage,
                    Name = modelInput.Name,
                    Description = modelInput.Description
                });

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

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        [Route("edit/translations/modify")]
        [RequirePermissions(Security.Authorization.Permission.ThematicEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyTranslation(ThematicLanguagesInputViewModel modelInput)
        {
            Thematic entity = null;

            try
            {
                entity = Repository.Get(modelInput.WorkId);
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
        [RequirePermissions(Permission.ThematicEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTranslation(System.Guid ThematicId, TranslatableLanguage translatableLanguage)
        {
            Thematic entity = null;

            try
            {
                entity = Repository.Get(ThematicId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Translations.First(b => b.ThematicId == ThematicId && b.TranslatableLanguage == translatableLanguage);

                entity.Translations.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
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
