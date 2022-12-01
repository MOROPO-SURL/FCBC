using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Models.View;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Extensions
{
    public static class ManagementDashboardSectionProviderEnumerableExtensions
    {
        public static Dictionary<string, ICollection<ManagementDashboardSectionViewModel>> ToManagementDashboardSectionGroups(this IEnumerable<SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> self)
        {
            var securitySections = new List<ManagementDashboardSectionViewModel>();

            foreach (var provider in self)
            {
                if (provider is SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Providers.LocalSecurityWebManagementDashboardSectionProvider)
                {
                    securitySections.AddRange(provider.GetManagementDashboardSections());
                }                
            }

            securitySections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Registros",
                Controller = "traces",
                Action = "index",
                Area = "Security",
                Role = Security.Authorization.Roles.TraceViewer
            });

            var complementsSections = new List<ManagementDashboardSectionViewModel>();

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Manifestación Artística",
                Controller = "artisticmanifestations",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.ArtisticManifestationViewer
            });

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Premios",
                Controller = "awards",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.AwardViewer
            });

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Distinciones",
                Controller = "distinctions",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.DistinctionViewer
            });

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Reconocimientos",
                Controller = "recognitions",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.RecognitionViewer
            });

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Técnicas",
                Controller = "techniques",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.TechniqueViewer
            });

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Tipo de Contratista",
                Controller = "contractorkinds",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.ContractorKindViewer
            });

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Critico",
                Controller = "critics",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.CriticViewer
            });

            var institutionSections = new List<ManagementDashboardSectionViewModel>();

            institutionSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Servicios",
                Controller = "services",
                Action = "index",
                Area = "institution",
                Role = Security.Authorization.Roles.ServiceViewer
            });

            institutionSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Contratista",
                Controller = "contractors",
                Action = "index",
                Area = "institution",
                Role = Security.Authorization.Roles.ContractorViewer
            });


            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Temáticas",
                Controller = "thematics",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.ThematicViewer
            });

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Monedas",
                Controller = "currencies",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.CurrencyViewer
            });

            complementsSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Rol Administartivos",
                Controller = "administrativeroles",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.AdministrativeRolesViewer
            });

            institutionSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Artista",
                Controller = "artists",
                Action = "index",
                Area = "institution",
                Role = Security.Authorization.Roles.ArtistViewer
            });

            institutionSections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Trabajadores",
                Controller = "employees",
                Action = "index",
                Area = "institution",
                Role = Security.Authorization.Roles.EmployeeViewer
            });

            return new Dictionary<string, ICollection<ManagementDashboardSectionViewModel>>
            {
                { "Seguridad", securitySections },
                { "Complementos", complementsSections },
                { "Institución", institutionSections }
            };
        }
    }
}
