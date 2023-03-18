using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.EF.Entities;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("Security")]
    public class TracesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementFilteredController<Trace, long, TraceInputViewModel, TraceDisplayViewModel, TracesFilterModel>
    {
        public TracesController(IEntityRepository<Trace, long> repository, IStringLocalizer<TracesController> localizer, ILogger<TracesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.TraceViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.TraceViewer)]
        public override IActionResult Filter(TracesFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "traces-management-filter";
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(TraceInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(long key)
        {
            return NotFound();
        }

        public override IActionResult Edit(TraceInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(long key)
        {
            return NotFound();
        }

        public override IActionResult Delete(TraceDisplayViewModel displayModel)
        {
            return NotFound();
        }
    }
}
