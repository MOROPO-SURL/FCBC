using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistAssessmentBridgeDisplayViewModel
    {
        public System.Guid ArtistId { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Critic")]
        public System.Guid CriticId { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Critic")]
        public System.Guid CriticUpdateId { get; set; }

        public ICollection<SelectListItem> SelectCritics { get; set; } = new List<SelectListItem>();
        [System.ComponentModel.DataAnnotations.Display(Name = "DateOfAssessment")]
        public System.DateTime DateOfAssessment { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Criterion")]
        public string Criterion { get; set; }

        public System.Guid AsignIdUtility(System.Guid Id)
        {
            this.CriticUpdateId = Id;
            return this.CriticUpdateId;
        }
    }
}
