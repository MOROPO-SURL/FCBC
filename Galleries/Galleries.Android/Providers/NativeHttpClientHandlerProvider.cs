using Javax.Net.Ssl;

[assembly: Xamarin.Forms.Dependency(typeof(MOROPO.FCBC.Galleries.Droid.Providers.NativeHttpClientHandlerProvider))]

namespace MOROPO.FCBC.Galleries.Droid.Providers
{
    public sealed class NativeHttpClientHandlerProvider : Galleries.Providers.Facade.INativeHttpClientHandlerProviderFacade
    {
        public System.Net.Http.HttpClientHandler Get()
        {
            return new IgnoreSSLClientHandler();
        }

        internal class IgnoreSSLClientHandler : Xamarin.Android.Net.AndroidClientHandler
        {
            protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
            {
                return Android.Net.SSLCertificateSocketFactory.GetInsecure(1000, null);
            }

            protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
            {
                return new IgnoreSSLHostnameVerifier();
            }
        }

        internal class IgnoreSSLHostnameVerifier : Java.Lang.Object, IHostnameVerifier
        {
            public bool Verify(string hostname, ISSLSession session)
            {
                return true;
            }
        }
    }
}