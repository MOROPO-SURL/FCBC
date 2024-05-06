using MOROPO.FCBC.SGI.Persistence.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class InstitutionalInformationTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public InstitutionalInformationKind InstitutionalInformationKind { get; set; }
        public InstitutionalInformation InstitutionalInformation { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string SocialObject { get; set; }
        public string Mission { get; set; }
        public string Vision {get; set;}
        public string History { get; set; }
        
    }
}
