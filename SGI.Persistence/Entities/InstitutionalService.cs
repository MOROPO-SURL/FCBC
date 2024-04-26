using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class InstitutionalService : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public List<InstitutionalServiceTranslation> Translations { get; set; }

        public InstitutionalService()
        {
            Id = System.Guid.NewGuid();
            Translations = new List<InstitutionalServiceTranslation>();
        }
    }
}
