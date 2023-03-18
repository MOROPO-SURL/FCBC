using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("manifestations")]
    public class ArtisticManifestationsController : AbstractEntityManagementController<ArtisticManifestation, Guid, ArtisticManifestationInputViewModel, ArtisticManifestationDisplayViewModel>
    {
        private readonly IEntityRepository<ArtisticManifestationGroup, Guid> artisticManifestationGroupRepository;

        public ArtisticManifestationsController(IEntityRepository<ArtisticManifestationGroup, Guid> artisticManifestationGroupRepository, IEntityRepository<ArtisticManifestation, Guid> repository, IStringLocalizer<ArtisticManifestationsController> localizer, ILogger<ArtisticManifestationsController> logger) : base(repository, localizer, logger)
        {
            this.artisticManifestationGroupRepository = artisticManifestationGroupRepository;
        }

        [RequirePermissions(Permission.ArtisticManifestationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ArtisticManifestationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ArtisticManifestationCreator)]
        public override IActionResult Create(ArtisticManifestationInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ArtisticManifestationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ArtisticManifestationEditor)]
        public override IActionResult Edit(ArtisticManifestationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ArtisticManifestationRemover)]
        public override IActionResult Delete(Guid key) 
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ArtisticManifestationRemover)]
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

        protected override void PopulateViewModelDisplayExtras(ArtisticManifestationDisplayViewModel modelDisplay, ArtisticManifestation entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.GroupId == null) return;

            var group = artisticManifestationGroupRepository.Get(entity.GroupId.Value);

            modelDisplay.Group = new ArtisticManifestationGroupDisplayViewModel();

            modelDisplay.Group.Import(group);

            CompleteManifestationGroup(group, modelDisplay.Group);
        }

        private void CompleteManifestationGroup(ArtisticManifestationGroup entity, ArtisticManifestationGroupDisplayViewModel displayModel)
        {
            if (entity.ParentId == null) return;

            var parent = artisticManifestationGroupRepository.Get(entity.Id);

            displayModel.Parent = new ArtisticManifestationGroupDisplayViewModel();

            displayModel.Parent.Import(parent);

            CompleteManifestationGroup(entity.Parent, displayModel.Parent);
        }

        private void PopulateTree(ArtisticManifestationInputViewModel modelInput)
        {
            var groups = artisticManifestationGroupRepository.GetAll().ToList();
            
            modelInput.SerializedTree = Newtonsoft.Json.JsonConvert.SerializeObject(GetTree(groups, null));
        }

        private List<object> GetTree(List<ArtisticManifestationGroup> groups, Guid? parentId)
        {
            var result = new List<object>();

            groups = artisticManifestationGroupRepository.GetAll().Where(m => m.ParentId == parentId).ToList();
            
            if (!(groups.Count() == 0))
            {
                foreach (var group in groups)
                {
                    result.Add(new
                    {
                        id = group.Id,
                        text = group.Name,
                        children = GetTree(groups, group.Id)
                    });
                }
            }

            return result;
        }
    }
}
