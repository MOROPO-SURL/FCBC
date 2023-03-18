using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CraftWorkCriterionManageViewModel
    {
        public CraftWorkCriterionManageViewModel()
        {
        }

        public CraftWorkDisplayViewModel WorkDisplay { get; set; }

        [Display(Name = "Critic")]
        public System.Guid CriticId { get; set; }

        public ICollection<SelectListItem> SelectCritics { get; set; } = new List<SelectListItem>();

        [Display(Name = "Criterion")]
        public string Criterion { get; set; }

        [Display(Name = "Day of Criterion")]
        public DateTime DateOfCriterion { get; set; } = DateTime.Now;
    }
}
