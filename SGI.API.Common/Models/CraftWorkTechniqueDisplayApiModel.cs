namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class CraftWorkTechniqueDisplayApiModel : Contract.AbstractTechniqueDisplayApiModel, SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.CraftTechnique, System.Guid>
    {
        public void Import(Persistence.Entities.CraftTechnique entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
