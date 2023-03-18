using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class ThematicsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Thematic, System.Guid, ThematicInputViewModel, ThematicDisplayViewModel>
    {
        public ThematicsController(IEntityRepository<Thematic, System.Guid> repository, IStringLocalizer<ThematicsController> localizer, ILogger<ThematicsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ThematicViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ThematicCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ThematicCreator)]
        public override IActionResult Create(ThematicInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ThematicEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ThematicEditor)]
        public override IActionResult Edit(ThematicInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.ThematicRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ThematicRemover)]
        public override IActionResult Delete(ThematicDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
