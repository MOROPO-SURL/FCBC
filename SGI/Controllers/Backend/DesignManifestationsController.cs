using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequiresRoleBasePermission(SmartSolucionesCuba.SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("manifestations")]
    public class DesignManifestationsController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<DesignManifestation, Guid, DesignManifestationInputViewModel, DesignManifestationDisplayViewModel>
    {
        private readonly IEntityRepository<DesignManifestationGroup, Guid> designManifestationGroupRepository;

        public DesignManifestationsController(IEntityRepository<DesignManifestationGroup, Guid> designManifestationGroupRepository, IEntityRepository<DesignManifestation, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<DesignManifestationsController> localizer, ILogger<DesignManifestationsController> logger) : base(repository, localizer, logger)
        {
            this.designManifestationGroupRepository = designManifestationGroupRepository;
        }

        [RequiresRoleBasePermission(Roles.DesignManifestationViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.DesignManifestationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.DesignManifestationCreator)]
        public override IActionResult Create(DesignManifestationInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.DesignManifestationEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.DesignManifestationEditor)]
        public override IActionResult Edit(DesignManifestationInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.DesignManifestationRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.DesignManifestationRemover)]
        public override IActionResult Delete(DesignManifestationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(DesignManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(DesignManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigureEdit(DesignManifestation entity, DesignManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(DesignManifestationInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void PopulateViewModelDisplayExtras(DesignManifestationDisplayViewModel modelDisplay, DesignManifestation entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.GroupId == null) return;

            var group = designManifestationGroupRepository.Get(entity.GroupId.Value);

            modelDisplay.Group = new DesignManifestationGroupDisplayViewModel();

            modelDisplay.Group.Import(group);

            CompleteManifestationGroup(group, modelDisplay.Group);
        }

        private void CompleteManifestationGroup(DesignManifestationGroup entity, DesignManifestationGroupDisplayViewModel displayModel)
        {
            if (entity.ParentId == null) return;

            var parent = designManifestationGroupRepository.Get(entity.Id);

            displayModel.Parent = new DesignManifestationGroupDisplayViewModel();

            displayModel.Parent.Import(parent);

            CompleteManifestationGroup(entity.Parent, displayModel.Parent);
        }

        private void PopulateTree(DesignManifestationInputViewModel modelInput)
        {
            var groups = designManifestationGroupRepository.GetAll().ToList();

            modelInput.SerializedTree = Newtonsoft.Json.JsonConvert.SerializeObject(GetTree(groups, null));
        }

        private List<object> GetTree(List<DesignManifestationGroup> groups, Guid? parentId)
        {
            var result = new List<object>();

            groups = designManifestationGroupRepository.GetAll().Where(m => m.ParentId == parentId).ToList();

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
