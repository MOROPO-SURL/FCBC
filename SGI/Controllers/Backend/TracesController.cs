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
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("Security")]
    public class TracesController : AbstractEntityManagementFilteredController<Trace, long, TraceInputViewModel, TraceDisplayViewModel, TracesFilterModel>
    {
        private readonly IEntityRepository<Trace, long> traceRepository;

        private static readonly string[] DAYS_OF_WEEK = new[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

        public TracesController(IEntityRepository<Trace, long> repository, IStringLocalizer<TracesController> localizer, ILogger<TracesController> logger, IEntityRepository<Trace, long> traceRepository) : base(repository, localizer, logger)
        {
            this.traceRepository = traceRepository;
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

        [HttpGet]
        [Route("[action]")]
        public IActionResult Charts()
        {
            var startEdgeDate = System.DateTime.Today;
            var passWeekEdgeDate = startEdgeDate.AddDays(-6);
            var traces = traceRepository.GetAll();

            var model = new ChartViewModel();

            var tracesByUsers = new Dictionary<string, long>();

            foreach (Trace trace in traces)
            {
                if (!tracesByUsers.ContainsKey(trace.Author))
                {
                    tracesByUsers.Add(trace.Author, 1);
                }
                else
                {
                    tracesByUsers[trace.Author]++;
                }  
            }
            model.UsersList.AddRange(tracesByUsers.Keys);
            model.TracesbyUsers.AddRange(tracesByUsers.Values);

            for (System.DateTime currentDate = passWeekEdgeDate; currentDate <= startEdgeDate; currentDate = currentDate.AddDays(1))
            {
                model.WeekStatisticsColumns.Add(DAYS_OF_WEEK[(int)currentDate.DayOfWeek]);

                model.WeekTracesStatisticsValues.Add(traceRepository.Count(entity => entity.Registered.Date == currentDate));
            }
            return View(model);
        }

    }
}
