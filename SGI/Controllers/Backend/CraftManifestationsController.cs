using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("manifestations")]
    public class CraftManifestationsController : AbstractEntityManagementController<CraftManifestation, Guid, CraftManifestationInputViewModel, CraftManifestationDisplayViewModel>
    {
        public CraftManifestationsController(IEntityRepository<CraftManifestation, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CraftManifestationsController> localizer, ILogger<CraftManifestationsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.CraftManifestationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.CraftManifestationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.CraftManifestationCreator)]
        public override IActionResult Create(CraftManifestationInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.CraftManifestationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.CraftManifestationEditor)]
        public override IActionResult Edit(CraftManifestationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.CraftManifestationRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.CraftManifestationRemover)]
        public override IActionResult Delete(CraftManifestationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
