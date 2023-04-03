namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class Gallery : SAPRESSC.Core.Persistence.Entities.IEntity<Enumerations.GalleryId>
    {
        public Gallery()
        {
            EmployeeBridges = new System.Collections.Generic.List<GalleryEmployeeBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public Enumerations.GalleryId Id { get; set; }

        public SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity Identity { get; set; }
        public System.Guid? IdentityId { get; set; }

        public string History { get; set; }

        public System.Collections.Generic.List<GalleryEmployeeBridge> EmployeeBridges { get; set; } 
    }
}
