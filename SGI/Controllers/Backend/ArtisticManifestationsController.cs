using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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
    public class ArtisticManifestationsController : AbstractEntityManagementController<ArtisticManifestation, Guid, ArtisticManifestationInputViewModel, ArtisticManifestationDisplayViewModel>
    {
        public ArtisticManifestationsController(IEntityRepository<ArtisticManifestation, Guid> repository, IStringLocalizer<ArtisticManifestationsController> localizer, ILogger<ArtisticManifestationsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ArtisticManifestationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ArtisticManifestationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ArtisticManifestationCreator)]
        public override IActionResult Create(ArtisticManifestationInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ArtisticManifestationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ArtisticManifestationEditor)]
        public override IActionResult Edit(ArtisticManifestationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ArtisticManifestationRemover)]
        public override IActionResult Delete(Guid key) 
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ArtisticManifestationRemover)]
        public override IActionResult Delete(ArtisticManifestationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
