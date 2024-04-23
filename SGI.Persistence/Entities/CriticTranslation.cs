using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class CriticTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public System.Guid CriticId { get; set; }
        public Critic Critic { get; set; }
        [System.ComponentModel.DataAnnotations.Required]        
        public string About { get; set; }
    }
}
