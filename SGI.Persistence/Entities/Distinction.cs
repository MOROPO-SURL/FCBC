using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities;
using System;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Distinction : INamedEntity<Guid>
    {
        public Guid Id { get ; set; }
        public string Name { get; set; } 
        
        public Distinction()
        {
            Id = Guid.NewGuid();
        }
    }
}
