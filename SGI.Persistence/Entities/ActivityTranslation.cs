using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ActivityTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public System.Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
    }
}
