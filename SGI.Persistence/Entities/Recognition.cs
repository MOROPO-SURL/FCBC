using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities;
using System;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Recognition : INamedEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Recognition()
        {
            Id = Guid.NewGuid();
        }
    }
}
