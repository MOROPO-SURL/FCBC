using MOROPO.SAPRESSC.Core.Api.Connect.Helpers;
using System.Collections.Generic;

namespace MOROPO.FCBC.Galleries.Providers
{
    public sealed class HttpClientHelperProvider : SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade
    {
        private static HttpClientHelperProvider instance;

        public static HttpClientHelperProvider Instance
        {
            get 
            {                
                if (instance == null) instance = new HttpClientHelperProvider();

                return instance;
            }
        }

        public HttpClientHelper HttpClientHelper { get; private set; }

        private HttpClientHelperProvider()
        {
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                var handler = Xamarin.Forms.DependencyService.Get<Facade.INativeHttpClientHandlerProviderFacade>().Get();

                handler.ServerCertificateCustomValidationCallback = (message, certificate, chain, errors) => true;

                HttpClientHelper = new HttpClientHelper(handler, Constants.Networking.SGI_BASE_URL, null, () => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(SGI.API.Common.Constants.Authorization.API_KEY_HEADER_PROPERTY, Constants.Security.TOKEN) });
            }
            else
            {
                HttpClientHelper = new HttpClientHelper(new System.Net.Http.HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, certificate, chain, errors) => true
                }, Constants.Networking.SGI_BASE_URL, null, () => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(SGI.API.Common.Constants.Authorization.API_KEY_HEADER_PROPERTY, Constants.Security.TOKEN) });
            }
        }
    }
}
