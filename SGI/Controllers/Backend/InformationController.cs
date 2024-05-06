using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Route("management/[area]/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[area]/[controller]")]
    [Area("institution")]
    public class InformationController : SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<InstitutionalInformation, InstitutionalInformationKind> institutionalInformationRepository;

        public InformationController(IEntityRepository<InstitutionalInformation, InstitutionalInformationKind> institutionalInformationRepository, Microsoft.Extensions.Localization.IStringLocalizer<InformationController> localizer, ILogger<InformationController> logger) : base(localizer, logger)
        {
            this.institutionalInformationRepository = institutionalInformationRepository;
        }

        [RequirePermissions(Security.Authorization.Permission.InstitutionalInformationViewer)]
        public IActionResult Index()
        {
            var entities = institutionalInformationRepository.GetAll().ToArray();

            var model = new Models.View.InstitutionInformationDisplayViewModel 
            {
                SocialObject = entities.First(entity => entity.Id == InstitutionalInformationKind.SocialObject).Value,
                Mission = entities.First(entity => entity.Id == InstitutionalInformationKind.Mission).Value,
                Vision = entities.First(entity => entity.Id == InstitutionalInformationKind.Vision).Value,
                Histoy = entities.First(entity => entity.Id == InstitutionalInformationKind.History).Value
            };

            return View(model);
        }

        [HttpGet]
        [RequirePermissions(Security.Authorization.Permission.InstitutionalInformationEditor)]
        [Route("[action]")]
        public virtual IActionResult Edit()
        {
            try
            {
                var socialObjectEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.SocialObject);
                var missionEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.Mission);
                var visionEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.Vision);
                var historyEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.History);

                var modelInput = new Models.View.InstitutionInformationInputViewModel
                {
                    SocialObject = socialObjectEntity.Value,
                    Mission = missionEntity.Value,
                    Vision = visionEntity.Value,
                    Histoy = historyEntity.Value
                };

                return View(modelInput);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, null, "NotFoundMessage", null);

            return Redirect("/");
        }

        [HttpPost]
        [RequirePermissions(Security.Authorization.Permission.InstitutionalInformationEditor)]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(Models.View.InstitutionInformationInputViewModel modelInput)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var socialObjectEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.SocialObject);
                    var missionEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.Mission);
                    var visionEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.Vision);
                    var historyEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.History);

                    socialObjectEntity.Value = modelInput.SocialObject;
                    missionEntity.Value = modelInput.Mission;
                    visionEntity.Value = modelInput.Vision;
                    historyEntity.Value = modelInput.Histoy;

                    institutionalInformationRepository.Update(socialObjectEntity, User.Identity.Name, false);
                    institutionalInformationRepository.Update(missionEntity, User.Identity.Name, false);
                    institutionalInformationRepository.Update(visionEntity, User.Identity.Name, false);
                    institutionalInformationRepository.Update(historyEntity, User.Identity.Name, true);

                    SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Success, null, "EditionSuccessMessage", null);

                    return RedirectToAction("index");
                }

                catch (System.Exception e)
                {
                    if (!(e.InnerException is SAPRESSC.Core.Web.Frontend.InvalidModelException))
                    {
                        logger.LogError(e.InnerException, e.Message);

                        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, null, "NotFoundMessage", null);

                        return Redirect("/");
                    }
                }
            }

            SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, null, "InvalidMessage", null);

            return View(modelInput);
        }

        #region Translations
        [Route("edit/{key}/translations")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        public ActionResult EditTranslations(Persistence.Enumerations.InstitutionalInformationKind key)
        {
            var languages = new List<Persistence.Enumerations.TranslatableLanguage>();

            foreach (Persistence.Enumerations.TranslatableLanguage language in Enum.GetValues(typeof(Persistence.Enumerations.TranslatableLanguage)))
            {
                languages.Add(language);
            }

            var entity = institutionalInformationRepository.Get(key);

            var languagesSelected = entity.Translations.Select(l => l.TranslatableLanguage).ToArray();

            var resultModel = new InstitutionalInformationTranslationsManagementViewModel
            {
                InstitutionalInformation = new InstitutionalInformationDisplayViewModel(),
                NotSelectedLanguages = languages.Except(languagesSelected).ToArray(),
                Translations = entity.Translations.Select(item =>
                {
                    var model = new InstitutionalInformationTranslationDisplayViewModel
                    {
                        TranslatableLanguage = item.TranslatableLanguage,
                        Mission = item.Mission,
                        SocialObject = item.SocialObject,
                        Vision = item.Vision,
                        History = item.History
                    };

                    return model;
                }).ToArray()
            };

            resultModel.InstitutionalInformation.Import(entity);

            return View(resultModel);
        }

        [Route("edit/translations/add")]
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTranslation(InstitutionalInformationTranslationInputViewModel modelInput)
        {
            InstitutionalInformation entity = null;

            try
            {
                entity = institutionalInformationRepository.Get(modelInput.InstitutionalInformationKindId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }


            if (ModelState.IsValid)
            {
                entity.Translations.Add(new InstitutionalInformationTranslation
                {
                    InstitutionalInformationKind = entity.Id,
                    TranslatableLanguage = modelInput.TranslatableLanguage,
                    Mission = modelInput.Mission,
                    SocialObject = modelInput.SocialObject,
                    History = modelInput.History,
                    Vision = modelInput.Vision
                });

                institutionalInformationRepository.Update(entity, User.Identity.Name);

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
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyTranslation(InstitutionalInformationTranslationInputViewModel modelInput)
        {
            InstitutionalInformation entity = null;

            try
            {
                entity = institutionalInformationRepository.Get(modelInput.InstitutionalInformationKindId);
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

                translation.SocialObject = modelInput.SocialObject;
                translation.Vision = modelInput.Vision;
                translation.History = modelInput.History;
                translation.Mission = modelInput.Mission;

                institutionalInformationRepository.Update(entity, User.Identity.Name);

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
        [RequirePermissions(Security.Authorization.Permission.GalleryEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTranslation(Persistence.Enumerations.InstitutionalInformationKind InstitutionalInformationId, TranslatableLanguage translatableLanguage)
        {
            InstitutionalInformation entity = null;

            try
            {
                entity = institutionalInformationRepository.Get(InstitutionalInformationId);
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

                institutionalInformationRepository.Update(entity, User.Identity.Name);

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
