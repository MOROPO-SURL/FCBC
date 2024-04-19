namespace MOROPO.FCBC.SGI.API.Connect.Clients
{
    public class GalleryConsultApiConnectClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityConsultApiConnectClient<Persistence.Entities.Gallery, Persistence.Enumerations.GalleryId, Common.Models.GalleryDisplayApiModel>
    {
        public GalleryConsultApiConnectClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => string.Format("{0}/api/galleries", System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
    }
}
