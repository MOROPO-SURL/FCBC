using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistPhoneContactInputViewModel : PhoneContactInputViewModel
    {
        public System.Guid ArtistId { get; set; }
    }
}
