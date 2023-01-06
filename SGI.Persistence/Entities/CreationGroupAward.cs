using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CreationGroupAward : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get ; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GroupId")]
        public CreationGroup Group { get; set; }

        public System.Guid GroupId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AwardId")]
        public Award Award { get; set; }
        public System.Guid AwardId { get; set; }

        [Required]
        public string Context { get; set; }
    }
}
