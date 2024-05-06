using MOROPO.FCBC.SGI.API.Common.Models;
using MOROPO.FCBC.SGI.API.Common.Models.Contract;

namespace MOROPO.FCBC.Galleries.Models.View.Contract
{
    public abstract class AbstractWorkViewModel
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public ImageViewModel FeatureImage { get; set; }

        public int Year { get; set; }

        public string About { get; set; }

        public AbstractManifestationDisplayApiModel Manifestation { get; set; }

        public AbstractTechniqueDisplayApiModel[] Techniques { get; set; }

        public CreatorDisplayApiModel[] Creators { get; set; }

        public ThemathicDisplayApiModel[] Themathics { get; set; }

        public MaterialDisplayApiModel[] Materials { get; set; }

        public ImageViewModel[] Images { get; set; }

        public VideoDisplayApiModel[] Videos { get; set; }

        public ValorationDisplayApiModel[] Valorations { get; set; }
    }
}
