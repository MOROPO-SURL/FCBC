using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.EF.Repositories;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class ContractorsController : AbstractEntityManagementController<Contractor, Guid, ContractorInputViewModel, ContractorDisplayViewModel>
    {
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;
        private readonly IEntityRepository<ContractorKind, int> contractKindRepository;
        public ContractorsController(IEntityRepository<Contractor, Guid> repository, IStringLocalizer<ContractorsController> localizer, ILogger<ContractorsController> logger, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IEntityRepository<ContractorKind, int> contractKindRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.contractKindRepository = contractKindRepository;
        }

        [RequiresRoleBasePermission(Roles.ContractorViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ContractorCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ContractorCreator)]
        public override IActionResult Create(ContractorInputViewModel inputViewModel)
        {
           return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        public override IActionResult Edit(ContractorInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ContractorRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ContractorRemover)]
        public override IActionResult Delete(ContractorDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateWithOrganizationIdentityAndContractkind(ContractorInputViewModel modelInput)
        {            
            modelInput.OrgaizacionIdentities = organizationIdentityRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();
            modelInput.ContractKinds = contractKindRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();
        }

        protected override void ConfigureCreate(ContractorInputViewModel modelInput)
        {
            PopulateWithOrganizationIdentityAndContractkind(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ContractorInputViewModel modelInput)
        {
            PopulateWithOrganizationIdentityAndContractkind(modelInput);
        }

        protected override void ConfigureEdit(Contractor entity, ContractorInputViewModel modelInput)
        {
            PopulateWithOrganizationIdentityAndContractkind(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ContractorInputViewModel modelInput)
        {
            PopulateWithOrganizationIdentityAndContractkind(modelInput);
        }

        protected override void PreCreate(Contractor entity, ContractorInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.OrganizationIdentityId = modelInput.OrganizationIdentityId.Value;
            entity.ContractorKindId = modelInput.ContractKindId.Value;
        }

        protected override void PreEdit(Contractor entity, ContractorInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.OrganizationIdentity.Id != modelInput.OrganizationIdentityId)
            {
                entity.OrganizationIdentityId = modelInput.OrganizationIdentityId.Value;
            }
            if (entity.ContractorKind.Id != modelInput.ContractKindId)
            {
                entity.ContractorKind.Id = modelInput.ContractKindId.Value;
            }
        }

        protected override void ValidateCreate(ContractorInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);

            if (modelInput.OrganizationIdentityId == null)
            {
                ModelState.AddModelError("OrganizationIdentityId", "OrganizationSelectionRequired");
            }
            if (modelInput.ContractKindId == null)
            {
                ModelState.AddModelError("ContractKindId", "ContractKindSelectionRequired");
            }
        }

        protected override void ValidateEdit(ContractorInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);

            if (modelInput.OrganizationIdentityId == null)
            {
                ModelState.AddModelError("OrganizationIdentityId", "OrganizationSelectionRequired");
            }
            if (modelInput.ContractKindId == null)
            {
                ModelState.AddModelError("ContractKindId", "ContractKindSelectionRequired");
            }
        }
    }
}
