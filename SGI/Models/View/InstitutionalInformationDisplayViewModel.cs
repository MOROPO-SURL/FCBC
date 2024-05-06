using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class InstitutionalInformationDisplayViewModel:  SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.InstitutionalInformation, Persistence.Enumerations.InstitutionalInformationKind>
    {
        public Persistence.Enumerations.InstitutionalInformationKind Id { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }
       

        public void Import(InstitutionalInformation entity)
        {
            Id = entity.Id;
            Value = entity.Value;
        }
    }
}
