using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("complements")]
    public class AdministrativeRolesController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<AdministrativeRole, System.Guid, AdministrativeRoleInputViewModel, AdministrativeRoleDisplayViewModel>
    {
        public AdministrativeRolesController(IEntityRepository<AdministrativeRole, System.Guid> repository, IStringLocalizer<AdministrativeRolesController> localizer, ILogger<AdministrativeRolesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.AdministrativeRolesViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.AdministrativeRolesCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.AdministrativeRolesCreator)]
        public override IActionResult Create(AdministrativeRoleInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.AdministrativeRolesEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.AdministrativeRolesEditor)]
        public override IActionResult Edit(AdministrativeRoleInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.AdministrativeRolesRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.AdministrativeRolesRemover)]
        public override IActionResult Delete(AdministrativeRoleDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
