using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
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
        private readonly IEnumerable<SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;

        private readonly IEntityRepository<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace, long> traceRepository;
        private readonly IEntityRepository<SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities.User, string> userRepository;
        private readonly IEntityRepository<Artist, System.Guid> creatorRepository;
        private readonly IEntityRepository<CreationGroup, System.Guid> creationGroupRepository;
        private readonly IEntityRepository<Contractor, System.Guid> contractorRepository;
        private readonly IEntityRepository<ArtWork, System.Guid> artWorkRepository;

        private static readonly string[] DAYS_OF_WEEK = new[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

        public DashboardController(IEntityRepository<ArtWork, System.Guid> artWorkRepository, IEntityRepository<Contractor, System.Guid> contractorRepository, IEntityRepository<CreationGroup, System.Guid> creationGroupRepository, IEntityRepository<Artist, System.Guid> creatorRepository, IEntityRepository<SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities.User, string> userRepository, IEnumerable<SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders, Microsoft.Extensions.Localization.IStringLocalizer<DashboardController> localizer, ILogger<DashboardController> logger, IEntityRepository<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace, long> traceRepository) : base(localizer, logger)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
            this.userRepository = userRepository;
            this.traceRepository = traceRepository;
            this.creatorRepository = creatorRepository;
            this.creationGroupRepository = creationGroupRepository;
            this.contractorRepository = contractorRepository;
            this.artWorkRepository = artWorkRepository;
        }

        public IActionResult Index()
        {
            var startEdgeDate = System.DateTime.Today;
            var passWeekEdgeDate = startEdgeDate.AddDays(-6);
            var traces = traceRepository.GetAll();

            var model = new Models.View.ManagementDashboardViewModel 
            { 
                Sections = managementDashboardSectionProviders.ToManagementDashboardSectionGroups(),
                UserCount = userRepository.Count(),
                TraceCount = traceRepository.Count(),
                CreatorCount = creatorRepository.Count(),
                CreationGroupCount = creationGroupRepository.Count(),
                ContractorCount = contractorRepository.Count(),
                WorkCount = artWorkRepository.Count()
            };

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
