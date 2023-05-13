using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.API.Common.Models;
using MOROPO.FCBC.SGI.API.Common.Models.Filter;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Common.Models;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.Linq;
using System.Linq.Expressions;

namespace MOROPO.FCBC.SGI.Controllers.Frontend.Api
{
    // TODO Fix ApiKey Authorization claims from permission to role
    //[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Swashbuckle.AspNetCore.Annotations.SwaggerTag("Exposure's endpoints.")]
    public class ExposuresController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiConsultFilteredController<Exposure, System.Guid, ExposureDisplayApiModel, ExposureFilterApiModel>
    {
        public ExposuresController(IEntityRepository<Exposure, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ExposuresController> localizer, ILogger<ExposuresController> logger) : base(repository, localizer, logger)
        {
        }

        protected override Expression<System.Func<Exposure, bool>> GetGlobalSelector()
        {
            return entity => entity.Enabled;
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exposure's list", "Endpoint to obtain the full list of Exposures.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel[]))]
        public override ActionResult<ExposureDisplayApiModel[]> Get()
        {
            return base.Get();
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get a Exposure", "Endpoint to obtain a Exposures by it's id.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel))]
        public override ActionResult<ExposureDisplayApiModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exposure's pagination", "Endpoint to obtain the paginated list of Exposures.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<ExposureDisplayApiModel>))]
        public override ActionResult<PaginationModel<ExposureDisplayApiModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exposure's pagination", "Endpoint to obtain the paginated list of Exposures.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel[]))]
        public override ActionResult<ExposureDisplayApiModel[]> GetFiltered([FromBody] ExposureFilterApiModel filterModel)
        {
            return base.GetFiltered(filterModel);
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exposure's pagination", "Endpoint to obtain the paginated list of Exposures.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<ExposureDisplayApiModel>))]
        public override ActionResult<PaginationModel<ExposureDisplayApiModel>> GetPaginateFiltered([FromBody] ExposureFilterApiModel filterModel, int sheet = 1, int limit = 25)
        {
            return base.GetPaginateFiltered(filterModel, sheet, limit);
        }

        protected override ExposureDisplayApiModel EntityToDisplayModel(Exposure entity)
        {
            var model = base.EntityToDisplayModel(entity);

            model.Import(entity);

            model.Curators = entity.EmployeeCuratorshipBridges.Select(bridge =>
            {
                var _model = new EmployeeDisplayApiModel();

                _model.Import(bridge.Employee);

                _model.ImageExtension = bridge.Employee.Identity.Image?.ContentType.ContentTypeToExtension();

                return _model;
            }).ToArray();

            return model;
        }
    }
}
