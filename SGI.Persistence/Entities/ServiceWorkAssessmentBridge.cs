using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceWorkAssessmentBridge
    {
        public System.Guid WorkId { get; set; }

        public ServiceWork Work { get; set; }

        public System.Guid CriticId { get; set; }

        public Critic Critic { get; set; }

        public DateTime DateOfAssessment { get; set; }
        public string Criterion { get; set; }
    }
}
