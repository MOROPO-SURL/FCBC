using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("complements")]
    public class ThematicsController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Thematic, System.Guid, ThematicInputViewModel, ThematicDisplayViewModel>
    {
        public ThematicsController(IEntityRepository<Thematic, System.Guid> repository, IStringLocalizer<ThematicsController> localizer, ILogger<ThematicsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.ThematicViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ThematicCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ThematicCreator)]
        public override IActionResult Create(ThematicInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ThematicEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ThematicEditor)]
        public override IActionResult Edit(ThematicInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ThematicRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ThematicRemover)]
        public override IActionResult Delete(ThematicDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
