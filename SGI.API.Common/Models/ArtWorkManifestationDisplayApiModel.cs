namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ArtWorkManifestationDisplayApiModel : Contract.AbstractManifestationDisplayApiModel, SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtisticManifestation, System.Guid>
    {
        public void Import(Persistence.Entities.ArtisticManifestation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
