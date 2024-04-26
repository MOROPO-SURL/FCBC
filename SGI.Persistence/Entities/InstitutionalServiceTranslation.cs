using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class InstitutionalServiceTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public System.Guid InstitutionalServiceId { get; set; }
        public InstitutionalService InstitutionalService { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
