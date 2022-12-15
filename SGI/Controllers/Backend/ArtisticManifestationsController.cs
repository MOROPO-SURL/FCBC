using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("complements")]
    public class ArtisticManifestationsController : AbstractEntityManagementController<ArtisticManifestation, Guid, ArtisticManifestationInputViewModel, ArtisticManifestationDisplayViewModel>
    {
        private IEntityRepository<ArtisticManifestationGroup, Guid> manifestationKindRepository;
        public ArtisticManifestationsController(IEntityRepository<ArtisticManifestationGroup, Guid> manifestationKindRepository, IEntityRepository<ArtisticManifestation, Guid> repository, IStringLocalizer<ArtisticManifestationsController> localizer, ILogger<ArtisticManifestationsController> logger) : base(repository, localizer, logger)
        {
            this.manifestationKindRepository = manifestationKindRepository;
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationCreator)]
        public override IActionResult Create(ArtisticManifestationInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationEditor)]
        public override IActionResult Edit(ArtisticManifestationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationRemover)]
        public override IActionResult Delete(Guid key) 
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationRemover)]
        public override IActionResult Delete(ArtisticManifestationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(ArtisticManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ArtisticManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigureEdit(ArtisticManifestation entity, ArtisticManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ArtisticManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        private void PopulateTree(ArtisticManifestationInputViewModel modelInput)
        {
            var manifestationKinds = manifestationKindRepository.GetAll().ToList();
            modelInput.SerializedTree = Newtonsoft.Json.JsonConvert.SerializeObject(GetTree(manifestationKinds, null));
        }

        private List<object> GetTree(List<ArtisticManifestationGroup> manifestationKinds, System.Guid? parentId)
        {
            var result = new List<object>();

            manifestationKinds = manifestationKindRepository.GetAll().Where(m => m.ParentId == parentId).ToList();
            if (!(manifestationKinds.Count() == 0))
            {
                foreach (var manifestationKind in manifestationKinds)
                {
                    result.Add(new
                    {
                        id = manifestationKind.Id,
                        text = manifestationKind.Name,
                        children = GetTree(manifestationKinds, manifestationKind.Id)
                    });
                }
            }
            return result;
        }
    }
}
