using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Collections.Generic;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Route("awards")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/awards")]
    [Area("complements")]
    public class AwardsController : AbstractEntityManagementController<Award, Guid, AwardInputViewModel, AwardDisplayViewModel>
    {
        IEntityRepository<Country, Guid> _repositoryCountry;
        public AwardsController(IEntityRepository<Award, Guid> repository, IStringLocalizer<AwardsController> localizer, Microsoft.Extensions.Logging.ILogger<SmartSolucionesCuba.SAPRESSC.Core.Web.Frontend.Controllers.AbstractEntityManagementController<Award, Guid, AwardInputViewModel, AwardDisplayViewModel>> logger, IEntityRepository<Country, Guid> repositoryCountry) : base(repository, localizer, logger)
        {
            _repositoryCountry = repositoryCountry;
        }

        [RequiresRoleBasePermission(Roles.AwardViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.AwardCreator)]
        public override IActionResult Create()
        {
            ViewBag.Countries = _repositoryCountry.GetAll();
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.AwardCreator)]
        public override IActionResult Create(AwardInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.AwardEditor)]
        public override IActionResult Edit(Guid key)
        {                       
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.AwardEditor)]
        public override IActionResult Edit(AwardInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.AwardRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.AwardRemover)]
        public override IActionResult Delete(AwardDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
