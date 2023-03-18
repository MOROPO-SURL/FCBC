using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class UnitMeasurementsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<UnitMeasurement, System.Guid, UnitMeasurementInputViewModel, UnitMeasurementDisplayViewModel>
    {
        public UnitMeasurementsController(IEntityRepository<UnitMeasurement, Guid> repository, IStringLocalizer<UnitMeasurementsController> localizer, ILogger<UnitMeasurementsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.UnitMeasurementViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.UnitMeasurementCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.UnitMeasurementCreator)]
        public override IActionResult Create(UnitMeasurementInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.UnitMeasurementEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.UnitMeasurementEditor)]
        public override IActionResult Edit(UnitMeasurementInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.UnitMeasurementRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.UnitMeasurementRemover)]
        public override IActionResult Delete(UnitMeasurementDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
