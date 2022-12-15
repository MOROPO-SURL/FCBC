using MOROPO.FCBC.SGI.Persistence.Enumerations;

namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class InstitutionInformation : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<InstitutionInformationKind>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public InstitutionInformationKind Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Value { get; set; }
    }
}
