using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CriterialInputViewModel
    {
        public System.Guid Id { get; set; }

        public System.Guid OwnerId { get; set; }

        [Display(Name = "Critic")]
        public System.Guid CriticId { get; set; }

        public SelectListItem[] CriticSelectListItems { get; set; }

        [Display(Name = "Criterion")]
        public string Criterion { get; set; }

        [Display(Name = "Day of Criterion")]
        public System.DateTime DateOfCriterion { get; set; } = System.DateTime.Now;
    }
}
