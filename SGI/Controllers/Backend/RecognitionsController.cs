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
    public class RecognitionsController : AbstractEntityManagementController<Recognition, Guid, RecognitionInputViewModel, RecognitionDisplayViewModel>
    {
        public RecognitionsController(IEntityRepository<Recognition, Guid> repository, IStringLocalizer<RecognitionsController> localizer, ILogger<RecognitionsController> logger) : base(repository, localizer, logger)
        {
        }
        [RequirePermissions(Permission.RecognitionViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.RecognitionCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.RecognitionCreator)]
        public override IActionResult Create(RecognitionInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.RecognitionEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.RecognitionEditor)]
        public override IActionResult Edit(RecognitionInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.RecognitionRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.RecognitionRemover)]
        public override IActionResult Delete(RecognitionDisplayViewModel displayViewModel)
        {
            return base.Delete(displayViewModel);
        }
    }
}
