using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class GroupAssessmentBridge
    {
        public System.Guid GroupId { get; set; }

        public CreationGroup CreationGroup { get; set; }

        public System.Guid CriticId { get; set; }

        public Critic Critic { get; set; }

        public DateTime DateOfAssessment { get; set; }
        public string Criterion { get; set; }
    }
}
