
using System;
using System.Threading.Tasks;
using Urho;

namespace MOROPO.FCBC.Galleries.Views.Rendered
{
	public class UrhoSurfaceRenderedView : Xamarin.Forms.View
    {
		private readonly System.Threading.Tasks.TaskCompletionSource<System.Func<Urho.ApplicationOptions, System.Threading.Tasks.Task<Galleries.Presenters.VirtualGalleryRoomPresenter>>> runnerInitTaskSource;

        public UrhoSurfaceRenderedView()
		{
			runnerInitTaskSource = new System.Threading.Tasks.TaskCompletionSource<System.Func<Urho.ApplicationOptions, System.Threading.Tasks.Task<Galleries.Presenters.VirtualGalleryRoomPresenter>>>();
		}

        public void RegisterRunner(System.Func<Urho.ApplicationOptions, System.Threading.Tasks.Task<Galleries.Presenters.VirtualGalleryRoomPresenter>> runner)
        {
            runnerInitTaskSource.TrySetResult(runner);
        }

        public async Task<Galleries.Presenters.VirtualGalleryRoomPresenter> Show(ApplicationOptions options)
        {
            var runner = await runnerInitTaskSource.Task;

            if (runner == null)
            {
                throw new InvalidOperationException("UrhoRunner should not be null.");
            }

            return (Galleries.Presenters.VirtualGalleryRoomPresenter)await runner(options);
        }
    }
}

