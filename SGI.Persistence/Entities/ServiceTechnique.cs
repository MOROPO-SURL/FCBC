namespace MOROPO.FCBC.SGI.Persistence.Entities
{
    public class ServiceTechnique : Contract.AbstractTechnique
    {
        public System.Collections.Generic.List<ServiceTechniqueTranslation> Translations { get; set; }
    }
}
