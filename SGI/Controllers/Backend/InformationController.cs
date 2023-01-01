using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
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
    }
}
