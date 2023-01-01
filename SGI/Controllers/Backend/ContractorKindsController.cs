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
    public class ContractorKindsController : AbstractEntityManagementController<ContractorKind, int, ContractorKindInputViewModel, ContractorKindDisplayViewModel>
    {
        public ContractorKindsController(IEntityRepository<ContractorKind, int> repository, IStringLocalizer<ContractorKindsController> localizer, ILogger<ContractorKindsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.ContractorKindViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ContractorKindCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ContractorKindCreator)]
        public override IActionResult Create(ContractorKindInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ContractorKindEditor)]
        public override IActionResult Edit(int key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ContractorKindEditor)]
        public override IActionResult Edit(ContractorKindInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ContractorKindRemover)]
        public override IActionResult Delete(int key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ContractorKindRemover)]
        public override IActionResult Delete(ContractorKindDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
