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

            model.ArtWorks = entity.ArtWorkBridges.Where(bridge => bridge.ArtWork.FeatureImage != null).Select(bridge =>
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
                _model.Images = bridge.ArtWork.ImageBridges.Select(_bridge => new ImageDisplayApiModel
                {
                    Id = _bridge.ImageId,
                    Extension = _bridge.Image.ContentType.ContentTypeToExtension(),
                    Width = _bridge.Image.Width,
                    Height = _bridge.Image.Height
                }).ToArray();
                _model.ArtTechniques = bridge.ArtWork.TechniqueBridges.Select(_bridge => new ArtWorkTechniqueDisplayApiModel
                {
                    Id = _bridge.TechniqueId,
                    Name = _bridge.Technique.Name,
                    Description = _bridge.Technique.Description
                }).ToArray();
                _model.Creators = bridge.ArtWork.CreatorBridges.Select(_bridge => new CreatorDisplayApiModel
                {
                    Id = _bridge.CreatorId,
                    Name = _bridge.Creator.Identity.Name,
                    LastName = _bridge.Creator.Identity.LastName,
                    Sex = _bridge.Creator.Identity.Sex,
                    ImageId = _bridge.Creator.Identity.ImageId,
                    ImageExtension = _bridge.Creator.Identity.Image?.ContentType.ContentTypeToExtension(),
                    ArtWorkManifestations = _bridge.Creator.ArtisticManifestationBridges.Select(__bridge =>
                    {
                        var __model = new ArtWorkManifestationDisplayApiModel();

                        __model.Import(__bridge.Manifestation);

                        return __model;
                    }).ToArray(),
                    CraftWorkManifestations = _bridge.Creator.CraftManifestationBridges.Select(__bridge =>
                    {
                        var __model = new CraftWorkManifestationDisplayApiModel();

                        __model.Import(__bridge.Manifestation);

                        return __model;
                    }).ToArray(),
                    ServiceManifestations = _bridge.Creator.ServiceLineBridges.Select(__bridge =>
                    {
                        var __model = new ServiceManifestationDisplayApiModel();

                        __model.Import(__bridge.Line);

                        return __model;
                    }).ToArray()
                }).ToArray();
                _model.Materials = bridge.ArtWork.MaterialBridges.Select(_bridge => new MaterialDisplayApiModel
                {
                    Id = _bridge.MaterialId,
                    Name = _bridge.Material.Name,
                    Description = _bridge.Material.Description
                }).ToArray();
                _model.Themathics = bridge.ArtWork.ThematicBridges.Select(_bridge => new ThemathicDisplayApiModel
                {
                    Id = _bridge.ThematicId,
                    Name = _bridge.Thematic.Name,
                    Description = _bridge.Thematic.Description
                }).ToArray();
                _model.Valorations = bridge.ArtWork.Valorations.Select(_entity => 
                {
                    var __model = new ValorationDisplayApiModel();

                    __model.Import(_entity);

                    __model.Critic.ImageExtension = _entity.Critic.Identity.Image.ContentType.ContentTypeToExtension();

                    return __model;
                }).ToArray();
                _model.ArtWorkAwards = bridge.ArtWork.Awards.Select(_entity =>
                {
                    var __model = new ArtWorkAwardDisplayApiModel();

                    __model.Import(_entity);

                    return __model;
                }).ToArray();

                return _model;
            }).ToArray();

            return model;
        }
    }
}
