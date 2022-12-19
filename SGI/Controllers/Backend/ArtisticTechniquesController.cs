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
    public class ArtisticTechniquesController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ArtisticTechnique, System.Guid, ArtisticTechniqueInputViewModel, ArtisticTechniqueDisplayViewModel>
    {
        public ArtisticTechniquesController(IEntityRepository<ArtisticTechnique, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ArtisticTechniquesController> localizer, ILogger<ArtisticTechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.ArtisticTechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ArtisticTechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ArtisticTechniqueCreator)]
        public override IActionResult Create(ArtisticTechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ArtisticTechniqueEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ArtisticTechniqueEditor)]
        public override IActionResult Edit(ArtisticTechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ArtisticTechniqueRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ArtisticTechniqueRemover)]
        public override IActionResult Delete(ArtisticTechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
