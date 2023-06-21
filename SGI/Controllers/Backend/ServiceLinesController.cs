using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("manifestations")]
    public class ServiceLinesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ServiceLine, Guid, ServiceLineInputViewModel, ServiceLineDisplayViewModel>
    {
        public ServiceLinesController(IEntityRepository<ServiceLine, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ServiceLinesController> localizer, ILogger<ServiceLinesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ServiceLineViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ServiceLineCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ServiceLineCreator)]
        public override IActionResult Create(ServiceLineInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ServiceLineEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ServiceLineEditor)]
        public override IActionResult Edit(ServiceLineInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ServiceLineRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ServiceLineRemover)]
        public override IActionResult Delete(ServiceLineDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
