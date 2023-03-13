namespace MOROPO.FCBC.SGI.Models.View
{
    public class WorkAssessmentBridgeDisplayViewModel
    {
        public System.Guid WorkId { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Critic")]
        public System.Guid CriticId { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Critic")]
        public System.Guid CriticUpdateId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> SelectCritics { get; set; } = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
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
