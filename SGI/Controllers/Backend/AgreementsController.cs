using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class AgreementsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Agreement, System.Guid, AgreementInputViewModel, AgreementDisplayViewModel>
    {
        public AgreementsController(IEntityRepository<Agreement, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<AgreementsController> localizer, Microsoft.Extensions.Logging.ILogger<AgreementsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.AgreementViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.AgreementCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.AgreementCreator)]
        public override IActionResult Create(AgreementInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.AgreementEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.AgreementEditor)]
        public override IActionResult Edit(AgreementInputViewModel inputViewModel)
        {
            return base.Edit(inputViewModel);
        }

        [RequirePermissions(Permission.AgreementRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.AgreementRemover)]
        public override IActionResult Delete(AgreementDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}