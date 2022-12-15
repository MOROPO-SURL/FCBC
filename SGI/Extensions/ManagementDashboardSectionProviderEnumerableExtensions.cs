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
                    Title = "Artísticas",
                    Controller = "artisticmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanales",
                    Controller = "craftmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "servicelines",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
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
                    Role = Security.Authorization.Roles.TechniqueViewer
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
                    Title = "Tipo de Manifestación",
                    Controller = "manifestationkinds",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Roles.ManifestationKindViewer
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
                    Title = "Artísticas",
                    Controller = "artworks",
                    Action = "index",
                    Area = "works",
                    Role = Security.Authorization.Roles.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanales",
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

            return new Dictionary<string, ICollection<ManagementDashboardSectionViewModel>>
            {
                { "Seguridad", securitySection },
                { "SKIP1", null },
                { "Institución", institutionSection },
                { "Obras", worksSection },
                { "Manifestaciones", manifestationsSection },
                { "Complementarios", complementsSection }
            };
        }
    }
}
