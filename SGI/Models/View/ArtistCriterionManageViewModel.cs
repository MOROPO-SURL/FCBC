using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistCriterionManageViewModel
    {
        public ArtistCriterionManageViewModel()
        {
        }

        public ArtistDisplayViewModel ArtistDisplay { get; set; }

        [Display(Name = "Critic")]
        public System.Guid CriticId { get; set; }

        public ICollection<SelectListItem> SelectCritics { get; set; } = new List<SelectListItem>();

        [Display(Name = "Criterion")]
        public string Criterion { get; set; }

        [Display(Name = "Day of Criterion")]
        public DateTime DateOfCriterion { get; set; } = DateTime.Now;
    }
}
