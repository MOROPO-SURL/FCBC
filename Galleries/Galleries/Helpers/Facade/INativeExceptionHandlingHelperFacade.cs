namespace MOROPO.FCBC.Galleries.Helpers.Facade
{
    public interface INativeExceptionHandlingHelperFacade
    {
        System.Exception ToCrossPlatformException(System.Exception exception);
    }
}
