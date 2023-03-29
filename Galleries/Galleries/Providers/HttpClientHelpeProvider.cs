using MOROPO.SAPRESSC.Core.Api.Connect.Helpers;
using System.Collections.Generic;

namespace MOROPO.FCBC.Galleries.Providers
{
    public sealed class HttpClientHelpeProvider : SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade
    {
        private static HttpClientHelpeProvider instance;

        public static HttpClientHelpeProvider Instance
        {
            get 
            {                
                if (instance == null) instance = new HttpClientHelpeProvider();

                return instance;
            }
        }

        public HttpClientHelper HttpClientHelper { get; private set; }

        private HttpClientHelpeProvider()
        {
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                HttpClientHelper = new HttpClientHelper(Xamarin.Forms.DependencyService.Get<Providers.Facade.INativeHttpClientHandlerProviderFacade>().Get(), Constants.Networking.API_BASE_URL, null, () => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(SGI.API.Common.Constants.Authorization.API_KEY_HEADER_PROPERTY, Constants.Security.TOKEN) });
            }
            else
            {
                HttpClientHelper = new HttpClientHelper(Constants.Networking.API_BASE_URL, null, () => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(SGI.API.Common.Constants.Authorization.API_KEY_HEADER_PROPERTY, Constants.Security.TOKEN) });
            }
        }
    }
}
