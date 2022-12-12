using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SmartSolucionesCuba.SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Route("management/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[controller]")]
    [Area("institution")]
    public class InstitutionInformationController : SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<FBCB.SGI.Persistence.Entities.InstitutionInformation, FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind> institutionInformationRepository;

        public InstitutionInformationController(IEntityRepository<FBCB.SGI.Persistence.Entities.InstitutionInformation, FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind> institutionInformationRepository, Microsoft.Extensions.Localization.IStringLocalizer<InstitutionInformationController> localizer, ILogger<InstitutionInformationController> logger) : base(localizer, logger)
        {
            this.institutionInformationRepository = institutionInformationRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.InstitutionInformationViewer)]
        public IActionResult Index()
        {
            var entities = institutionInformationRepository.GetAll().ToArray();

            var model = new Models.View.InstitutionInformationDisplayViewModel 
            {
                SocialObject = entities.First(entity => entity.Id == FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.SocialObject).Value,
                Mission = entities.First(entity => entity.Id == FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.Mission).Value,
                Vision = entities.First(entity => entity.Id == FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.Vision).Value,
                Histoy = entities.First(entity => entity.Id == FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.History).Value
            };

            return View(model);
        }

        [HttpGet]
        [RequiresRoleBasePermission(Security.Authorization.Roles.InstitutionInformationEditor)]
        [Route("[action]")]
        public virtual IActionResult Edit()
        {
            try
            {
                var socialObjectEntity = institutionInformationRepository.Get(FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.SocialObject);
                var missionEntity = institutionInformationRepository.Get(FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.Mission);
                var visionEntity = institutionInformationRepository.Get(FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.Vision);
                var historyEntity = institutionInformationRepository.Get(FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.History);

                var modelInput = new Models.View.InstitutionInformationInputViewModel();

                modelInput.SocialObject = socialObjectEntity.Value;
                modelInput.Mission = missionEntity.Value;
                modelInput.Vision = visionEntity.Value;
                modelInput.Histoy = historyEntity.Value;

                return View(modelInput);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, null, "NotFoundMessage", null);

            return Redirect("/");
        }

        [HttpPost]
        [RequiresRoleBasePermission(Security.Authorization.Roles.InstitutionInformationEditor)]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(Models.View.InstitutionInformationInputViewModel modelInput)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var socialObjectEntity = institutionInformationRepository.Get(FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.SocialObject);
                    var missionEntity = institutionInformationRepository.Get(FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.Mission);
                    var visionEntity = institutionInformationRepository.Get(FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.Vision);
                    var historyEntity = institutionInformationRepository.Get(FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind.History);

                    socialObjectEntity.Value = modelInput.SocialObject;
                    missionEntity.Value = modelInput.Mission;
                    visionEntity.Value = modelInput.Vision;
                    historyEntity.Value = modelInput.Histoy;

                    institutionInformationRepository.Update(socialObjectEntity, User.Identity.Name, false);
                    institutionInformationRepository.Update(missionEntity, User.Identity.Name, false);
                    institutionInformationRepository.Update(visionEntity, User.Identity.Name, false);
                    institutionInformationRepository.Update(historyEntity, User.Identity.Name, true);

                    SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Success, null, "EditionSuccessMessage", null);

                    return RedirectToAction("index");
                }

                catch (System.Exception e)
                {
                    if (!(e.InnerException is SmartSolucionesCuba.SAPRESSC.Core.Web.Frontend.InvalidModelException))
                    {
                        logger.LogError(e.InnerException, e.Message);

                        SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, null, "NotFoundMessage", null);

                        return Redirect("/");
                    }
                }
            }

            SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, null, "InvalidMessage", null);

            return View(modelInput);
        }
    }
}
