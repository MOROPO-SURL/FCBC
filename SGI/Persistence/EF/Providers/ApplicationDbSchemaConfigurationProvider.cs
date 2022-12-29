using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Configurators;

namespace MOROPO.FCBC.SGI.Persistence.EF.Providers
{
    public sealed class ApplicationDbSchemaConfigurationProvider : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public string DefaultPersistenceSchema => null;

        public System.Collections.Generic.ICollection<IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new System.Collections.Generic.List<IEntityTypeConfigurator>
                {
                    new Configurators.CriticEntityTypeConfigurator(),
                    new Configurators.ContractorEntityTypeConfigurator(),
                    new Configurators.ArtistEntityTypeConfigurator(),
                    new Configurators.CreationGroupEntityTypeConfigurator(),
                    new Configurators.AssociationEntityTypeConfigurator(),
                    new Configurators.EmployeeEntityTypeConfigurator(),
                    new Configurators.EmployeeAdministrativeRoleBridgeEntityTypeConfigurator(),
                    new Configurators.ArtistArtisticManifestationBridgeEntityTypeConfigurator(),
                    new Configurators.ArtistCraftManifestationBridgeEntityTypeConfigurator(),
                    new Configurators.ArtistDesignManifestationBridgeEntityTypeConfigurator(),
                    new Configurators.ArtistServiceLineBridgeEntityTypeConfigurator(),
                    new Configurators.CreationGroupArtisticManifestationBridgeEntityTypeConfigurator(),
                    new Configurators.CreationGroupCraftManifestationBridgeEntityTypeConfigurator(),
                    new Configurators.CreationGroupDesignManifestationBridgeEntityTypeConfigurator(),
                    new Configurators.CreationGroupServiceLineBridgeEntityTypeConfigurator(),
                    new Configurators.ArtistResolutionLockEntityTypeConfigurator(),
                    new Configurators.ArtistAssotiationLockEntityTypeConfigurator(),
                    new Configurators.CreationGroupArtistBridgeEntityTypeConfigurator(),
                    new Configurators.ArtWorkTechniqueBridgeEntityTypeConfigurator(),
                    new Configurators.CraftWorkTechniqueBridgeEntityTypeConfigurator(),
                    new Configurators.DesignWorkTechniqueBridgeEntityTypeConfigurator(),
                    new Configurators.ServiceWorkTechniqueBridgeEntityTypeConfigurator(),
                    new Configurators.ArtWorkImageBridgeEntityTypeConfigurator(),
                    new Configurators.CraftWorkImageBridgeEntityTypeConfigurator(),
                    new Configurators.DesignWorkImageBridgeEntityTypeConfigurator(),
                    new Configurators.ServiceWorkImageBridgeEntityTypeConfigurator(),
                    new Configurators.ArtWorkMaterialBridgeEntityTypeConfigurator(),
                    new Configurators.ArtWorkCreatorBridgeEntityTypeConfigurator(),
                    new Configurators.ArtWorkCreationGroupBridgeEntityTypeConfigurator(),
                    new Configurators.ArtistAwardEntityTypeConfiguratior(),
                    new Configurators.ArtistRecognitionEntityTypeConfigurator(),
                    new Configurators.ArtistDistinctionBridgeEntityTypeConfigurator(),
                    new Configurators.CreationGroupAwardBridgeEntityTypeConfigurator(),
                    new Configurators.CreationGroupDistinctionBridgeEntityTypeConfigurator(),
                    new Configurators.CreationGroupRecognitionBridgeEntityTypeConfigurator()
                };
            }
        }
    }
}