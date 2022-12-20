using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ManagementDashboardViewModel
    {
        public ManagementDashboardViewModel()
        {
            UsersList = new List<string>();
            TracesbyUsers = new List<long>();
            WeekStatisticsColumns = new List<string>();
            WeekTracesStatisticsValues = new List<int>();
        }

        public Dictionary<string, ICollection<SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Models.View.ManagementDashboardSectionViewModel>> Sections { get; set; }

        public List<string> UsersList { get; set; }
        public List<long> TracesbyUsers { get; set; }
        public List<string> WeekStatisticsColumns { get; set; }
        public List<int> WeekTracesStatisticsValues { get; set; }

        public int UserCount { get; set; }
        public int TraceCount { get; set; }
        public int CreatorCount { get; set; }
        public int CreationGroupCount { get; set; }
        public int ContractorCount { get; set; }
        public int WorkCount { get; set; }
    }
}
