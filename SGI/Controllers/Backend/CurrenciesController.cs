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
    public class CurrenciesController : AbstractEntityManagementController<Currency, int, CurrencyInputViewModel, CurrencyDisplayViewModel>
    {
        public CurrenciesController(IEntityRepository<Currency, int> repository, IStringLocalizer<CurrenciesController> localizer, ILogger<CurrenciesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.CurrencyViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.CurrencyCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.CurrencyCreator)]
        public override IActionResult Create(CurrencyInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.CurrencyEditor)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.CurrencyEditor)]
        public override IActionResult Edit(CurrencyInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.CurrencyRemover)]
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.CurrencyRemover)]
        public override IActionResult Delete(CurrencyDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
