using MOROPO.FCBC.SGI.API.Common.Models;
using MOROPO.FCBC.SGI.API.Common.Models.Filter;

namespace MOROPO.FCBC.SGI.API.Connect.Clients
{
    public class ExposureConsultApiConnectClient : SAPRESSC.Core.Api.Connect.Clients.AbstractEntityConsultFilteredApiConnectClient<Persistence.Entities.Exposure, System.Guid, ExposureDisplayApiModel, ExposureFilterApiModel>
    {
        public System.Globalization.CultureInfo Culture { get; set; }

        public ExposureConsultApiConnectClient(SAPRESSC.Core.Api.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => string.Format("{0}/api/exposures", Culture?.TwoLetterISOLanguageName ?? System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
    }
}