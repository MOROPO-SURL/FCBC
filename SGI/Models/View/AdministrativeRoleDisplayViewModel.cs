namespace MOROPO.FCBC.SGI.Models.View
{
    public class AdministrativeRoleDisplayViewModel: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void Import(MOROPO.FBCB.SGI.Persistence.Entities.AdministrativeRole entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
