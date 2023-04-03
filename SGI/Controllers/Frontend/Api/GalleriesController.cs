using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.API.Common.Models;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.SAPRESSC.Core.Common.Models;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;

namespace MOROPO.FCBC.SGI.Controllers.Frontend.Api
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Swashbuckle.AspNetCore.Annotations.SwaggerTag("Gallery's endpoints.")]
    public class GalleriesController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiConsultController<Persistence.Entities.Gallery, GalleryId, GalleryDisplayApiModel>
    {
        public GalleriesController(IEntityRepository<Persistence.Entities.Gallery, GalleryId> repository, Microsoft.Extensions.Localization.IStringLocalizer<GalleriesController> localizer, ILogger<GalleriesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Gallery's list", "Endpoint to obtain the full list of Galleries.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(GalleryDisplayApiModel[]))]
        public override ActionResult<GalleryDisplayApiModel[]> Get()
        {
            return base.Get();
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get a Gallery", "Endpoint to obtain a Galleries by it's id.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(GalleryDisplayApiModel))]
        public override ActionResult<GalleryDisplayApiModel> Get(GalleryId key)
        {
            return base.Get(key);
        }

        [RequirePermissions(Security.Authorization.Permission.GalleryViewer)]
        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Galllery's pagination", "Endpoint to obtain the paginated list of Galleries.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<GalleryDisplayApiModel>))]
        public override ActionResult<PaginationModel<GalleryDisplayApiModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }

        protected override GalleryDisplayApiModel EntityToDisplayModel(Persistence.Entities.Gallery entity)
        {
            var model = base.EntityToDisplayModel(entity);

            model.ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension();

            return model;
        }
    }
}
