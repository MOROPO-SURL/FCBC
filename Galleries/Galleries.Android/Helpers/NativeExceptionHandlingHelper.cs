[assembly: Xamarin.Forms.Dependency(typeof(MOROPO.FCBC.Galleries.Droid.Helpers.NativeExceptionHandlingHelper))]

namespace MOROPO.FCBC.Galleries.Droid.Helpers
{
    public sealed class NativeExceptionHandlingHelper : Galleries.Helpers.Facade.INativeExceptionHandlingHelperFacade
    {
        public System.Exception ToCrossPlatformException(System.Exception exception)
        {
            if (exception is Java.Net.UnknownHostException)
            {
                return new System.Net.Http.HttpRequestException(exception.Message, exception);
            }

            return exception;
        }
    }
}