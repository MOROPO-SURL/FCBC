namespace MOROPO.FCBC.SGI.API.Connect.Clients
{
    public class ExhibitConsultApiConnectClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityConsultFilteredApiConnectClient<Persistence.Entities.Exhibit, System.Guid, Common.Models.ExhibitDisplayApiModel, Common.Models.Filter.ExhibitFilterApiModel>
    {
        public System.Globalization.CultureInfo Culture { get; set; }

        public ExhibitConsultApiConnectClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => string.Format("{0}/api/exhibits", Culture?.TwoLetterISOLanguageName ?? System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
    }
}
