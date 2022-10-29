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
    [Area("complements")]
    public class RecognitionsController : AbstractEntityManagementController<Recognition, Guid, RecognitionInputViewModel, RecognitionDisplayViewModel>
    {
        public RecognitionsController(IEntityRepository<Recognition, Guid> repository, IStringLocalizer<RecognitionsController> localizer, ILogger<RecognitionsController> logger) : base(repository, localizer, logger)
        {
        }
        [RequiresRoleBasePermission(Roles.RecognitionViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.RecognitionCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.RecognitionCreator)]
        public override IActionResult Create(RecognitionInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.RecognitionEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.RecognitionEditor)]
        public override IActionResult Edit(RecognitionInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.RecognitionRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.RecognitionRemover)]
        public override IActionResult Delete(RecognitionDisplayViewModel displayViewModel)
        {
            return base.Delete(displayViewModel);
        }
    }
}
