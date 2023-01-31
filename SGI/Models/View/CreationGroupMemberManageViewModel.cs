namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupMemberManageViewModel
    {
        public CreationGroupDisplayViewModel Group { get; set; }

        public System.Collections.Generic.ICollection<ArtistDisplayViewModel> Members { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> NotMembers { get; set; }
    }
}
