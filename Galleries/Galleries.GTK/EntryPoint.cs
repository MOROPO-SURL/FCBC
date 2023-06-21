namespace MOROPO.FCBC.Galleries.GTK
{
    public class EntryPoint
    {
        [System.STAThread]
        public static void Main(string[] args)
        {
            Gtk.Application.Init();

            Xamarin.Forms.Forms.Init();

            var window = new Xamarin.Forms.Platform.GTK.FormsWindow();
            
            window.LoadApplication(new App());
            window.SetApplicationTitle("Galería Amalia");
            window.Show();

            Gtk.Application.Run();
        }
    }
}
