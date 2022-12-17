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
    public class UnitMeasurementsController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<UnitMeasurement, System.Guid, UnitMeasurementInputViewModel, UnitMeasurementDisplayViewModel>
    {
        public UnitMeasurementsController(IEntityRepository<UnitMeasurement, Guid> repository, IStringLocalizer<UnitMeasurementsController> localizer, ILogger<UnitMeasurementsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.UnitMeasurementViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.UnitMeasurementCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.UnitMeasurementCreator)]
        public override IActionResult Create(UnitMeasurementInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.UnitMeasurementEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.UnitMeasurementEditor)]
        public override IActionResult Edit(UnitMeasurementInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.UnitMeasurementRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.UnitMeasurementRemover)]
        public override IActionResult Delete(UnitMeasurementDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
