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
    [Route("distinctions")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/distinctions")]
    [Area("complements")]
    public class DistinctionsController : AbstractEntityManagementController<Distinction, Guid, DistinctionInputViewModel, DistinctionDisplayViewModel>
    {
        public DistinctionsController(IEntityRepository<Distinction, Guid> repository, IStringLocalizer<Distinction> localizer, ILogger<SmartSolucionesCuba.SAPRESSC.Core.Web.Frontend.Controllers.AbstractEntityManagementController<Distinction, Guid, DistinctionInputViewModel, DistinctionDisplayViewModel>> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.DistinctionViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.DistinctionCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.DistinctionCreator)]
        public override IActionResult Create(DistinctionInputViewModel inputViewModel)
        {
           return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.DistinctionEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.DistinctionEditor)]
        public override IActionResult Edit(DistinctionInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.DistinctionRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.DistinctionRemover)]
        public override IActionResult Delete(DistinctionDisplayViewModel displayViewModel)
        {
           return base.Delete(displayViewModel);
        }
    }
}
