using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ExposureTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public System.Guid ExposureId { get; set; }
        public Exposure Exposure { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
    }
}
