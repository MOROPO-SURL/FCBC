[assembly: Xamarin.Forms.Dependency(typeof(MOROPO.FCBC.Galleries.Droid.Providers.NativeHttpClientHandlerProvider))]

namespace MOROPO.FCBC.Galleries.Droid.Providers
{
    public sealed class NativeHttpClientHandlerProvider : Galleries.Providers.Facade.INativeHttpClientHandlerProviderFacade
    {
        public System.Net.Http.HttpClientHandler Get()
        {
            return new Xamarin.Android.Net.AndroidClientHandler();
        }
    }
}