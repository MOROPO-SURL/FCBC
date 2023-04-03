using Microsoft.AspNetCore.Mvc;
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
    public class CraftManifestationsController : AbstractEntityManagementController<CraftManifestation, Guid, CraftManifestationInputViewModel, CraftManifestationDisplayViewModel>
    {
        private readonly IEntityRepository<CraftManifestationGroup, Guid> craftManifestationGroupRepository;

        public CraftManifestationsController(IEntityRepository<CraftManifestationGroup, Guid> craftManifestationGroupRepository, IEntityRepository<CraftManifestation, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CraftManifestationsController> localizer, ILogger<CraftManifestationsController> logger) : base(repository, localizer, logger)
        {
            this.craftManifestationGroupRepository = craftManifestationGroupRepository;
        }

        [RequirePermissions(Permission.CraftManifestationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.CraftManifestationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.CraftManifestationCreator)]
        public override IActionResult Create(CraftManifestationInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.CraftManifestationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.CraftManifestationEditor)]
        public override IActionResult Edit(CraftManifestationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.CraftManifestationRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.CraftManifestationRemover)]
        public override IActionResult Delete(CraftManifestationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(CraftManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(CraftManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigureEdit(CraftManifestation entity, CraftManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(CraftManifestation entity, CraftManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void PopulateViewModelDisplayExtras(CraftManifestationDisplayViewModel modelDisplay, CraftManifestation entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.GroupId == null) return;

            var group = craftManifestationGroupRepository.Get(entity.GroupId.Value);

            modelDisplay.Group = new CraftManifestationGroupDisplayViewModel();

            modelDisplay.Group.Import(group);

            CompleteManifestationGroup(group, modelDisplay.Group);
        }

        private void CompleteManifestationGroup(CraftManifestationGroup entity, CraftManifestationGroupDisplayViewModel displayModel)
        {
            if (entity.ParentId == null) return;

            var parent = craftManifestationGroupRepository.Get(entity.Id);

            displayModel.Parent = new CraftManifestationGroupDisplayViewModel();

            displayModel.Parent.Import(parent);

            CompleteManifestationGroup(entity.Parent, displayModel.Parent);
        }

        private void PopulateTree(CraftManifestationInputViewModel modelInput)
        {
            var groups = craftManifestationGroupRepository.GetAll().ToList();

            modelInput.SerializedTree = Newtonsoft.Json.JsonConvert.SerializeObject(GetTree(groups, null));
        }

        private List<object> GetTree(List<CraftManifestationGroup> groups, Guid? parentId)
        {
            var result = new List<object>();

            groups = craftManifestationGroupRepository.GetAll().Where(m => m.ParentId == parentId).ToList();

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
