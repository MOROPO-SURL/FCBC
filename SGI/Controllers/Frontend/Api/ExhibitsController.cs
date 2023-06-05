using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.API.Common.Models;
using MOROPO.FCBC.SGI.API.Common.Models.Filter;
using MOROPO.SAPRESSC.Core.Common.Models;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Frontend.Api
{
    // TODO Fix ApiKey Authorization claims from permission to role
    //[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Swashbuckle.AspNetCore.Annotations.SwaggerTag("Exhibit's endpoints.")]
    public class ExhibitsController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiConsultFilteredController<Persistence.Entities.Exhibit, System.Guid, API.Common.Models.ExhibitDisplayApiModel, API.Common.Models.Filter.ExhibitFilterApiModel>
    {
        public ExhibitsController(IEntityRepository<Persistence.Entities.Exhibit, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ExhibitsController> localizer, ILogger<ExhibitsController> logger) : base(repository, localizer, logger)
        {
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exhibit's list", "Endpoint to obtain the full list of Exhibities.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel[]))]
        public override ActionResult<ExhibitDisplayApiModel[]> Get()
        {
            return base.Get();
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get a Exhibit", "Endpoint to obtain a Exhibities by it's id.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel))]
        public override ActionResult<ExhibitDisplayApiModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exhibit's pagination", "Endpoint to obtain the paginated list of Exhibities.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel[]))]
        public override ActionResult<ExhibitDisplayApiModel[]> GetFiltered([FromBody] ExhibitFilterApiModel filterModel)
        {
            return base.GetFiltered(filterModel);
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exhibit's pagination", "Endpoint to obtain the paginated list of Exhibities.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<ExposureDisplayApiModel>))]
        public override ActionResult<PaginationModel<ExhibitDisplayApiModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exhibit's pagination", "Endpoint to obtain the paginated list of Exhibities.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<ExposureDisplayApiModel>))]
        public override ActionResult<PaginationModel<ExhibitDisplayApiModel>> GetPaginateFiltered([FromBody] ExhibitFilterApiModel filterModel, int sheet = 1, int limit = 25)
        {
            return base.GetPaginateFiltered(filterModel, sheet, limit);
        }

        protected override ExhibitDisplayApiModel EntityToDisplayModel(Persistence.Entities.Exhibit entity)
        {
            var model = base.EntityToDisplayModel(entity);

            model.Import(entity);

            model.ArtWorks = entity.ArtWorkBridges.Select(bridge =>
            {
                var _model = new ArtWorkDisplayApiModel();

                _model.Import(bridge.ArtWork);
                
                _model.FeatureImage = new ImageDisplayApiModel
                {
                    Id = bridge.ArtWork.FeatureImageId.Value,
                    Extension = bridge.ArtWork.FeatureImage.ContentType.ContentTypeToExtension(),
                    Width = bridge.ArtWork.FeatureImage.Width,
                    Height = bridge.ArtWork.FeatureImage.Height
                };
                _model.ArtTechniques = bridge.ArtWork.TechniqueBridges.Select(bridg3 => new ArtTechniqueDisplayApiModel
                {
                    Id = bridg3.TechniqueId,
                    Name = bridg3.Technique.Name,
                    Description = bridg3.Technique.Description
                }).ToArray();
                _model.Creators = bridge.ArtWork.CreatorBridges.Select(bridg3 => new CreatorDisplayApiModel
                {
                    Id = bridg3.CreatorId,
                    Name = bridg3.Creator.Identity.Name,
                    LastName = bridg3.Creator.Identity.LastName,
                    Sex = bridg3.Creator.Identity.Sex,
                    ImageId = bridg3.Creator.Identity.ImageId,
                    ImageExtension = bridg3.Creator.Identity.Image?.ContentType.ContentTypeToExtension()
                }).ToArray();

                return _model;
            }).ToArray();

            return model;
        }
    }
}
