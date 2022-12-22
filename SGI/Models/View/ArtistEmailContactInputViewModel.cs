using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistEmailContactInputViewModel : EmailContactInputViewModel
    {
        public System.Guid ArtistId { get; set; }
    }
}
