using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceWorkTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public System.Guid ServiceWorkId { get; set; }
        public ServiceWork ServiceWork { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
