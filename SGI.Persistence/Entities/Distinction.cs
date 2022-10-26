using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
