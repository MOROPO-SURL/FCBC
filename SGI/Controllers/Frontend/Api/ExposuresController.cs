using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.API.Common.Models;
using MOROPO.FCBC.SGI.API.Common.Models.Filter;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Common.Models;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;

namespace MOROPO.FCBC.SGI.Controllers.Frontend.Api
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Swashbuckle.AspNetCore.Annotations.SwaggerTag("Exposure's endpoints.")]
    public class ExposuresController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiConsultFilteredController<Exposure, System.Guid, ExposureDisplayApiModel, ExposureFilterApiModel>
    {
        public ExposuresController(IEntityRepository<Exposure, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ExposuresController> localizer, ILogger<ExposuresController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exposure's list", "Endpoint to obtain the full list of Exposures.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel[]))]
        public override ActionResult<ExposureDisplayApiModel[]> Get()
        {
            return base.Get();
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get a Exposure", "Endpoint to obtain a Exposures by it's id.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel))]
        public override ActionResult<ExposureDisplayApiModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exposure's pagination", "Endpoint to obtain the paginated list of Exposures.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<ExposureDisplayApiModel>))]
        public override ActionResult<PaginationModel<ExposureDisplayApiModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exposure's pagination", "Endpoint to obtain the paginated list of Exposures.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel[]))]
        public override ActionResult<ExposureDisplayApiModel[]> GetFiltered([FromBody] ExposureFilterApiModel filterModel)
        {
            return base.GetFiltered(filterModel);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exposure's pagination", "Endpoint to obtain the paginated list of Exposures.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<ExposureDisplayApiModel>))]
        public override ActionResult<PaginationModel<ExposureDisplayApiModel>> GetPaginateFiltered([FromBody] ExposureFilterApiModel filterModel, int sheet = 1, int limit = 25)
        {
            return base.GetPaginateFiltered(filterModel, sheet, limit);
        }
    }
}
