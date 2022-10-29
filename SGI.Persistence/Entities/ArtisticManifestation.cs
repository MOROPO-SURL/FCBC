using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class ArtisticManifestation : INamedEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ArtisticManifestation()
        {
            Id = Guid.NewGuid();
        }
        
    }
}
