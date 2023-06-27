using AppKit;
using CoreAnimation;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Urho;
using Urho.Actions;

namespace MOROPO.FCBC.Galleries.macOS.Views
{
	public class UrhoSurfaceView : NSView
    {
        static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);

        bool paused;

        public UrhoSurfaceView()
        {
            AutoresizingMask = NSViewResizingMask.WidthSizable | NSViewResizingMask.HeightSizable;
        }

        public override void ViewDidMoveToWindow()
        {
            base.ViewDidMoveToWindow();

            PostsFrameChangedNotifications = true;
            PostsBoundsChangedNotifications = true;
        }

        public override void SetFrameSize(CoreGraphics.CGSize newSize)
        {
            base.SetFrameSize(newSize);

            if (Urho.Application.HasCurrent)
            {
                NSOpenGLContext.CurrentContext?.Update();
            }
        }

        public bool Paused
        {
            get { return paused; }
            set
            {
                if (paused && !value)
                {
                    paused = value;

                    StartLoop();
                }
                else
                {
                    paused = value;
                }
            }
        }

        public int FpsLimit { get; set; } = 60;

        public static Color ConvertColor(NSColor color)
        {
            nfloat r, g, b, a;
            color.GetRgba(out r, out g, out b, out a);
            return new Color((float)r, (float)g, (float)b, (float)a);
        }

        public async Task<TApplication> Show<TApplication>(ApplicationOptions opts = null) where TApplication : Application
        {
            return (TApplication)(await Show(typeof(TApplication), opts));
        }

        [DllImport("@rpath/Urho.framework/Urho")]
        extern static void SDL_Cocoa_SetExternalView(IntPtr view);

        public async Task<Application> Show(Type appType, ApplicationOptions opts = null)
        {
            paused = false;
            opts = opts ?? new ApplicationOptions();
            await Semaphore.WaitAsync();
            if (Application.HasCurrent)
            {
                await Application.Current.Exit();
            }

            await Task.Yield();

            opts.ExternalWindow = Handle;
            opts.DelayedStart = true;
            opts.LimitFps = false;
            opts.ResourcePrefixPaths = new string[] { Environment.CurrentDirectory };
            WantsBestResolutionOpenGLSurface = opts.HighDpi;

            var app = Application.CreateInstance(appType, opts);

            app.Run();

            Hidden = true;

            Semaphore.Release();

            StartLoop();

            await Task.Yield();

            Hidden = false;

            return app;
        }

        public void Stop()
        {
            Application.Current?.Exit();
            //Controls.Clear();
        }

        async void StartLoop()
        {
            while (!Paused && Application.Current != null && Application.Current.IsActive)
            {
                var elapsed = Application.Current.Engine.RunFrame();
                var targetMax = 1000000L / FpsLimit;
                if (elapsed >= targetMax)
                    await Task.Yield();
                else
                {
                    var ts = TimeSpan.FromMilliseconds((targetMax - elapsed) / 1000d);

                    await Task.Delay(ts);
                }
            }
        }
    }
}

