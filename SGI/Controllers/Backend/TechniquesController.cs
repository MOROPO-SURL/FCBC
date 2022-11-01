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
    public class TechniquesController : AbstractEntityManagementController<Technique, Guid, TechniqueInputViewModel, TechniqueDisplayViewModel>
    {
        public TechniquesController(IEntityRepository<Technique, Guid> repository, IStringLocalizer<TechniquesController> localizer, ILogger<TechniquesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.TechniqueViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.TechniqueCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.TechniqueCreator)]
        public override IActionResult Create(TechniqueInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.TechniqueEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.TechniqueEditor)]
        public override IActionResult Edit(TechniqueInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.TechniqueRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.TechniqueRemover)]
        public override IActionResult Delete(TechniqueDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
