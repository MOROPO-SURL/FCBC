using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class ExposuresController : SAPRESSC.Core.Web.Backend.Controllers.AbstractHabilitableEntityManagementController<Exposure, System.Guid, ExposureInputViewModel, ExposureDisplayViewModel>
    {
        public ExposuresController(IEntityRepository<Exposure, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ExposuresController> localizer, ILogger<ExposuresController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureCreator)]
        public override IActionResult Create(ExposureInputViewModel model)
        {
            return base.Create(model);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public override IActionResult Edit(ExposureInputViewModel model)
        {
            return base.Edit(model);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public override IActionResult Enable(System.Guid key)
        {
            return base.Enable(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureEditor)]
        public override IActionResult Disable(System.Guid key)
        {
            return base.Disable(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ExposureRemover)]
        public override IActionResult Delete(ExposureDisplayViewModel model)
        {
            return base.Delete(model);
        }

        protected override void PreCreate(Exposure entity, ExposureInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.GalleryId = Persistence.Enumerations.GalleryId.Amalia;
        }
    }
}
