[assembly: Xamarin.Forms.Dependency(typeof(MOROPO.FCBC.Galleries.macOS.Managers.NativeApplicationManager))]

namespace MOROPO.FCBC.Galleries.macOS.Managers
{
	public class NativeApplicationManager : Galleries.Managers.Facade.INativeApplicationManagerFacade
	{
		public void Close()
        {
            AppKit.NSApplication.SharedApplication.Terminate(null);
        }
    }
}