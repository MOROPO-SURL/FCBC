using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class AssociationTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public System.Guid AssociationId { get; set; }
        public Association Association { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
    }
}
