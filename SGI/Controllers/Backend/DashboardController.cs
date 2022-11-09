using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SmartSolucionesCuba.SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [ChangeUserPasswordRequiredAction]
    [CatchChangeUserPasswordRequiredException]
    [Route("management/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[controller]")]
    public class DashboardController : SmartSolucionesCuba.SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly System.Collections.Generic.IEnumerable<SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;

        private readonly IEntityRepository<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace, long> traceRepository;

        private static readonly string[] DAYS_OF_WEEK = new[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

        public DashboardController(System.Collections.Generic.IEnumerable<SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders, Microsoft.Extensions.Localization.IStringLocalizer<DashboardController> localizer, ILogger<DashboardController> logger, IEntityRepository<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace, long> traceRepository) : base(localizer, logger)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
            this.traceRepository = traceRepository;
        }

        public IActionResult Index()
        {
            var startEdgeDate = System.DateTime.Today;
            var passWeekEdgeDate = startEdgeDate.AddDays(-6);
            var traces = traceRepository.GetAll();

            var model = new Models.View.ManagementDashboardViewModel { Sections = managementDashboardSectionProviders.ToManagementDashboardSectionGroups() };

            var tracesByUsers = new Dictionary<string, long>();

            foreach (SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace trace in traces)
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
