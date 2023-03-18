using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class ArtisticTechniquesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ArtisticTechnique, System.Guid, ArtisticTechniqueInputViewModel, ArtisticTechniqueDisplayViewModel>
    {
        public ArtisticTechniquesController(IEntityRepository<ArtisticTechnique, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ArtisticTechniquesController> localizer, ILogger<ArtisticTechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ArtisticTechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ArtisticTechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ArtisticTechniqueCreator)]
        public override IActionResult Create(ArtisticTechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ArtisticTechniqueEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ArtisticTechniqueEditor)]
        public override IActionResult Edit(ArtisticTechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ArtisticTechniqueRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ArtisticTechniqueRemover)]
        public override IActionResult Delete(ArtisticTechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
