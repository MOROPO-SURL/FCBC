
namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorKindDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<FBCB.SGI.Persistence.Entities.ContractorKind, int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(FBCB.SGI.Persistence.Entities.ContractorKind entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
