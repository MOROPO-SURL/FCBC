using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ManagementDashboardViewModel
    {
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.ICollection<SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Models.View.ManagementDashboardSectionViewModel>> Sections { get; set; }

        public List<string> UsersList { get; set; }
        public List<long> TracesbyUsers { get; set; }
        public List<string> WeekStatisticsColumns { get; set; }
        public List<int> WeekTracesStatisticsValues { get; set; }

        public ManagementDashboardViewModel()
        {
            UsersList = new List<string>();
            TracesbyUsers = new List<long>();
            WeekStatisticsColumns = new List<string>();
            WeekTracesStatisticsValues = new List<int>();
        }
    }
}
