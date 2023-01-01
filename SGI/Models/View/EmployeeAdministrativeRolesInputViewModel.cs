using MOROPO.SAPRESSC.Core.Common.Models.View;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeAdministrativeRolesInputViewModel
    {
        public List<SelectionDescriptedItemInputViewModel<System.Guid>> SelectableRoles { get; set; }

        public System.Guid EmployeeId { get; set; }

        public EmployeeAdministrativeRolesInputViewModel()
        {
            SelectableRoles = new List<SelectionDescriptedItemInputViewModel<System.Guid>>();
        }
    }
}
