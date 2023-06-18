using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;

namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class EmailContactItemDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<EmailContactItem, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Label { get; set; }

        public string EmailAddress { get; set; }

        public void Import(EmailContactItem entity)
        {
            Id = entity.Id;
            Label = entity.Label;
            EmailAddress = entity.EmailAddress;
        }
    }
}
