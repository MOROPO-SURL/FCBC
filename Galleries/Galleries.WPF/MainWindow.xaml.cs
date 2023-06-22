namespace MOROPO.FCBC.Galleries.WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Xamarin.Forms.Platform.WPF.FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();

            Xamarin.Forms.Forms.Init();

            LoadApplication(new Galleries.App());
        }
    }
}
