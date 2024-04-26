namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class CraftWorkManifestationDisplayApiModel : Contract.AbstractManifestationDisplayApiModel, SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.CraftManifestation, System.Guid>
    {
        public void Import(Persistence.Entities.CraftManifestation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
