using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistAward: SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
        public System.Guid ArtistId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AwardId")]
        public Award Award { get; set; }
        public System.Guid AwardId { get; set; }

        [Required]
        public string Context { get; set; }
    }
}
