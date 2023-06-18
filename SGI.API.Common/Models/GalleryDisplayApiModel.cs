using System.Linq;

namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class GalleryDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Gallery, Persistence.Enumerations.GalleryId>
    {
        public Persistence.Enumerations.GalleryId Id { get; set; }

        public string Name { get; set; }

        public System.DateTime? BornDate { get; set; }

        public string About { get; set; }

        public string History { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public EmailContactItemDisplayApiModel[] EmailContacts { get; set; }

        public PhoneContactItemDisplayApiModel[] PhoneContacts { get; set; }

        public WebContactItemDisplayApiModel[] WebContacts { get; set; }

        public void Import(Persistence.Entities.Gallery entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            BornDate = entity.Identity.BornDate;
            About = entity.Identity.About;
            ImageId = entity.Identity.ImageId;
            History = entity.History;
            EmailContacts = entity.Identity.OrganizationEmailContactItemBridges.Select(bridge =>
            {
                var model = new EmailContactItemDisplayApiModel();

                model.Import(bridge.ContactItem);

                return model;
            }).ToArray();
            PhoneContacts = entity.Identity.OrganizationPhoneContactItemBridges.Select(bridge =>
            {
                var model = new PhoneContactItemDisplayApiModel();

                model.Import(bridge.ContactItem);

                return model;
            }).ToArray();
            WebContacts = entity.Identity.OrganizationSocialContactItemBridges.Select(bridge =>
            {
                return new WebContactItemDisplayApiModel
                {
                    Id = bridge.ContactItemId,
                    Label = bridge.ContactItem.Label,
                    URL = bridge.ContactItem.SocialId
                };
            }).ToArray();
        }
    }
}
