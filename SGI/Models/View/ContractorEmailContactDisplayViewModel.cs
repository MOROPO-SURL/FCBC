using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorEmailContactDisplayViewModel
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; }

        [Display(Name = "Email")]
        public string Address { get; set; }

        public System.Guid ContractorId { get; set; }
    }
}
