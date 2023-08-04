namespace MOROPO.FCBC.Galleries.UWP.Installer.Helpers
{
    internal class AssetSetupHelper
    {
        private System.Threading.CancellationTokenSource actionCancellationTokenSource;

        internal void Install(Enumerations.SetupMode mode, System.Action asyncCallbackAction, System.Action<Models.View.ProgressDetailViewModel> progressCallbackAction)
        {
            var progress = new System.Progress<Models.View.ProgressDetailViewModel>(model => progressCallbackAction.Invoke(model));

            //IsRunning = true;
            //ProgressTotal = 100;
            
            actionCancellationTokenSource = new System.Threading.CancellationTokenSource();
            
            //AppxBundleSetupHelper.SetupAsync(Enumerations.SetupMode.Install, CertificateName, progress, actionCancellationTokenSource.Token, update);
            
            //IsRunning = false;
            //cancelTransfer = null;
            //IsCurrentlyInstalled = result;
            //ProductStatus = string.Format(installedmessage, IsCurrentlyInstalled ? "" : "NOT ");
            
            //if (result) CurrentActionName = "Success";
        }
    }
}
