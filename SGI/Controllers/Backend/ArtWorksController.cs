using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SmartSolucionesCuba.SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("works")]
    public class ArtWorksController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ArtWork, System.Guid, ArtWorkInputViewModel, ArtWorkDisplayViewModel>
    {
        private readonly IEntityRepository<ArtisticManifestation, System.Guid> manifestationRepository;

        public ArtWorksController(IEntityRepository<ArtisticManifestation, System.Guid> manifestationRepository, IEntityRepository<ArtWork, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ArtWorksController> localizer, ILogger<ArtWorksController> logger) : base(repository, localizer, logger)
        {
            this.manifestationRepository = manifestationRepository;
        }

        [RequiresRoleBasePermission(Roles.ArtWorkViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ArtWorkCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ArtWorkCreator)]
        public override IActionResult Create(ArtWorkInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ArtWorkEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ArtWorkEditor)]
        public override IActionResult Edit(ArtWorkInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ArtWorkRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ArtWorkRemover)]
        public override IActionResult Delete(ArtWorkDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(ArtWorkInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
        }

        protected override void ConfigureEdit(ArtWork entity, ArtWorkInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ArtWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ArtWorkInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(modelInput);

            PopulateInputModelForAvailableManifestations(modelInput);
        }

        protected override void PreCreate(ArtWork entity, ArtWorkInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.Manifestation = manifestationRepository.Get(modelInput.ManifestationId.Value);
        }

        protected override void PreEdit(ArtWork entity, ArtWorkInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.ManifestationId == modelInput.ManifestationId) return;

            entity.Manifestation = manifestationRepository.Get(modelInput.ManifestationId.Value);
        }

        private void PopulateInputModelForAvailableManifestations(ArtWorkInputViewModel model)
        {
            model.AvaiableManifestations = manifestationRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = entity.Name,
                Value = entity.Id.ToString()
            }).ToList();
        }
    }
}
