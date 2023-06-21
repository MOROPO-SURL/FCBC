using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;

namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class PhoneContactItemDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<PhoneContactItem, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Label { get; set; }

        public string PhoneNumber { get; set; }

        public void Import(PhoneContactItem entity)
        {
            Id = entity.Id;
            Label = entity.Label;
            PhoneNumber = entity.PhoneNumber;
        }
    }
}
