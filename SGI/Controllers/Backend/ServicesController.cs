using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class ServicesController : AbstractEntityManagementController<Service, Guid, ServiceInputViewModel, ServiceDisplayViewModel>
    {
        public ServicesController(IEntityRepository<Service, Guid> repository, IStringLocalizer<ServicesController> localizer, ILogger<ServicesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.ServiceViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ServiceCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ServiceCreator)]
        public override IActionResult Create(ServiceInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ServiceEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ServiceEditor)]
        public override IActionResult Edit(ServiceInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ServiceRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ServiceRemover)]
        public override IActionResult Delete(ServiceDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
