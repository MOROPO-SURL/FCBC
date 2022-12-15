using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
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
    public class InformationController : SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<InstitutionalInformation, InstitutionalInformationKind> institutionalInformationRepository;

        public InformationController(IEntityRepository<InstitutionalInformation, InstitutionalInformationKind> institutionalInformationRepository, Microsoft.Extensions.Localization.IStringLocalizer<InformationController> localizer, ILogger<InformationController> logger) : base(localizer, logger)
        {
            this.institutionalInformationRepository = institutionalInformationRepository;
        }

        [RequiresRoleBasePermission(Security.Authorization.Roles.InstitutionalInformationViewer)]
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
        [RequiresRoleBasePermission(Security.Authorization.Roles.InstitutionalInformationEditor)]
        [Route("[action]")]
        public virtual IActionResult Edit()
        {
            try
            {
                var socialObjectEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.SocialObject);
                var missionEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.Mission);
                var visionEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.Vision);
                var historyEntity = institutionalInformationRepository.Get(InstitutionalInformationKind.History);

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
        [RequiresRoleBasePermission(Security.Authorization.Roles.InstitutionalInformationEditor)]
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
