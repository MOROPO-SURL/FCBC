using MOROPO.FCBC.Galleries.UWP.Installer.Extensions;
using System;
using System.Linq;

namespace MOROPO.FCBC.Galleries.UWP.Installer.Helpers
{
    internal class AppxBundleSetupHelper
    {
        internal static async System.Threading.Tasks.Task<Windows.Management.Deployment.DeploymentResult> SetupAsync(Enumerations.SetupMode mode, IProgress<Models.View.ProgressDetailViewModel> progressCallback, System.Threading.CancellationToken cancellationToken = default)
        {
            var appxUri = new Uri(GetResourcePath("AmaliaGallery.appxbundle"));

            var dependencieUris = GetDependencies();

            var packageManager = new Windows.Management.Deployment.PackageManager();

            var deploymentProgressCallback = new Progress<Windows.Management.Deployment.DeploymentProgress>(progress =>
            {
                progressCallback.Report(new Models.View.ProgressDetailViewModel
                {
                    Percentage = progress.percentage,
                    Message = "..."
                });
            });

            // Cancellation is likely to be impossible for user to trigger for Certificate installation.  Consider adding in slight delay?
            if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException();

            // MP! fixme: Hangs on next call.  Something to do with progressCallback, without it the call completes.

            if (mode == Enumerations.SetupMode.Update)
            {
                return await packageManager.UpdatePackageAsync(appxUri, dependencieUris, Windows.Management.Deployment.DeploymentOptions.ForceTargetApplicationShutdown).AsTask(deploymentProgressCallback).WaitOrCancel(cancellationToken);
            }
            else
            {
                return await packageManager.AddPackageAsync(appxUri, dependencieUris, Windows.Management.Deployment.DeploymentOptions.ForceTargetApplicationShutdown).AsTask(deploymentProgressCallback).WaitOrCancel(cancellationToken);
            }
        }

        private static string GetResourcePath(string fileName)
        {
            return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Resources", fileName);
        }

        private static string GetFullResourceName(string shortResourceName)
        {
            return $"{System.Reflection.Assembly.GetEntryAssembly().GetName().Name}.Certificate.{shortResourceName}";
        }

        private static string GetArchitecture()
        {
            string arch = Environment.GetEnvironmentVariable("Processor_Architecture");

            if (string.Equals(arch, "x86"))
            {
                if (!(Environment.GetEnvironmentVariable("ProgramFiles(Arm)") is null))
                {
                    arch = "arm64";
                }
                else if (!(Environment.GetEnvironmentVariable("ProgramFiles(x86)") is null))
                {
                    arch = "amd64";
                }
            }
            return arch.ToLowerInvariant();
        }

        /// <summary>
        /// Based on the "Add-AppDevPackage.ps1" that generates when you publish an app with target sideloading.
        /// </summary>
        private static System.Collections.Generic.List<Uri> GetDependencies()
        {
            var dependencies = new System.Collections.Generic.List<Uri>();

            var arch = GetArchitecture();
            
            var dependenciesPath = GetResourcePath("Dependencies");

            if (string.Equals(arch, "x86") || string.Equals(arch, "amd64") || string.Equals(arch, "arm64"))
            {
                dependencies.AddRange(GetDependencies("x86", dependenciesPath));
            }

            if (string.Equals(arch, "amd64"))
            {
                dependencies.AddRange(GetDependencies("x64", dependenciesPath));
            }

            if (string.Equals(arch, "arm") || string.Equals(arch, "arm64"))
            {
                dependencies.AddRange(GetDependencies("arm", dependenciesPath));
            }

            if (string.Equals(arch, "arm64"))
            {
                dependencies.AddRange(GetDependencies("arm64", dependenciesPath));
            }

            return dependencies;
        }

        private static System.Collections.Generic.List<Uri> GetDependencies(string arch, string dependenciesPath)
        {
            var archDepPath = System.IO.Path.Combine(dependenciesPath, arch);

            var dependencies = new System.Collections.Generic.List<Uri>();

            if (System.IO.Directory.Exists(archDepPath))
            {
                // Dependencies can have the .appx and .msix extension:
                dependencies.AddRange(System.IO.Directory.GetFiles(archDepPath, @"*.appx", System.IO.SearchOption.AllDirectories).Select(x => new Uri(x)));
                dependencies.AddRange(System.IO.Directory.GetFiles(archDepPath, @"*.msix", System.IO.SearchOption.AllDirectories).Select(x => new Uri(x)));
            }           
            
            return dependencies;
        }
    }
}