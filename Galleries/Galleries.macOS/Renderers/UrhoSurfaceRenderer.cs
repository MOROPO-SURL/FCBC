using System.Threading.Tasks;
using MOROPO.FCBC.Galleries.Views.Rendered;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRendererAttribute(typeof(UrhoSurfaceRenderedView), typeof(MOROPO.FCBC.Galleries.macOS.Renderers.UrhoSurfaceRenderer))]

namespace MOROPO.FCBC.Galleries.macOS.Renderers
{
	public class UrhoSurfaceRenderer : Xamarin.Forms.Platform.MacOS.ViewRenderer<UrhoSurfaceRenderedView, Urho.Extensions.Cocoa.UrhoSurface>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<UrhoSurfaceRenderedView> e)
        {
            if (e.NewElement != null)
            {
                var surface = new Urho.Extensions.Cocoa.UrhoSurface();

                e.NewElement.RegisterRunner(surface.Show<Galleries.Presenters.VirtualGalleryRoomPresenter>);

                SetNativeControl(surface);
            }

            base.OnElementChanged(e);
        }
    }
}