namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ServiceManifestationDisplayApiModel : Contract.AbstractManifestationDisplayApiModel, SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ServiceLine, System.Guid>
    {
        public void Import(Persistence.Entities.ServiceLine entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
