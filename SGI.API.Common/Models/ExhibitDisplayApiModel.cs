namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ExhibitDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Exhibit, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int Index { get; set; }

        public ArtWorkDisplayApiModel[] ArtWorks { get; set; }

        public CraftWorkDisplayApiModel[] CraftWorks { get; set; }

        public void Import(Persistence.Entities.Exhibit entity)
        {
            Id = entity.Id;
            Index = entity.Index;
        }
    }
}
