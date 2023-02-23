namespace MOROPO.FCBC.SGI.Models.View
{
    public class GroupAssessmentBridgeDisplayViewModel
    {
        public System.Guid GroupId { get; set; }

        public System.Guid CriticId { get; set; }
        public System.Guid CriticUpdateId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> SelectCritics { get; set; } = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

        public System.DateTime DateOfAssessment { get; set; }
        public string Criterion { get; set; }

        public System.Guid AsignIdUtility(System.Guid Id)
        {
            this.CriticUpdateId = Id;
            return this.CriticUpdateId;
        }
    }
}
