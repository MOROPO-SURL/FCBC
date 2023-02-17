using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class GroupCriterionManageViewModel
    {
        public GroupCriterionManageViewModel()
        {
        }

        public CreationGroupDisplayViewModel GroupDisplay { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Critic")]
        public System.Guid CriticId { get; set; }

        public System.Collections.Generic.ICollection<SelectListItem> SelectCritics { get; set; } = new System.Collections.Generic.List<SelectListItem>();

        [System.ComponentModel.DataAnnotations.Display(Name = "Criterion")]
        public string Criterion { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Day of Criterion")]
        public System.DateTime DateOfCriterion { get; set; } = DateTime.Now;
    }
}
