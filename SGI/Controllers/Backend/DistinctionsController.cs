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
    [Area("complements")]
    public class DistinctionsController : AbstractEntityManagementController<Distinction, Guid, DistinctionInputViewModel, DistinctionDisplayViewModel>
    {
        public DistinctionsController(IEntityRepository<Distinction, Guid> repository, IStringLocalizer<DistinctionsController> localizer, ILogger<DistinctionsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.DistinctionViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.DistinctionCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.DistinctionCreator)]
        public override IActionResult Create(DistinctionInputViewModel inputViewModel)
        {
           return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.DistinctionEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.DistinctionEditor)]
        public override IActionResult Edit(DistinctionInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.DistinctionRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.DistinctionRemover)]
        public override IActionResult Delete(DistinctionDisplayViewModel displayViewModel)
        {
           return base.Delete(displayViewModel);
        }
    }
}
