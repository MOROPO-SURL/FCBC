using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Critic : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Critic()
        {
            Id = System.Guid.NewGuid();
            Identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity();
            Translations = new List<CriticTranslation>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdentityId")]
        public SAPRESSC.Middleware.Identity.Persistence.Entities.PersonIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }
        public List<CriticTranslation> Translations { get; set; }
    }
}
