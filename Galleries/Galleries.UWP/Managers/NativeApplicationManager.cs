[assembly: Xamarin.Forms.Dependency(typeof(MOROPO.FCBC.Galleries.UWP.Managers.NativeApplicationManager))]

namespace MOROPO.FCBC.Galleries.UWP.Managers
{
    public class NativeApplicationManager : Galleries.Managers.Facade.INativeApplicationManagerFacade
    {
        public void Close()
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}
