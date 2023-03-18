using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AssociationPhoneContactInputViewModel : PhoneContactInputViewModel
    {
        public System.Guid AssociationId { get; set; }
    }
}
