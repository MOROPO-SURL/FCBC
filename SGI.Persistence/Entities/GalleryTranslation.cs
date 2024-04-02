using System;
using System.Collections.Generic;
using System.Text;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class GalleryTranslation
    {
        public Enumerations.TranslatableLanguage TranslatableLanguage { get; set; }
        public Persistence.Enumerations.GalleryId GalleryId { get; set; }
        public Gallery Gallery { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
    }
}
