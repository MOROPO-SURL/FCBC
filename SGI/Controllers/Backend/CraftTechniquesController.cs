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
    public class CraftTechniquesController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<CraftTechnique, System.Guid, CraftTechniqueInputViewModel, CraftTechniqueDisplayViewModel>
    {
        public CraftTechniquesController(IEntityRepository<CraftTechnique, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CraftTechniquesController> localizer, ILogger<CraftTechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.CraftTechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.CraftTechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.CraftTechniqueCreator)]
        public override IActionResult Create(CraftTechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.CraftTechniqueEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.CraftTechniqueEditor)]
        public override IActionResult Edit(CraftTechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.CraftTechniqueRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.CraftTechniqueRemover)]
        public override IActionResult Delete(CraftTechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
