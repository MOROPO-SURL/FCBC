using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("complements")]
    public class ArtisticManifestationsController : AbstractEntityManagementController<ArtisticManifestation, Guid, ArtisticManifestationInputViewModel, ArtisticManifestationDisplayViewModel>
    {        

        public ArtisticManifestationsController(IEntityRepository<ArtisticManifestation, Guid> repository, IStringLocalizer<ArtisticManifestationsController> localizer, ILogger<ArtisticManifestationsController> logger) : base(repository, localizer, logger)
        {           
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationCreator)]
        public override IActionResult Create(ArtisticManifestationInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationEditor)]
        public override IActionResult Edit(ArtisticManifestationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationRemover)]
        public override IActionResult Delete(Guid key) 
        {
            return base.Delete(key);
        }

        public override IActionResult Delete(ArtisticManifestationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
