using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CraftWorkAward: SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkId")]
        public CraftWork Work { get; set; }
        public System.Guid WorkId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AwardId")]
        public Award Award { get; set; }
        public System.Guid AwardId { get; set; }

        public string About { get; set; }

        public int Year { get; set; }


    }
}
