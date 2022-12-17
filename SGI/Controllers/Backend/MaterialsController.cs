using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Frontend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("complements")]
    public class MaterialsController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Material, System.Guid, MaterialInputViewModel, MaterialDisplayViewModel>
    {
        public MaterialsController(IEntityRepository<Material, Guid> repository, IStringLocalizer<MaterialsController> localizer, ILogger<MaterialsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.MaterialViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.MaterialCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.MaterialCreator)]
        public override IActionResult Create(MaterialInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.MaterialEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.MaterialEditor)]
        public override IActionResult Edit(MaterialInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.MaterialRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.MaterialRemover)]
        public override IActionResult Delete(MaterialDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
