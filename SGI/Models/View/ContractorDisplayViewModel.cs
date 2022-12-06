using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Contractor, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "Contract Type")]
        public string ContractKindName { get; set; }

        [Display(Name = "Organization")]
        public string OrganizationIdentityName { get; set; }

        [Display(Name = "Founding date")]
        public DateTime BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        public PostalContactItem PostalContactItem { get; set; }

        public ICollection<EmailContactItem> EmailContactItems { get; set; }

        public ICollection<PhoneContactItem> PhoneContactItems { get; set; }

        public void Import(Contractor entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.OrganizationIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            ContractKindName = entity.ContractorKind.Name;
            OrganizationIdentityName = entity.OrganizationIdentity.Name;
            BornDate = entity.OrganizationIdentity.BornDate;
            About = entity.OrganizationIdentity.About;
            PostalContactItem = entity.OrganizationIdentity.OrganizationPostalContactItemBridges.Select(b => b.ContactItem).FirstOrDefault();
            EmailContactItems = entity.OrganizationIdentity.OrganizationEmailContactItemBridges.Select(e => e.ContactItem).ToArray();
            PhoneContactItems = entity.OrganizationIdentity.OrganizationPhoneContactItemBridges.Select(p => p.ContactItem).ToArray();
        }
    }
}
