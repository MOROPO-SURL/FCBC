using MOROPO.FCBC.SGI.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AssociationDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<FBCB.SGI.Persistence.Entities.Association, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "Organization")]
        public string OrganizationIdentityName { get; set; }

        [Display(Name = "Founding date")]
        public DateTime BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [Display(Name = "Address")]
        public string AddressPostal { get; set; } 

        public void Import(FBCB.SGI.Persistence.Entities.Association entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.OrganizationIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;
            BornDate = entity.OrganizationIdentity.BornDate;
            About = entity.OrganizationIdentity.About;
            AddressPostal = entity.OrganizationIdentity.OrganizationPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
        }
    }
}
