using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SmartSolucionesCuba.SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("complements")]
    public class DesignTechniquesController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<DesignTechnique, System.Guid, DesignTechniqueInputViewModel, DesignTechniqueDisplayViewModel>
    {
        public DesignTechniquesController(IEntityRepository<DesignTechnique, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<DesignTechniquesController> localizer, ILogger<DesignTechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.DesignTechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.DesignTechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.DesignTechniqueCreator)]
        public override IActionResult Create(DesignTechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.DesignTechniqueEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.DesignTechniqueEditor)]
        public override IActionResult Edit(DesignTechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.DesignTechniqueRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.DesignTechniqueRemover)]
        public override IActionResult Delete(DesignTechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
