using System;
using AppKit;
using Foundation;
using MOROPO.FCBC.Galleries;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace MOROPO.FCBC.Galleries.macOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : FormsApplicationDelegate
    {
        private readonly NSWindow window;

        public AppDelegate()
        {
            var style = NSWindowStyle.FullSizeContentView | NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Titled;

            window = new NSWindow(NSScreen.MainScreen.Frame, style, NSBackingStore.Buffered, false);

            window.Title = "Galería Amalia"; // choose your own Title here
            window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override NSWindow MainWindow
        {
            get { return window; }
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            NSApplication.SharedApplication.MainMenu = MakeMainMenu();

            Forms.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            LoadApplication(new App());

            base.DidFinishLaunching(notification);
        }

        private NSMenu MakeMainMenu()
        {
            // top bar app menu
            var menubar = new NSMenu();

            var appMenuItem = new NSMenuItem();

            menubar.AddItem(appMenuItem);

            var appMenu = new NSMenu();

            appMenuItem.Submenu = appMenu;

            // add about
            var aboutMenuItem = new NSMenuItem(string.Format("Acerca de {0}", "Galería Amalia"), delegate
            {
                NSApplication.SharedApplication.OrderFrontStandardAboutPanel2(menubar);
            });

            appMenu.AddItem(aboutMenuItem);

            // add separator
            appMenu.AddItem(NSMenuItem.SeparatorItem);

            // add hide window menu item
            var hideWindowMenuItem = new NSMenuItem("Ocultar aplicación", "h", delegate
            {
                NSApplication.SharedApplication.Hide(menubar);
            });

            appMenu.AddItem(hideWindowMenuItem);

            // add show all menu item
            var showAllMenuItem = new NSMenuItem("Mostrar todo", delegate
            {
                NSApplication.SharedApplication.UnhideAllApplications(menubar);
            });

            appMenu.AddItem(showAllMenuItem);

            // add separator
            appMenu.AddItem(NSMenuItem.SeparatorItem);

            // add quit menu item
            var quitMenuItem = new NSMenuItem("Salir", "q", delegate
            {
                NSApplication.SharedApplication.Terminate(menubar);
            });

            appMenu.AddItem(quitMenuItem);

            return menubar;
        }
    }
}

