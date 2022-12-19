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
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequiresRoleBasePermission(SmartSolucionesCuba.SAPRESSC.Core.Web.Security.SecurityRoleCodes.ShellManager)]
    [Area("complements")]
    public class ArtisticManifestationGroupsController : AbstractEntityManagementController<ArtisticManifestationGroup, System.Guid, ArtisticManifestationGroupInputViewModel, ArtisticManifestationGroupDisplayViewModel>
    {
        public ArtisticManifestationGroupsController(IEntityRepository<ArtisticManifestationGroup, System.Guid> repository, IStringLocalizer<ArtisticManifestationGroupsController> localizer, ILogger<ArtisticManifestationGroupsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationGroupViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationGroupCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationGroupCreator)]
        public override IActionResult Create(ArtisticManifestationGroupInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationGroupEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationGroupEditor)]
        public override IActionResult Edit(ArtisticManifestationGroupInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationGroupRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ArtisticManifestationGroupRemover)]
        public override IActionResult Delete(ArtisticManifestationGroupDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PopulateViewModelDisplayExtras(ArtisticManifestationGroupDisplayViewModel modelDisplay, ArtisticManifestationGroup entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.ParentId == null) return;

            var parent = Repository.Get(entity.ParentId.Value);

            modelDisplay.Parent = new ArtisticManifestationGroupDisplayViewModel();

            modelDisplay.Parent.Import(parent);
        }

        protected override void ConfigureCreate(ArtisticManifestationGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ArtisticManifestationGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigureEdit(ArtisticManifestationGroup entity, ArtisticManifestationGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ArtisticManifestationGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        private void PopulateTree(ArtisticManifestationGroupInputViewModel modelInput)
        {
            modelInput.SerializedTree = Newtonsoft.Json.JsonConvert.SerializeObject(GetTree(modelInput.Id, null));
        }

        private List<object> GetTree(System.Guid? targetId, System.Guid? parentId)
        {
            var result = new List<object>();
            
            var entities = Repository.FindBy(m => m.ParentId == parentId).ToArray();
            
            if (entities.Length > 0)
            {
                foreach (var entity in entities)
                {
                    if(targetId != entity.Id) result.Add(new
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
