using System.Security.Cryptography.X509Certificates;

namespace MOROPO.FCBC.Galleries.UWP.Installer.Wind0ws
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private System.Threading.CancellationTokenSource actionCancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();

            actionCancellationTokenSource = new System.Threading.CancellationTokenSource();
        }

        private void MainActionButton_Click(object sender, System.Windows.RoutedEventArgs eventArgs)
        {
            MainProgressBar.Value = 0;
            MainProgressBar.IsIndeterminate = true;
            MainActionButton.IsEnabled = false;

            var progress = new System.Progress<Models.View.ProgressDetailViewModel>(model => Progress_Callback(model));

            System.Threading.Tasks.Task.Run(() =>
            {
                var certificate = new X509Certificate2(System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "AmaliaGallery.cer"));

                var certificateStore = new X509Store(StoreName.AuthRoot, StoreLocation.LocalMachine);

                certificateStore.Open(OpenFlags.ReadWrite);

                var installedCertificates = certificateStore.Certificates.Find(X509FindType.FindBySerialNumber, certificate.SerialNumber, false);

                if (installedCertificates.Count <= 0)
                {
                    certificateStore.Add(certificate);

                    certificateStore.Close();
                }
                
                System.Threading.Thread.Sleep(System.TimeSpan.FromSeconds(2));
            }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Dispatcher.Invoke(() =>
                    {
                        MainProgressBar.IsIndeterminate = false;
                        MainProgressBar.Value = 0;

                        MainActionButton.IsEnabled = true;

                        System.Windows.MessageBox.Show("No fue posible inicial el proceso de instalación. Por favor intentelo más tarde.", "Aviso");
                    });
                }

                actionCancellationTokenSource = new System.Threading.CancellationTokenSource();

                Helpers.AppxBundleSetupHelper.SetupAsync(Enumerations.SetupMode.Install, progress, actionCancellationTokenSource.Token).ContinueWith(_task => 
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (_task.IsCanceled || _task.IsFaulted)
                        {
                            MainProgressBar.IsIndeterminate = false;
                            MainProgressBar.Value = 0;

                            MainActionButton.IsEnabled = true;

                            System.Windows.MessageBox.Show(string.Format("El proceso de instalación no pudo realizarse de forma exitosamente.{0}{0}Detalles:{1}.", System.Environment.NewLine, _task.Exception.Message), "Error!");
                        }
                        else
                        {
                            MainProgressBar.IsIndeterminate = false;

                            MainActionButton.IsEnabled = true;

                            if (_task.Result.ExtendedErrorCode != null)
                            {
                                MainProgressBar.Value = 0;

                                System.Windows.MessageBox.Show(string.Format("El proceso de instalación no pudo completarse de forma exitosamente.{0}{0}Detalles:{1}.", System.Environment.NewLine, _task.Result.ErrorText), "Error!");
                            }
                            else
                            {
                                System.Windows.MessageBox.Show(string.Format("El proceso de instalación a concluido exitosamente.{0}{0}Ya puedes disfrutar de la aplicación Galería Amalia buscándola en el menú Inicio de Windows 10.", System.Environment.NewLine), "Listo!");
                            }
                        }
                    });
                });
            });
        }

        private void Progress_Callback(Models.View.ProgressDetailViewModel model)
        {
            if (MainProgressBar.IsIndeterminate)
            {
                MainProgressBar.IsIndeterminate = false;
            }

            MainProgressBar.Value = model.Percentage;
        }
    }
}
