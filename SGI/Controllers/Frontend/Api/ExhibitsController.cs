using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.API.Common.Models;
using MOROPO.FCBC.SGI.API.Common.Models.Filter;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.SAPRESSC.Core.Common.Models;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Frontend.Api
{
    // TODO Fix ApiKey Authorization claims from permission to role
    //[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/api/[controller]")]
    [ApiController]
    [Swashbuckle.AspNetCore.Annotations.SwaggerTag("Exhibit's endpoints.")]
    public class ExhibitsController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiConsultFilteredController<Persistence.Entities.Exhibit, System.Guid, API.Common.Models.ExhibitDisplayApiModel, API.Common.Models.Filter.ExhibitFilterApiModel>
    {
        private readonly IEntityRepository<Persistence.Entities.ArtWork, System.Guid> artWorkRepository;

        public ExhibitsController(IEntityRepository<Persistence.Entities.ArtWork, System.Guid> artWorkRepository, IEntityRepository<Persistence.Entities.Exhibit, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ExhibitsController> localizer, ILogger<ExhibitsController> logger) : base(repository, localizer, logger)
        {
            this.artWorkRepository = artWorkRepository;
        }

        // TODO Fix ApiKey Authorization claims from permission to role
        //[RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Exhibit's list", "Endpoint to obtain the full list of Exhibities.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ExposureDisplayApiModel[]))]
        public override ActionResult<ExhibitDisplayApiModel[]> Get()
        {
            return Ok(Repository.GetAll().ToArray().Select(entity => EntityToDisplayModel(entity)).ToArray());
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
            return Ok(Repository.FindBy(filterModel.GetSelector()).ToArray().Select(entity => EntityToDisplayModel(entity)).ToArray());
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

            TranslatableLanguage? translatableLanguage = null;

            if (RouteData.Values.ContainsKey("culture"))
            {
                switch (RouteData.Values["culture"])
                {
                    case "en":
                        translatableLanguage = TranslatableLanguage.English;

                        break;

                    case "fr":
                        translatableLanguage = TranslatableLanguage.French;

                        break;

                    case "ru":
                        translatableLanguage = TranslatableLanguage.Russian;

                        break;

                    case "pt":
                        translatableLanguage = TranslatableLanguage.Portuguese;

                        break;
                }
            }

            model.ArtWorks = entity.ArtWorkBridges.Where(bridge => bridge.ArtWork.FeatureImage != null && bridge.ArtWork.DimensionsUnitMeasurement.MilimetersRatio != null && bridge.ArtWork.Width != null && bridge.ArtWork.Height != null).Select(bridge =>
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
                _model.Videos = bridge.ArtWork.Videos.Select(_entity => new VideoDisplayApiModel
                {
                    Id = _entity.Id,
                    Extension = _entity.ContentType.ContentTypeToExtension()
                }).ToArray();
                _model.Techniques = bridge.ArtWork.TechniqueBridges.Select(_bridge => new ArtWorkTechniqueDisplayApiModel
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
                _model.Awards = bridge.ArtWork.Awards.Select(_entity =>
                {
                    var __model = new ArtWorkAwardDisplayApiModel();

                    __model.Import(_entity);

                    return __model;
                }).ToArray();

                var artWork = artWorkRepository.Get(bridge.ArtWorkId);

                var translation = artWork.Translations.FirstOrDefault(ent1ty => ent1ty.TranslatableLanguage == translatableLanguage);

                if (translation != null)
                {
                    if (translation.Title != null) _model.Title = translation.Title;
                    if (translation.Collection != null) _model.Collection = translation.Collection;
                    if (translation.Location != null) _model.Location = translation.Location;
                    if (translation.About != null) _model.About = translation.About;
                }

                return _model;
            }).ToArray();

            model.CraftWorks = entity.CraftWorkBridges.Where(bridge => bridge.CraftWork.FeatureImage != null).Select(bridge =>
            {
                var _model = new CraftWorkDisplayApiModel();

                _model.Import(bridge.CraftWork);

                _model.FeatureImage = new ImageDisplayApiModel
                {
                    Id = bridge.CraftWork.FeatureImageId.Value,
                    Extension = bridge.CraftWork.FeatureImage.ContentType.ContentTypeToExtension(),
                    Width = bridge.CraftWork.FeatureImage.Width,
                    Height = bridge.CraftWork.FeatureImage.Height
                };
                _model.Images = bridge.CraftWork.ImageBridges.Select(_bridge => new ImageDisplayApiModel
                {
                    Id = _bridge.ImageId,
                    Extension = _bridge.Image.ContentType.ContentTypeToExtension(),
                    Width = _bridge.Image.Width,
                    Height = _bridge.Image.Height
                }).ToArray();
                _model.Videos = bridge.CraftWork.Videos.Select(_entity => new VideoDisplayApiModel
                {
                    Id = _entity.Id,
                    Extension = _entity.ContentType.ContentTypeToExtension()
                }).ToArray();
                _model.Techniques = bridge.CraftWork.TechniqueBridges.Select(_bridge => new CraftWorkTechniqueDisplayApiModel
                {
                    Id = _bridge.TechniqueId,
                    Name = _bridge.Technique.Name,
                    Description = _bridge.Technique.Description
                }).ToArray();
                _model.Creators = bridge.CraftWork.CreatorBridges.Select(_bridge => new CreatorDisplayApiModel
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
                _model.Materials = bridge.CraftWork.MaterialBridges.Select(_bridge => new MaterialDisplayApiModel
                {
                    Id = _bridge.MaterialId,
                    Name = _bridge.Material.Name,
                    Description = _bridge.Material.Description
                }).ToArray();
                _model.Themathics = bridge.CraftWork.ThematicBridges.Select(_bridge => new ThemathicDisplayApiModel
                {
                    Id = _bridge.ThematicId,
                    Name = _bridge.Thematic.Name,
                    Description = _bridge.Thematic.Description
                }).ToArray();
                //_model.Valorations = bridge.CraftWork.Valorations.Select(_entity =>
                //{
                //    var __model = new ValorationDisplayApiModel();

                //    __model.Import(_entity);

                //    __model.Critic.ImageExtension = _entity.Critic.Identity.Image.ContentType.ContentTypeToExtension();

                //    return __model;
                //}).ToArray();
                //_model.Awards = bridge.CraftWork.Awards.Select(_entity =>
                //{
                //    var __model = new ArtWorkAwardDisplayApiModel();

                //    __model.Import(_entity);

                //    return __model;
                //}).ToArray();

                var craftWork = artWorkRepository.Get(bridge.CraftWorkId);

                var translation = craftWork.Translations.FirstOrDefault(ent1ty => ent1ty.TranslatableLanguage == translatableLanguage);

                if (translation != null)
                {
                    if (translation.Title != null) _model.Title = translation.Title;
                    if (translation.About != null) _model.About = translation.About;
                }

                return _model;
            }).ToArray();

            return model;
        }
    }
}
