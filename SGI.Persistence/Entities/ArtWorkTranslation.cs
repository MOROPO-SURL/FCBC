using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ArtWorkTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public System.Guid ArtWorkId { get; set; }
        public ArtWork ArtWork { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string Collection { get; set; }
        public string Location { get; set; }
    }
}
