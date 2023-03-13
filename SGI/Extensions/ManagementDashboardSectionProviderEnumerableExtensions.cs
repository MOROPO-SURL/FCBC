using MOROPO.SAPRESSC.Core.Web.Backend.Models.View;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Extensions
{
    public static class ManagementDashboardSectionProviderEnumerableExtensions
    {
        public static Dictionary<string, ICollection<ManagementDashboardSectionViewModel>> ToManagementDashboardSectionGroups(this IEnumerable<SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> self)
        {
            var securitySection = new List<ManagementDashboardSectionViewModel>();

            foreach (var provider in self)
            {
                if (provider is SAPRESSC.Middleware.Security.Essential.Web.Backend.Providers.ManagementDashboardSectionProvider)
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
                Role = Security.Authorization.Permission.TraceViewer
            });

            var manifestationsSection = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artes Plásticas",
                    Controller = "artisticmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Permission.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanía",
                    Controller = "craftmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Permission.CraftManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designmanifestations",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Permission.DesignManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "servicelines",
                    Action = "index",
                    Area = "manifestations",
                    Role = Security.Authorization.Permission.ServiceLineViewer
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
                    Role = Security.Authorization.Permission.AwardViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Distinciones",
                    Controller = "distinctions",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.DistinctionViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Reconocimientos",
                    Controller = "recognitions",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.RecognitionViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Tipo de Contratista",
                    Controller = "contractorkinds",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.ContractorKindViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Crítico",
                    Controller = "critics",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.CriticViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Resolución",
                    Controller = "resolutions",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.ResolutionViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Acuerdos del CA",
                    Controller = "agreements",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.AgreementViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Temáticas",
                    Controller = "thematics",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.ThematicViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Monedas",
                    Controller = "currencies",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.CurrencyViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Rol Administartivos",
                    Controller = "administrativeroles",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.AdministrativeRolesViewer
                },

                 new ManagementDashboardSectionViewModel
                {
                    Title = "Unidades de Medidas",
                    Controller = "unitmeasurements",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.UnitMeasurementViewer
                },
                 

                 new ManagementDashboardSectionViewModel
                 {
                    Title = "Materiales",
                    Controller = "materials",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.MaterialViewer
                 },

                 new ManagementDashboardSectionViewModel
                {
                    Title = "Tipo de Actividad",
                    Controller = "activitykinds",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.ActivityKindViewer
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
                    Role = Security.Authorization.Permission.InstitutionalInformationViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "services",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Permission.ServiceViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Creadores",
                    Controller = "artists",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Permission.ArtistViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Grupos de Creación",
                    Controller = "creationgroups",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Permission.CreationGroupViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Contratistas",
                    Controller = "contractors",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Permission.ContractorViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Asociaciones",
                    Controller = "associations",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Permission.AssociationViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Trabajadores",
                    Controller = "employees",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Permission.EmployeeViewer
                },

                new ManagementDashboardSectionViewModel
                {
                    Title = "Actividades",
                    Controller = "activities",
                    Action = "index",
                    Area = "institution",
                    Role = Security.Authorization.Permission.ActivityViewer
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
                    Role = Security.Authorization.Permission.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanía",
                    Controller = "craftworks",
                    Action = "index",
                    Area = "works",
                    Role = Security.Authorization.Permission.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designworks",
                    Action = "index",
                    Area = "works",
                    Role = Security.Authorization.Permission.ArtisticManifestationViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "serviceworks",
                    Action = "index",
                    Area = "works",
                    Role = Security.Authorization.Permission.ArtisticManifestationViewer
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
                    Role = Security.Authorization.Permission.ArtisticManifestationGroupViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanía",
                    Controller = "craftmanifestationgroups",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.CraftManifestationGroupViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designmanifestationgroups",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.DesignManifestationGroupViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "servicelinegroups",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.ServiceLineGroupViewer
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
                    Role = Security.Authorization.Permission.ArtisticTechniqueViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Artesanía",
                    Controller = "crafttechniques",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.CraftTechniqueViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Diseño",
                    Controller = "designtechniques",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.DesignTechniqueViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Servicios",
                    Controller = "servicetechniques",
                    Action = "index",
                    Area = "complements",
                    Role = Security.Authorization.Permission.ServiceTechniqueViewer
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
