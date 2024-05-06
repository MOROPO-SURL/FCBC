using MOROPO.FCBC.SGI.Persistence.Enumerations;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class InstitutionalInformation : SAPRESSC.Core.Persistence.Entities.IEntity<InstitutionalInformationKind>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public InstitutionalInformationKind Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Value { get; set; }
        
        public List<InstitutionalInformationTranslation> Translations { get; set; }

        public InstitutionalInformation()
        {
            Translations = new List<InstitutionalInformationTranslation>();
        }

    }
}
