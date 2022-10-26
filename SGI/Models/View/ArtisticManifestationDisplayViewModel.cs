using MOROPO.FBCB.SGI.Persistence.Entities;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtisticManifestationDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<ArtisticManifestation, Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public void Import(ArtisticManifestation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
