using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class AdministrativeRolesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<AdministrativeRole, System.Guid, AdministrativeRoleInputViewModel, AdministrativeRoleDisplayViewModel>
    {
        public AdministrativeRolesController(IEntityRepository<AdministrativeRole, System.Guid> repository, IStringLocalizer<AdministrativeRolesController> localizer, ILogger<AdministrativeRolesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.AdministrativeRolesViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.AdministrativeRolesCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.AdministrativeRolesCreator)]
        public override IActionResult Create(AdministrativeRoleInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.AdministrativeRolesEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.AdministrativeRolesEditor)]
        public override IActionResult Edit(AdministrativeRoleInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.AdministrativeRolesRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.AdministrativeRolesRemover)]
        public override IActionResult Delete(AdministrativeRoleDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
