using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Frontend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("complements")]
    public class ResolutionsController : AbstractEntityManagementController<Resolution, Guid, ResolutionInputViewModel, ResolutionDisplayViewModel>
    {

        public ResolutionsController(IEntityRepository<Resolution, Guid> repository, IStringLocalizer<ResolutionsController> localizer, ILogger<ResolutionsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.ResolutionViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ResolutionCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ResolutionCreator)]
        public override IActionResult Create(ResolutionInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ResolutionEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ResolutionEditor)]
        public override IActionResult Edit(ResolutionInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ResolutionRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ResolutionRemover)]
        public override IActionResult Delete(ResolutionDisplayViewModel displayViewModel)
        {
            return base.Delete(displayViewModel);
        }
    }
}
