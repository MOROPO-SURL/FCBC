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
    [Area("complements")]
    public class ServiceLineGroupsController : SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ServiceLineGroup, System.Guid, ServiceLineGroupInputViewModel, ServiceLineGroupDisplayViewModel>
    {
        public ServiceLineGroupsController(IEntityRepository<ServiceLineGroup, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ServiceLineGroupsController> localizer, ILogger<ServiceLineGroupsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequiresRoleBasePermission(Roles.ServiceLineGroupViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ServiceLineGroupCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ServiceLineGroupCreator)]
        public override IActionResult Create(ServiceLineGroupInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ServiceLineGroupEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ServiceLineGroupEditor)]
        public override IActionResult Edit(ServiceLineGroupInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ServiceLineGroupRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ServiceLineGroupRemover)]
        public override IActionResult Delete(ServiceLineGroupDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PopulateViewModelDisplayExtras(ServiceLineGroupDisplayViewModel modelDisplay, ServiceLineGroup entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.ParentId == null) return;

            var parent = Repository.Get(entity.ParentId.Value);

            modelDisplay.Parent = new ServiceLineGroupDisplayViewModel();

            modelDisplay.Parent.Import(parent);
        }

        protected override void ConfigureCreate(ServiceLineGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ServiceLineGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigureEdit(ServiceLineGroup entity, ServiceLineGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ServiceLineGroupInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        private void PopulateTree(ServiceLineGroupInputViewModel modelInput)
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
