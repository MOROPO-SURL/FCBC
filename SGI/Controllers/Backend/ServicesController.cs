using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class ServicesController : AbstractEntityManagementController<InstitutionalService, Guid, ServiceInputViewModel, ServiceDisplayViewModel>
    {
        public ServicesController(IEntityRepository<InstitutionalService, Guid> repository, IStringLocalizer<ServicesController> localizer, ILogger<ServicesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ServiceViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ServiceCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ServiceCreator)]
        public override IActionResult Create(ServiceInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ServiceEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ServiceEditor)]
        public override IActionResult Edit(ServiceInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ServiceRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ServiceRemover)]
        public override IActionResult Delete(ServiceDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
