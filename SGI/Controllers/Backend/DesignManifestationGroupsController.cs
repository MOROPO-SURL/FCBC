using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("complements")]
    public class DesignManifestationGroupsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<DesignManifestationGroup, System.Guid, DesignManifestationGroupInputViewModel, DesignManifestationGroupDisplayViewModel>
    {
        public DesignManifestationGroupsController(IEntityRepository<DesignManifestationGroup, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<DesignManifestationGroupsController> localizer, ILogger<DesignManifestationGroupsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Permission.DesignManifestationGroupViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.DesignManifestationGroupCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.DesignManifestationGroupCreator)]
        public override IActionResult Create(DesignManifestationGroupInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.DesignManifestationGroupEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.DesignManifestationGroupEditor)]
        public override IActionResult Edit(DesignManifestationGroupInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.DesignManifestationGroupRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.DesignManifestationGroupRemover)]
        public override IActionResult Delete(DesignManifestationGroupDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PopulateViewModelDisplayExtras(DesignManifestationGroupDisplayViewModel modelDisplay, DesignManifestationGroup entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.ParentId == null) return;

            var parent = Repository.Get(entity.ParentId.Value);

            modelDisplay.Parent = new DesignManifestationGroupDisplayViewModel();

            modelDisplay.Parent.Import(parent);
        }

        protected override void ConfigureCreate(DesignManifestationGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(DesignManifestationGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigureEdit(DesignManifestationGroup entity, DesignManifestationGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(DesignManifestationGroup entity, DesignManifestationGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        private void PopulateTree(DesignManifestationGroupInputViewModel modelInput)
        {
            modelInput.SerializedTree = Newtonsoft.Json.JsonConvert.SerializeObject(GetTree(modelInput.Id, null));
        }

        private System.Collections.Generic.List<object> GetTree(System.Guid? targetId, System.Guid? parentId)
        {
            var result = new System.Collections.Generic.List<object>();

            var entities = Repository.FindBy(m => m.ParentId == parentId).ToArray();

            if (entities.Length > 0)
            {
                foreach (var entity in entities)
                {
                    if (targetId != entity.Id) result.Add(new
                    {
                        id = entity.Id,
                        text = entity.Name,
                        children = GetTree(targetId, entity.Id)
                    });
                }
            }

            return result;
        }
    }
}
