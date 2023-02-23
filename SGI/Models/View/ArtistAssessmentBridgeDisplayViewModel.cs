using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistAssessmentBridgeDisplayViewModel
    {
        public System.Guid ArtistId { get; set; }        

        public System.Guid CriticId { get; set; }
        public System.Guid CriticUpdateId { get; set; }

        public ICollection<SelectListItem> SelectCritics { get; set; } = new List<SelectListItem>();       

        public System.DateTime DateOfAssessment { get; set; }
        public string Criterion { get; set; }

        public System.Guid AsignIdUtility(System.Guid Id)
        {
            this.CriticUpdateId = Id;
            return this.CriticUpdateId;
        }
    }
}
