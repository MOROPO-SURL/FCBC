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
                Area = "business",
                Role = Security.Authorization.Roles.TraceViewer
            });

            securitySections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Manifestación Artística",
                Controller = "artisticmanifestations",
                Action = "index",
                Area = "complements",
                Role = Security.Authorization.Roles.ArtisticManifestationViewer
            });

            return new Dictionary<string, ICollection<ManagementDashboardSectionViewModel>>
            {
                { "Seguridad", securitySections }
            };
        }
    }
}
