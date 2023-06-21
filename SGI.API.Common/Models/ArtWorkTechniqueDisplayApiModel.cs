namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ArtWorkTechniqueDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtisticTechnique, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.ArtisticTechnique entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
