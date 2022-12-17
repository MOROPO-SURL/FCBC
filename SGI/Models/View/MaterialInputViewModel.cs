using MOROPO.FCBC.SGI.Persistence.Entities;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class MaterialInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<Material, System.Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Material Export()
        {
            var entity = new Material();

            Mergue(entity);

            return entity;
        }

        public void Import(Material entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(Material entity)
        {
            entity.Id = Id;
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
