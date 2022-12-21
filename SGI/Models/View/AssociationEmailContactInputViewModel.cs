using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AssociationEmailContactInputViewModel : EmailContactInputViewModel
    {
        public System.Guid AssociationId { get; set; }
    }
}
