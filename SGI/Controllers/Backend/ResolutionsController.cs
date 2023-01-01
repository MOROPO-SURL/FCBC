using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Frontend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
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

        [RequirePermissions(Permission.ResolutionViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ResolutionCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ResolutionCreator)]
        public override IActionResult Create(ResolutionInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ResolutionEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ResolutionEditor)]
        public override IActionResult Edit(ResolutionInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.ResolutionRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ResolutionRemover)]
        public override IActionResult Delete(ResolutionDisplayViewModel displayViewModel)
        {
            return base.Delete(displayViewModel);
        }
    }
}
