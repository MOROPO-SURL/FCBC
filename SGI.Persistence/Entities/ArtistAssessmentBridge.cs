using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtistAssessmentBridge
    {
        public System.Guid ArtistId { get; set; }

        public Artist Artist { get; set; }

        public System.Guid CriticId { get; set; }

        public Critic Critic { get; set; }

        public DateTime DateOfAssessment { get; set; }
        public string Criterion { get; set; }

    }
}
