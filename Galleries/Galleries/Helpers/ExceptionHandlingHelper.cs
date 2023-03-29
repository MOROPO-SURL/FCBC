using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace MOROPO.FCBC.Galleries.Helpers
{
    internal sealed class ExceptionHandlingHelper
    {
        private const string SUPPORT_ORIENTATION_MESSAGE = "Por favor contacte con el servicio de asistencia técnica para notificar esta situación.";

        private static ExceptionHandlingHelper instance;

        public static ExceptionHandlingHelper Instance
        {
            get { return instance ?? (instance = new ExceptionHandlingHelper()); }
        }

        private ExceptionHandlingHelper()
        {
        }

        public void DefaultHandle(System.Exception exception, System.Action continuationCallback = null)
        {
            if (exception == null)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Aviso", string.Format("El sistema ha tardado demasiado tiempo en responder.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine), "Aceptar").ContinueWith(_ => { continuationCallback?.Invoke(); });

                return;
            }

            var title = "Aviso";

            System.Exception targetException = exception;

            while (targetException is System.AggregateException)
            {
                targetException = targetException.InnerException;
            }

            var message = "Problemas no esperados en el sistema.";

            if (targetException is System.Net.Http.HttpRequestException)
            {
                message = "No se puede acceder al servidor. Revise su conexión de red y/o la disponibilidad del servicio.";
            }
            else if (targetException is SAPRESSC.Core.Api.Connect.UnspectedHttpStatusException)
            {
                var unspectedHttpStatus = (targetException as SAPRESSC.Core.Api.Connect.UnspectedHttpStatusException).HttpStatusCode;

                if (unspectedHttpStatus == System.Net.HttpStatusCode.BadRequest)
                {
                    message = "Datos del envío incorrectos.";
                }
                else if (unspectedHttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    message = "La aplicación ha caducado y requiere ser actualizada.";

                    continuationCallback = () =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            Xamarin.Forms.Application.Current.MainPage.NavigationPushModalPageAsync(new Pages.RequiredUpdatePage());
                        });
                    };
                }
                else if (unspectedHttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    title = "No Autorizado";
                    message = "Usted no cuenta con los permisos necesarios para realizar esta operación.";
                }
                else if (unspectedHttpStatus == System.Net.HttpStatusCode.Conflict)
                {
                    message = string.Format("Esta operación no puede realizarse pues compromete la integridad del sistema.{0}{0}Compruebe que el elemento a modificar o eliminar no sea necesario en otras secciones.", System.Environment.NewLine);
                }
            }

            Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, "Aceptar").ContinueWith(task => { continuationCallback?.Invoke(); });
        }
    }
}
