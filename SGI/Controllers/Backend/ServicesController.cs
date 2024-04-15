using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class ServicesController : AbstractEntityManagementController<InstitutionalService, Guid, ServiceInputViewModel, ServiceDisplayViewModel>
    {
        public ServicesController(IEntityRepository<InstitutionalService, Guid> repository, IStringLocalizer<ServicesController> localizer, ILogger<ServicesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ServiceViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ServiceCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ServiceCreator)]
        public override IActionResult Create(ServiceInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ServiceEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ServiceEditor)]
        public override IActionResult Edit(ServiceInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ServiceRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ServiceRemover)]
        public override IActionResult Delete(ServiceDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        #region Language
        [Route("edit/{key}/languages")]
        [RequirePermissions(Permission.ServiceEditor)]
        public ActionResult EditLanguages(System.Guid key)
        {
            List<Persistence.Enumerations.TranslatableLanguage> languages = new List<Persistence.Enumerations.TranslatableLanguage>();

            foreach (Persistence.Enumerations.TranslatableLanguage language in Enum.GetValues(typeof(Persistence.Enumerations.TranslatableLanguage)))
            {
                languages.Add(language);
            }

            var entity = Repository.Get(key);

            var languagesSelected = entity.Translations.Select(l => l.TranslatableLanguage).ToArray();

            var notLanguagesSelected = languages.Except(languagesSelected).ToArray();

            var resultModel = new InstitutionalServiceLanguageManagerViewModel
            {
                Service = new ServiceDisplayViewModel()
            };

            resultModel.Service.Import(entity);

            resultModel.NotSelectedLanguage = notLanguagesSelected.Select(n => new SelectListItem { Value = n.ToString(), Text = n.ToString() }).ToArray();

            var translations = entity.Translations;

            resultModel.Translations = translations.Select(item =>
            {
                var model = new InstitutionalServiceTranslationDisplayViewModel
                {
                    InstitutionalServiceLanguege = new InstitutionalServiceLanguageDisplayViewModel
                    {
                        InstitutionalServiceId = item.InstitutionalServiceId,
                        Name = item.Name,
                        TranslatableLanguage = item.TranslatableLanguage
                    }
                };

                model.Import(item);

                return model;
            }).ToArray();

            return View(resultModel);
        }

        [Route("edit/languages/add")]
        [RequirePermissions(Permission.ServiceEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLanguage(InstitutionsLanguagesInputViewModel modelInput)
        {
            InstitutionalService entity = null;

            try
            {
                entity = Repository.Get(modelInput.GeneralId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }


            if (ModelState.IsValid)
            {
                entity.Translations.Add(new InstitutionalServiceTranslation { InstitutionalServiceId = entity.Id, TranslatableLanguage = modelInput.TranslatableLanguage, Name = modelInput.TranslatableLanguage.ToString() });

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

            return RedirectToAction("EditLanguages", new { key = entity.Id });
        }

        [Route("edit/languages/remove")]
        [RequirePermissions(Permission.ServiceEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLanguage(System.Guid InstitutionalServiceId, TranslatableLanguage translatableLanguage)
        {
            InstitutionalService entity = null;

            try
            {
                entity = Repository.Get(InstitutionalServiceId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Translations.First(b => b.InstitutionalServiceId == InstitutionalServiceId && b.TranslatableLanguage == translatableLanguage);

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

            return RedirectToAction("EditLanguages", new { key = entity.Id });
        }

        #endregion
    }
}
