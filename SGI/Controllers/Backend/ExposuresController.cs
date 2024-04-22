using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.Linq;

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

                var notIncludeArtWorks = artWorkRepository.FindBy(_entity => _entity.FeatureImageId != null && _entity.Width != null && _entity.Height != null).ToArray();

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

        #region Translations
        [Route("edit/{key}/translations")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public ActionResult EditTranslations(System.Guid key)
        {
            var languages = new System.Collections.Generic.List<Persistence.Enumerations.TranslatableLanguage>();

            foreach (Persistence.Enumerations.TranslatableLanguage language in System.Enum.GetValues(typeof(Persistence.Enumerations.TranslatableLanguage)))
            {
                languages.Add(language);
            }

            var entity = Repository.Get(key);

            var languagesSelected = entity.Translations.Select(l => l.TranslatableLanguage).ToArray();

            var resultModel = new ExposureTranslationsManagementViewModel
            {
                Exposure = new ExposureDisplayViewModel(),
                NotSelectedLanguages = languages.Except(languagesSelected).ToArray(),
                Translations = entity.Translations.Select(item =>
                {
                    var model = new ExposureTranslationDisplayViewModel
                    {
                        TranslatableLanguage = item.TranslatableLanguage,
                        Name = item.Name,
                        Description = item.Description
                    };

                    return model;
                }).ToArray()
            };

            resultModel.Exposure.Import(entity);

            return View(resultModel);
        }

        [Route("edit/translations/add")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTranslation(ExposureTranslationInputViewModel modelInput)
        {
            Exposure entity = null;

            try
            {
                entity = Repository.Get(modelInput.ExposureId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }


            if (ModelState.IsValid)
            {
                entity.Translations.Add(new ExposureTranslation
                {
                    ExposureId = entity.Id,
                    TranslatableLanguage = modelInput.TranslatableLanguage,
                    Name = modelInput.Name,
                    Description = modelInput.Description
                });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, null, "EditionSuccessMessage", null);
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

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        [Route("edit/translations/modify")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyTranslation(ExposureTranslationInputViewModel modelInput)
        {
            Exposure entity = null;

            try
            {
                entity = Repository.Get(modelInput.ExposureId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }


            if (ModelState.IsValid)
            {
                var translation = entity.Translations.First(item => item.TranslatableLanguage == modelInput.TranslatableLanguage);

                translation.Name = modelInput.Name;
                translation.Description = modelInput.Description;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, null, "EditionSuccessMessage", null);
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

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("EditTranslations", new { key = entity.Id });
        }

        [Route("edit/translations/remove")]
        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTranslation(System.Guid exposureId, Persistence.Enumerations.TranslatableLanguage translatableLanguage)
        {
            Exposure entity = null;

            try
            {
                entity = Repository.Get(exposureId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, "NotFoundMessage", null);

                return RedirectToAction("EditTranslations");
            }

            try
            {
                var bridge = entity.Translations.First(item => item.TranslatableLanguage == translatableLanguage);

                entity.Translations.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, null, "EditionSuccessMessage", null);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Warning, null, "EditionWarningMessage", null);
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
