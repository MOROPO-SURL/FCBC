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
    public class DesignTechniquesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<DesignTechnique, System.Guid, DesignTechniqueInputViewModel, DesignTechniqueDisplayViewModel>
    {
        public DesignTechniquesController(IEntityRepository<DesignTechnique, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<DesignTechniquesController> localizer, ILogger<DesignTechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.DesignTechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.DesignTechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.DesignTechniqueCreator)]
        public override IActionResult Create(DesignTechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.DesignTechniqueEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.DesignTechniqueEditor)]
        public override IActionResult Edit(DesignTechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.DesignTechniqueRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.DesignTechniqueRemover)]
        public override IActionResult Delete(DesignTechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
