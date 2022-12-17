using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Models.View;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Extensions
{
    public static class ManagementDashboardSectionProviderEnumerableExtensions
    {
        public static Dictionary<string, ICollection<ManagementDashboardSectionViewModel>> ToManagementDashboardSectionGroups(this IEnumerable<SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> self)
        {
            var securitySection = new List<ManagementDashboardSectionViewModel>();

            foreach (var provider in self)
            {
                if (provider is SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Providers.LocalSecurityWebManagementDashboardSectionProvider)
                {
                    securitySection.AddRange(provider.GetManagementDashboardSections());
                }                
            }

            securitySection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Registros",
                Controller = "traces",
                Action = "index",
                Area = "security",
                Role = Security.Authorization.Roles.TraceViewer
            });

            var manifestationsSection = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artes Plásticas",
                    Controller = "artisticmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanía",
                    Controller = "craftmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Roles.CraftManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Roles.DesignManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "servicelines",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Roles.ServiceLineViewer
                }
            };

            var complementsSection = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Premios",
                    Controller = "awards",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.AwardViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Distinciones",
                    Controller = "distinctions",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.DistinctionViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Reconocimientos",
                    Controller = "recognitions",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.RecognitionViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Técnicas",
                    Controller = "techniques",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.ArtisticTechniqueViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Tipo de Contratista",
                    Controller = "contractorkinds",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.ContractorKindViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Critico",
                    Controller = "critics",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.CriticViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Resolución",
                    Controller = "resolutions",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.CriticViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Temáticas",
                    Controller = "thematics",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.ThematicViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Monedas",
                    Controller = "currencies",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.CurrencyViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Rol Administartivos",
                    Controller = "administrativeroles",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.AdministrativeRolesViewer
                }
            };

            var institutionSection = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Información",
                    Controller = "information",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Roles.InstitutionalInformationViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "services",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Roles.ServiceViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Contratista",
                    Controller = "contractors",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Roles.ContractorViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artista",
                    Controller = "artists",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Roles.ArtistViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Grupos de Creación",
                    Controller = "creationgroups",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Roles.CreationGroupViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Asociaciones",
                    Controller = "associations",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Roles.AssociationViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Trabajadores",
                    Controller = "employees",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Roles.EmployeeViewer
                }
            };

            var worksSection = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artes Plásticas",
                    Controller = "artworks",
                    Action = "index",
                    Area = "works",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanía",
                    Controller = "craftworks",
                    Action = "index",
                    Area = "works",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designworks",
                    Action = "index",
                    Area = "works",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "serviceworks",
                    Action = "index",
                    Area = "works",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                }
            };

            var manifestionGroupsSection = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artes Plásticas",
                    Controller = "artisticmanifestationgroups",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.ArtisticManifestationGroupViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanía",
                    Controller = "craftmanifestationgroups",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.CraftManifestationGroupViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designmanifestationgroups",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.DesignManifestationGroupViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "servicelinegroups",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.ServiceLineGroupViewer
                }
            };

            var techniquesSection = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artes Plásticas",
                    Controller = "artistictechniques",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.ArtisticTechniqueViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanía",
                    Controller = "crafttechniques",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.CraftTechniqueViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designtechniques",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.DesignTechniqueViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "servicetechniques",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.ServiceTechniqueViewer
                }
            };

            return new Dictionary<string, ICollection<ManagementDashboardSectionViewModel>>
            {
                { "Seguridad", securitySection },
                { "Institución", institutionSection },
                { "Obras", worksSection },
                { "SKIP-2", null },
                { "Manifestaciones", manifestationsSection },
                { "SKIP-3", null },
                { "Grupos de Manifestaciones", manifestionGroupsSection },
                { "Técnicas", techniquesSection },
                { "SKIP-4", null },
                { "Auxiliares", complementsSection }
            };
        }
    }
}
