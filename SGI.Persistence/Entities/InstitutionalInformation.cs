using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class InstitutionalInformation : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<InstitutionalInformationKind>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public InstitutionalInformationKind Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Value { get; set; }
    }
}
