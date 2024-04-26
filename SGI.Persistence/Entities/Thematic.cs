using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Thematic : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public Thematic()
        {
            Id = System.Guid.NewGuid();
            Translations = new List<ThematicTranslation>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public List<ThematicTranslation> Translations { get; set; }

    }
}
