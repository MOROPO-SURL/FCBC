using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Frontend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using MOROPO.FCBC.SGI.Security.Authorization;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("Security")]
    public class TracesController : AbstractEntityManagementFilteredController<Trace, long, TraceInputViewModel, TraceDisplayViewModel, TracesFilterModel>
    {
        public TracesController(IEntityRepository<Trace, long> repository, IStringLocalizer<TracesController> localizer, ILogger<TracesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.TraceViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.TraceViewer)]
        public override IActionResult Filter(TracesFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "traces-management-filter";
        }

    }
}
