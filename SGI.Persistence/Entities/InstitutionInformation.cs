namespace MOROPO.FBCB.SGI.Persistence.Entities
{
    public class InstitutionInformation : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities.IEntity<Enumerations.InstitutionInformationKind>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Enumerations.InstitutionInformationKind Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Value { get; set; }
    }
}
