using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.FCBC.SGI.Security.Authorization;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class ActivityKindsController : AbstractEntityManagementController<ActivityKind, System.Guid, ActivityKindInputViewModel, ActivityKindDisplayViewModel>
    {
        public ActivityKindsController(IEntityRepository<ActivityKind, System.Guid> repository, IStringLocalizer<ActivityKindsController> localizer, ILogger<ActivityKindsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ActivityKindViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ActivityKindCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ActivityKindCreator)]
        public override IActionResult Create(ActivityKindInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ActivityKindEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ActivityKindEditor)]
        public override IActionResult Edit(ActivityKindInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ActivityKindRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ActivityKindRemover)]
        public override IActionResult Delete(ActivityKindDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
