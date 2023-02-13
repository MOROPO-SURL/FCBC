using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Security.Essential.Persistence.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [ChangeUserPasswordRequiredAction]
    [CatchChangeUserPasswordRequiredException]
    [Route("management/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[controller]")]
    public class DashboardController : SAPRESSC.Core.Web.Common.Controllers.BaseWebController
    {
        private readonly IEnumerable<SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;

        private readonly IEntityRepository<SAPRESSC.Core.Persistence.EF.Entities.Trace, long> traceRepository;
        private readonly IEntityRepository<User, string> userRepository;
        private readonly IEntityRepository<Artist, System.Guid> creatorRepository;
        private readonly IEntityRepository<CreationGroup, System.Guid> creationGroupRepository;
        private readonly IEntityRepository<Contractor, System.Guid> contractorRepository;
        private readonly IEntityRepository<ArtWork, System.Guid> artWorkRepository;
        private readonly IEntityRepository<CraftWork, System.Guid> craftWorkRepository;
        private readonly IEntityRepository<DesignWork, System.Guid> designWorkRepository;
        private readonly IEntityRepository<ServiceWork, System.Guid> serviceWorkRepository;

        private static readonly string[] DAYS_OF_WEEK = new[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

        public DashboardController(IEntityRepository<ServiceWork, System.Guid> serviceWorkRepository, IEntityRepository<DesignWork, System.Guid> designWorkRepository, IEntityRepository<CraftWork, System.Guid> craftWorkRepository, IEntityRepository<ArtWork, System.Guid> artWorkRepository, IEntityRepository<Contractor, System.Guid> contractorRepository, IEntityRepository<CreationGroup, System.Guid> creationGroupRepository, IEntityRepository<Artist, System.Guid> creatorRepository, IEntityRepository<User, string> userRepository, IEnumerable<SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders, Microsoft.Extensions.Localization.IStringLocalizer<DashboardController> localizer, ILogger<DashboardController> logger, IEntityRepository<SAPRESSC.Core.Persistence.EF.Entities.Trace, long> traceRepository) : base(localizer, logger)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
            this.userRepository = userRepository;
            this.traceRepository = traceRepository;
            this.creatorRepository = creatorRepository;
            this.creationGroupRepository = creationGroupRepository;
            this.contractorRepository = contractorRepository;
            this.artWorkRepository = artWorkRepository;
            this.craftWorkRepository = craftWorkRepository;
            this.designWorkRepository = designWorkRepository;
            this.serviceWorkRepository = serviceWorkRepository;
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
                WorkCount = artWorkRepository.Count(),
                CraftWorkCount = craftWorkRepository.Count(),
                DesignWorkCount = designWorkRepository.Count(),
                ServiceWorkCount = serviceWorkRepository.Count()
            };

            var tracesByUsers = new Dictionary<string, long>();

            foreach (MOROPO.SAPRESSC.Core.Persistence.EF.Entities.Trace trace in traces)
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

            var manifestations = new Dictionary<string, int>();

            foreach (ArtWork work in artWorkRepository.GetAll())
            {
                PopulateManifestationsDictionary(manifestations, work.Manifestation.Name);
            }

            foreach (CraftWork work in craftWorkRepository.GetAll())
            {
                PopulateManifestationsDictionary(manifestations, work.Manifestation.Name);
            }

            foreach (DesignWork work in designWorkRepository.GetAll())
            {
                PopulateManifestationsDictionary(manifestations, work.Manifestation.Name);
            }

            foreach (ServiceWork work in serviceWorkRepository.GetAll())
            {
                PopulateManifestationsDictionary(manifestations, work.Line.Name);
            }

            var OrderManifestationsStatistics = manifestations.OrderByDescending(f => f.Value).ToList().Take(8);

            model.manifestationsNames = OrderManifestationsStatistics.Select(r => r.Key).ToList();
            model.ManifestationsCounts = OrderManifestationsStatistics.Select(r => r.Value).ToList();

            return View(model);
        }

        private void PopulateManifestationsDictionary(Dictionary<string, int> manifestations, string manifestationName)
        {
            if (!manifestations.ContainsKey(manifestationName))
            {
                manifestations.Add(manifestationName, 1);
            }
            else
            {
                manifestations[manifestationName]++;
            }
        }
    }
}
