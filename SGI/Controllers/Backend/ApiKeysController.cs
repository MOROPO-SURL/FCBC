using Microsoft.AspNetCore.Mvc;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Security.Common.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.FCBC.SGI.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("security")]
    public class ApiKeysController : SAPRESSC.Core.Web.Backend.Controllers.AbstractHabilitableEntityManagementController<ApiKey, System.Guid, ApiKeyInputViewModel, ApiKeyDisplayViewModel>
    {
        private readonly IEntityRepository<Permission, int> permissionRepository;

        public ApiKeysController(IEntityRepository<Permission, int> permissionRepository, IEntityRepository<ApiKey, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ApiKeysController> localizer, Microsoft.Extensions.Logging.ILogger<ApiKeysController> logger) : base(repository, localizer, logger)
        {
            this.permissionRepository = permissionRepository;
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyCreator)]
        public override IActionResult Create(ApiKeyInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyEditor)]
        public override IActionResult Edit(ApiKeyInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyRemover)]
        public override IActionResult Delete(ApiKeyDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyEditor)]
        public override IActionResult Enable(System.Guid key)
        {
            return base.Enable(key);
        }

        [RequirePermissions(Security.Authorization.Permission.ApiKeyEditor)]
        public override IActionResult Disable(System.Guid key)
        {
            return base.Disable(key);
        }

        private void PopulateModelInputForAvailablePermissions(ApiKeyInputViewModel modelInput, ApiKey entity = null)
        {
            var unfamiliedPermissions = permissionRepository.FindBy(item => item.Family == null).ToArray();

            modelInput.UngroupedSelectablePermissions = unfamiliedPermissions.Select(item => new SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<int>
            {
                Id = item.Id,
                Selected = entity != null && entity.PermissionBridges.Any(e => e.PermissionId.Equals(item.Id)),
                Label = item.Name,
                Description = item.Description
            }).ToArray();

            var familiedPermissionGroups = permissionRepository.FindBy(item => item.Family != null).GroupBy(item => item.Family).ToArray();

            modelInput.GroupedSelectablePermissions = familiedPermissionGroups.Select(item => new System.Collections.Generic.KeyValuePair<string, SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<int>[]>
            (
                item.Key,
                item.Select(it3m => new SAPRESSC.Core.Common.Models.View.SelectionDescriptedItemInputViewModel<int>
                {
                    Id = it3m.Id,
                    Selected = entity != null && entity.PermissionBridges.Any(e => e.PermissionId.Equals(it3m.Id)),
                    Label = it3m.Name,
                    Description = it3m.Description
                }).ToArray()
            )).ToArray();
        }

        protected override void ConfigureCreate(ApiKeyInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateModelInputForAvailablePermissions(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ApiKeyInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateModelInputForAvailablePermissions(modelInput);
        }

        protected override void ConfigureEdit(ApiKey entity, ApiKeyInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateModelInputForAvailablePermissions(modelInput, entity);
        }

        protected override void ConfigurePostFailEditValidation(ApiKey entity, ApiKeyInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            PopulateModelInputForAvailablePermissions(modelInput, entity);
        }

        protected override void PreCreate(ApiKey entity, ApiKeyInputViewModel modelInput)
        {
            base.PostCreate(entity, modelInput);

            entity.PermissionBridges = modelInput.UngroupedSelectablePermissions.Where(item => item.Selected).Select(item => new ApiKeyPermissionBridge { PermissionId = item.Id, ApiKey = entity }).ToList();
        }

        protected override void PreEdit(ApiKey entity, ApiKeyInputViewModel modelInput)
        {
            base.PostEdit(entity, modelInput);

            entity.PermissionBridges.Clear();

            entity.PermissionBridges = modelInput.UngroupedSelectablePermissions.Where(item => item.Selected).Select(item => new ApiKeyPermissionBridge { PermissionId = item.Id, ApiKeyId = entity.Id }).ToList();
        }
    }
}
