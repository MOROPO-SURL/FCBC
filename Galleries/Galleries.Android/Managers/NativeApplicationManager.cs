[assembly: Xamarin.Forms.Dependency(typeof(MOROPO.FCBC.Galleries.Droid.Managers.NativeApplicationManager))]

namespace MOROPO.FCBC.Galleries.Droid.Managers
{
    public sealed class NativeApplicationManager : Galleries.Managers.Facade.INativeApplicationManagerFacade
    {
        public void Close()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}