namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ArtWorkDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtWork, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public ImageDisplayApiModel FeatureImage { get; set; }

        public void Import(Persistence.Entities.ArtWork entity)
        {
            Id = entity.Id;
            Title = entity.Title;
        }
    }
}
