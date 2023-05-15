using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("manifestations")]
    public class DesignManifestationsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<DesignManifestation, Guid, DesignManifestationInputViewModel, DesignManifestationDisplayViewModel>
    {
        public DesignManifestationsController(IEntityRepository<DesignManifestation, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<DesignManifestationsController> localizer, ILogger<DesignManifestationsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.DesignManifestationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.DesignManifestationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.DesignManifestationCreator)]
        public override IActionResult Create(DesignManifestationInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.DesignManifestationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.DesignManifestationEditor)]
        public override IActionResult Edit(DesignManifestationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.DesignManifestationRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.DesignManifestationRemover)]
        public override IActionResult Delete(DesignManifestationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
