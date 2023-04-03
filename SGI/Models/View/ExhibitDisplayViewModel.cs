namespace MOROPO.FCBC.SGI.Models.View
{
    public class ExhibitDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Exhibit, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int Index { get; set; }

        public ArtWorkDisplayViewModel ArtWork { get; set; }

        public void Import(Persistence.Entities.Exhibit entity)
        {
            Id = entity.Id;
            Index = entity.Index;
        }
    }
}
