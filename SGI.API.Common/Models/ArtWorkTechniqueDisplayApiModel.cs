namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ArtWorkTechniqueDisplayApiModel : Contract.AbstractTechniqueDisplayApiModel, SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtisticTechnique, System.Guid>
    {
        public void Import(Persistence.Entities.ArtisticTechnique entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
