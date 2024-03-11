using System.Linq;

namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class EmployeeDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Employee, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public string About { get; set; }

        public EmailContactItemDisplayApiModel[] EmailContacts { get; set; }

        public PhoneContactItemDisplayApiModel[] PhoneContacts { get; set; }

        public void Import(Persistence.Entities.Employee entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            ImageId = entity.Identity.ImageId;
            Sex = entity.Identity.Sex;
            About = entity.Identity.About;
            EmailContacts = entity.Identity.PersonEmailContactItemBridges.Select(bridge =>
            {
                var model = new EmailContactItemDisplayApiModel();

                model.Import(bridge.ContactItem);

                return model;
            }).ToArray();
            PhoneContacts = entity.Identity.PersonPhoneContactItemBridges.Select(bridge =>
            {
                var model = new PhoneContactItemDisplayApiModel();

                model.Import(bridge.ContactItem);

                return model;
            }).ToArray();
        }
    }
}
