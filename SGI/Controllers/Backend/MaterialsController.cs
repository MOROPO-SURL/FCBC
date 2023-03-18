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
    public class MaterialsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Material, System.Guid, MaterialInputViewModel, MaterialDisplayViewModel>
    {
        public MaterialsController(IEntityRepository<Material, System.Guid> repository, IStringLocalizer<MaterialsController> localizer, ILogger<MaterialsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.MaterialViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.MaterialCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.MaterialCreator)]
        public override IActionResult Create(MaterialInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.MaterialEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.MaterialEditor)]
        public override IActionResult Edit(MaterialInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.MaterialRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.MaterialRemover)]
        public override IActionResult Delete(MaterialDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
