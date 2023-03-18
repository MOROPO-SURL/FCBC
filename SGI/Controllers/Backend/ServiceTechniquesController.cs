using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class ServiceTechniquesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ServiceTechnique, System.Guid, ServiceTechniqueInputViewModel, ServiceTechniqueDisplayViewModel>
    {
        public ServiceTechniquesController(IEntityRepository<ServiceTechnique, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ServiceTechniquesController> localizer, ILogger<ServiceTechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ServiceTechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ServiceTechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ServiceTechniqueCreator)]
        public override IActionResult Create(ServiceTechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ServiceTechniqueEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ServiceTechniqueEditor)]
        public override IActionResult Edit(ServiceTechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ServiceTechniqueRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ServiceTechniqueRemover)]
        public override IActionResult Delete(ServiceTechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
