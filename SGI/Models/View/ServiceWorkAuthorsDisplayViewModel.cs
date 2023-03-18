namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkAuthorsDisplayViewModel
    {
        public ServiceWorkDisplayViewModel Work { get; set; }

        public ArtistDisplayViewModel[] Creators { get; set; }

        public CreationGroupDisplayViewModel[] CreationGroups { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCreators { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCreationGroups { get; set; }
    }
}
