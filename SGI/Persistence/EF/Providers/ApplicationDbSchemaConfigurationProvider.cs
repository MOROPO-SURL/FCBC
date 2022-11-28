using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators;

namespace MOROPO.FCBC.SGI.Persistence.EF.Providers
{
    public sealed class ApplicationDbSchemaConfigurationProvider : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => null;

        public System.Collections.Generic.ICollection<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new System.Collections.Generic.List<IEntityTypeConfigurator>
                {
                    new Configurators.CriticEntityTypeConfigurator(),
                    new Configurators.ContractorEntityTypeConfigurator(),
                    new Configurators.ArtistEntityTypeConfigurator(),
                    new Configurators.CreationGroupEntityTypeConfigurator()
                };
            }
        }
    }
}