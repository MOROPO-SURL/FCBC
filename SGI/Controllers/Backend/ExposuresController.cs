using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.Collections.Generic;
using System;
using System.Linq;
using MOROPO.FCBC.SGI.Security.Authorization;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class ExposuresController : SAPRESSC.Core.Web.Backend.Controllers.AbstractHabilitableEntityManagementController<Exposure, System.Guid, ExposureInputViewModel, ExposureDisplayViewModel>
    {
        private readonly IEntityRepository<Employee, System.Guid> employeeRepository;
        private readonly IEntityRepository<Exhibit, System.Guid> exhibitRepository;
        private readonly IEntityRepository<ArtWork, System.Guid> artWorkRepository;

        public ExposuresController(IEntityRepository<ArtWork, System.Guid> artWorkRepository, IEntityRepository<Exhibit, System.Guid> exhibitRepository, IEntityRepository<Employee, System.Guid> employeeRepository, IEntityRepository<Exposure, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ExposuresController> localizer, ILogger<ExposuresController> logger) : base(repository, localizer, logger)
        {
            this.employeeRepository = employeeRepository;
            this.exhibitRepository = exhibitRepository;
            this.artWorkRepository = artWorkRepository;
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureCreator)]
        public override IActionResult Create(ExposureInputViewModel model)
        {
            return base.Create(model);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public override IActionResult Edit(ExposureInputViewModel model)
        {
            return base.Edit(model);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public override IActionResult Enable(System.Guid key)
        {
            return base.Enable(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public override IActionResult Disable(System.Guid key)
        {
            return base.Disable(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureRemover)]
        public override IActionResult Delete(ExposureDisplayViewModel model)
        {
            return base.Delete(model);
        }

        protected override void PreCreate(Exposure entity, ExposureInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.GalleryId = Persistence.Enumerations.GalleryId.Amalia;
        }

        #region Curatorship

        [HttpGet]
        [Route("edit/{key}/curatorship")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public IActionResult EditEmployees(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var members = entity.EmployeeCuratorshipBridges.Select(b => b.Employee).ToArray();

                var notMemberEmployees = employeeRepository.GetAll().ToArray();

                if (members.Count() != 0)
                {
                    notMemberEmployees = notMemberEmployees.Except(members).ToArray();
                }

                var resultModel = new EditExposureCuratorshipViewModel
                {
                    Exposure = new ExposureDisplayViewModel()
                };

                resultModel.Exposure.Import(entity);

                resultModel.AvailableEmployeeSelectItems = notMemberEmployees.Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = n.Id.ToString(), Text = n.Identity.Name + " " + n.Identity.LastName }).ToArray();

                resultModel.Employees = members.Select(item =>
                {
                    var model = new EmployeeDisplayViewModel();

                    model.Import(item);

                    return model;
                }).ToArray();

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/curatorship/add")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult AddCurator(System.Guid key, System.Guid employeeId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    entity.EmployeeCuratorshipBridges.Add(new ExposureEmployeeCuratorshipBridge
                    {
                        EmployeeId = employeeId,
                        Exposure = entity
                    });

                    Repository.Update(entity, User.Identity.Name);

                    SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                    return RedirectToAction("editemployees", new { key = entity.Id });
                }

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editemployees", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/curatorship/remove")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveCurator(System.Guid key, System.Guid employeeId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.EmployeeCuratorshipBridges.First(b => b.EmployeeId == employeeId);

                entity.EmployeeCuratorshipBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editemployees", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editemployees", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region Exhibits
        [HttpGet]
        [Route("edit/{key}/exhibits")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public IActionResult EditExhibits(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var items = exhibitRepository.FindBy(ent1ty => ent1ty.ExposureId == key).ToArray();

                var includeArtWorks = new System.Collections.Generic.List<ArtWork>();

                foreach (var item in items)
                {
                    includeArtWorks.AddRange(item.ArtWorkBridges.Select(_item => _item.ArtWork).ToArray());
                }

                var noIncludeIndexes = new System.Collections.Generic.List<int>();

                for (int i = 1; i <= 18; i++)
                {
                    if (items.Any(ent1ty => ent1ty.Index == i)) continue;

                    noIncludeIndexes.Add(i);
                }

                var notIncludeArtWorks = artWorkRepository.FindBy(ent1ty => ent1ty.FeatureImageId != null).ToArray();

                if (includeArtWorks.Count != 0)
                {
                    notIncludeArtWorks = notIncludeArtWorks.Except(includeArtWorks).ToArray();
                }

                var resultModel = new EditExposureExhibitsViewModel
                {
                    Exposure = new ExposureDisplayViewModel()
                };

                resultModel.Exposure.Import(entity);

                resultModel.AvailableIdexes = noIncludeIndexes.Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = n.ToString(), Text = n.ToString() }).ToArray();
                resultModel.AvailableArtWorks = notIncludeArtWorks.Select(n => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = n.Id.ToString(), Text = n.Title }).ToArray();

                resultModel.Exhibits = items.OrderBy(item => item.Index).Select(item =>
                {
                    var model = new ExhibitDisplayViewModel();

                    model.Import(item);

                    var mainArtWork = item.ArtWorkBridges.First().ArtWork;

                    model.ArtWork = new ArtWorkDisplayViewModel
                    {
                        Title = mainArtWork.Title,
                        FeatureImageId = mainArtWork.FeatureImageId,
                        FeatureImageExtension = mainArtWork.FeatureImage?.ContentType.ContentTypeToExtension()
                    };

                    return model;
                }).ToArray();

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/exhibits/add")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult AddExhibit(System.Guid key, System.Guid workId, int index)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    var target = new Exhibit
                    {
                        Index = index,
                        ExposureId = entity.Id
                    };

                    target.ArtWorkBridges.Add(new ExhibitArtWorkBridge
                    {
                        ArtWorkId = workId,
                        Exhibit = target
                    });

                    exhibitRepository.Add(target, User.Identity.Name);

                    SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                    return RedirectToAction("editexhibits", new { key = entity.Id });
                }

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editexhibits", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/exhibits/remove")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveExhibit(System.Guid key, System.Guid exhibitId)
        {
            try
            {
                var entity = Repository.Get(key);

                var target = exhibitRepository.Get(exhibitId);

                if (target.ExposureId != entity.Id)
                {
                    throw new System.InvalidOperationException();
                }

                exhibitRepository.Delete(target, User.Identity.Name);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editexhibits", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editexhibits", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region Language
        [Route("edit/{key}/languages")]
        [RequirePermissions(Permission.ExposureEditor)]
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

            var resultModel = new ExposureLanguageManagerViewModel
            {
                Exposure = new ExposureDisplayViewModel()
            };

            resultModel.Exposure.Import(entity);

            resultModel.NotSelectedLanguage = notLanguagesSelected.Select(n => new SelectListItem { Value = n.ToString(), Text = n.ToString() }).ToArray();

            var translations = entity.Translations;

            resultModel.Translations = translations.Select(item =>
            {
                var model = new ExposureTranslationDisplayViewModel
                {
                    ExposureLanguege = new ExposureLanguageDisplayViewModel
                    {
                        ExposureId = item.ExposureId,
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
        [RequirePermissions(Permission.ExposureEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLanguage(InstitutionsLanguagesInputViewModel modelInput)
        {
            Exposure entity = null;

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
                entity.Translations.Add(new ExposureTranslation { ExposureId = entity.Id, TranslatableLanguage = modelInput.TranslatableLanguage, Name = modelInput.TranslatableLanguage.ToString() });

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
        [RequirePermissions(Permission.ExposureEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLanguage(System.Guid ExposureId, TranslatableLanguage translatableLanguage)
        {
            Exposure entity = null;

            try
            {
                entity = Repository.Get(ExposureId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            try
            {
                var bridge = entity.Translations.First(b => b.ExposureId == ExposureId && b.TranslatableLanguage == translatableLanguage);

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
