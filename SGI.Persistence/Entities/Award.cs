using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class Award : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.INamedEntity<Guid>
    {
        public Award()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Required]
        public System.Guid CountryId { get; set; }
    }
}
