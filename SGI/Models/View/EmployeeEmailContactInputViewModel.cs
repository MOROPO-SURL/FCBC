using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class EmployeeEmailContactInputViewModel : EmailContactInputViewModel
    {
        public System.Guid EmployeeId { get; set; }
    }
}
