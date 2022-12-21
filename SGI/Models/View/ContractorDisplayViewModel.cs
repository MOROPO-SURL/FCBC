using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Contractor, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "ContractKind")]
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
            ImageId = entity.Identity.ImageId ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            ContractKindName = entity.ContractorKind.Name;
            OrganizationIdentityName = entity.Identity.Name;
            BornDate = entity.Identity.BornDate;
            About = entity.Identity.About;
            PostalContactItem = entity.Identity.OrganizationPostalContactItemBridges.Select(b => b.ContactItem).FirstOrDefault();
            EmailContactItems = entity.Identity.OrganizationEmailContactItemBridges.Select(e => e.ContactItem).ToArray();
            PhoneContactItems = entity.Identity.OrganizationPhoneContactItemBridges.Select(p => p.ContactItem).ToArray();
        }
    }
}
