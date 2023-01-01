using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class AwardsController : AbstractEntityManagementController<Award, Guid, AwardInputViewModel, AwardDisplayViewModel>
    {
        private readonly IEntityRepository<Country, Guid> countryRepository;
        
        public AwardsController(IEntityRepository<Award, Guid> repository, IStringLocalizer<AwardsController> localizer, Microsoft.Extensions.Logging.ILogger<AwardsController> logger, IEntityRepository<Country, Guid> countryRepository) : base(repository, localizer, logger)
        {
            this.countryRepository = countryRepository;
            
        }

        [RequirePermissions(Permission.AwardViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.AwardCreator)]
        public override IActionResult Create()
        {       
            return base.Create();
        }

        [RequirePermissions(Permission.AwardCreator)]
        public override IActionResult Create(AwardInputViewModel inputViewModel)
        {     
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.AwardEditor)]
        public override IActionResult Edit(Guid key)
        {                       
            return base.Edit(key);
        }

        [RequirePermissions(Permission.AwardEditor)]
        public override IActionResult Edit(AwardInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.AwardRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.AwardRemover)]
        public override IActionResult Delete(AwardDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateWithCountries(AwardInputViewModel modelInput)
        {
            modelInput.Countries = countryRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        protected override void ConfigureCreate(AwardInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(AwardInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigureEdit(Award entity, AwardInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(AwardInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void PreCreate(Award entity, AwardInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.CountryId = modelInput.CountryId.Value;
        }

        protected override void PreEdit(Award entity, AwardInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.Country.Id != modelInput.CountryId)
            {
                entity.CountryId = modelInput.CountryId.Value;
            }
        }

        protected override void ValidateCreate(AwardInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            if (modelInput.CountryId == null)
            {
                ModelState.AddModelError("CountryId", "CountrySelectionRequired");
            }
        }

        protected override void ValidateEdit(AwardInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);

            if (modelInput.CountryId == null)
            {
                ModelState.AddModelError("CountryId", "CountrySelectionRequired");
            }
        }
    }
}
