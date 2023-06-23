using System.Threading;
using System.Threading.Tasks;

namespace MOROPO.FCBC.Galleries.UWP.Installer.Extensions
{
    internal static class TaskExtensions
    {
        internal static async Task<T> WaitOrCancel<T>(this Task<T> task, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            
            await Task.WhenAny(task, token.WhenCanceled());
            
            token.ThrowIfCancellationRequested();

            return await task;
        }

        internal static Task WhenCanceled(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();

            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            
            return tcs.Task;
        }
    }
}
