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
    public class CraftTechniquesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<CraftTechnique, System.Guid, CraftTechniqueInputViewModel, CraftTechniqueDisplayViewModel>
    {
        public CraftTechniquesController(IEntityRepository<CraftTechnique, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CraftTechniquesController> localizer, ILogger<CraftTechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.CraftTechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.CraftTechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.CraftTechniqueCreator)]
        public override IActionResult Create(CraftTechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.CraftTechniqueEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.CraftTechniqueEditor)]
        public override IActionResult Edit(CraftTechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.CraftTechniqueRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.CraftTechniqueRemover)]
        public override IActionResult Delete(CraftTechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
