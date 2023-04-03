using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Award : SAPRESSC.Core.Persistence.Entities.INamedEntity<Guid>
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
        public Guid? CountryId { get; set; }

        public string Description { get; set; }
    }
}
