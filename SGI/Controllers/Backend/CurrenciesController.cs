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

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class CurrenciesController : AbstractEntityManagementController<Currency, int, CurrencyInputViewModel, CurrencyDisplayViewModel>
    {
        public CurrenciesController(IEntityRepository<Currency, int> repository, IStringLocalizer<CurrenciesController> localizer, ILogger<CurrenciesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.CurrencyViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.CurrencyCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.CurrencyCreator)]
        public override IActionResult Create(CurrencyInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.CurrencyEditor)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.CurrencyEditor)]
        public override IActionResult Edit(CurrencyInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.CurrencyRemover)]
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.CurrencyRemover)]
        public override IActionResult Delete(CurrencyDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
