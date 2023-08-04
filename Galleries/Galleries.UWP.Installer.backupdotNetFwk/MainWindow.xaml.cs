using System.Security.Cryptography.X509Certificates;

namespace MOROPO.FCBC.Galleries.UWP.Installer
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private System.Threading.CancellationTokenSource actionCancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs eventArgs)
        {
            (sender as System.Windows.Controls.Button).IsEnabled = false;

            var progress = new System.Progress<Models.View.ProgressDetailViewModel>(model => Progress_Callback(model));

            System.Threading.Tasks.Task.Run(() =>
            {
                var certificate = new X509Certificate2(System.IO.Path.Combine(System.Environment.CurrentDirectory, "Resources", "Galleries.UWP_0.1.0.0_x86_x64_arm.cer"));

                var personalCertificateStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                var trustedPeopleCertificateStore = new X509Store(StoreName.TrustedPeople, StoreLocation.CurrentUser);

                personalCertificateStore.Open(OpenFlags.ReadWrite);
                trustedPeopleCertificateStore.Open(OpenFlags.ReadWrite);

                var installedPersonalCertificates = personalCertificateStore.Certificates.Find(X509FindType.FindBySerialNumber, certificate.SerialNumber, false);
                var installedTrustedPeopleCertificates = trustedPeopleCertificateStore.Certificates.Find(X509FindType.FindBySerialNumber, certificate.SerialNumber, false);

                if (installedPersonalCertificates.Count <= 0)
                {
                    personalCertificateStore.Add(certificate);

                    personalCertificateStore.Close();
                }

                if (installedTrustedPeopleCertificates.Count <= 0)
                {
                    trustedPeopleCertificateStore.Add(certificate);

                    trustedPeopleCertificateStore.Close();
                }
            }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {

                }

                actionCancellationTokenSource = new System.Threading.CancellationTokenSource();

                Helpers.AppxBundleSetupHelper.SetupAsync(Enumerations.SetupMode.Install, progress, actionCancellationTokenSource.Token);
            });
        }

        private void Progress_Callback(Models.View.ProgressDetailViewModel model)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Progress: {0}, Message: {1}", model.Percentage, model.Message));
        }
    }
}
