using MOROPO.FBCB.SGI.Persistence.Entities;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ManifestationKindDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<ManifestationKind, System.Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ManifestationKind Parent { get; set; }

        public void Import(ManifestationKind entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Parent = entity.Parent;
        }
    }
}
