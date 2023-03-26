using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("manifestations")]
    public class ServiceLinesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ServiceLine, Guid, ServiceLineInputViewModel, ServiceLineDisplayViewModel>
    {
        private readonly IEntityRepository<ServiceLineGroup, Guid> serviceLineGroupRepository;

        public ServiceLinesController(IEntityRepository<ServiceLineGroup, Guid> serviceLineGroupRepository, IEntityRepository<ServiceLine, Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ServiceLinesController> localizer, ILogger<ServiceLinesController> logger) : base(repository, localizer, logger)
        {
            this.serviceLineGroupRepository = serviceLineGroupRepository;
        }

        [RequirePermissions(Permission.ServiceLineViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ServiceLineCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ServiceLineCreator)]
        public override IActionResult Create(ServiceLineInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequirePermissions(Permission.ServiceLineEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ServiceLineEditor)]
        public override IActionResult Edit(ServiceLineInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ServiceLineRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Permission.ServiceLineRemover)]
        public override IActionResult Delete(ServiceLineDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(ServiceLineInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ServiceLineInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigureEdit(ServiceLine entity, ServiceLineInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ServiceLine entity, ServiceLineInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void PopulateViewModelDisplayExtras(ServiceLineDisplayViewModel modelDisplay, ServiceLine entity)
        {
            base.PopulateViewModelDisplayExtras(modelDisplay, entity);

            if (entity.GroupId == null) return;

            var group = serviceLineGroupRepository.Get(entity.GroupId.Value);

            modelDisplay.Group = new ServiceLineGroupDisplayViewModel();

            modelDisplay.Group.Import(group);

            CompleteManifestationGroup(group, modelDisplay.Group);
        }

        private void CompleteManifestationGroup(ServiceLineGroup entity, ServiceLineGroupDisplayViewModel displayModel)
        {
            if (entity.ParentId == null) return;

            var parent = serviceLineGroupRepository.Get(entity.Id);

            displayModel.Parent = new ServiceLineGroupDisplayViewModel();

            displayModel.Parent.Import(parent);

            CompleteManifestationGroup(entity.Parent, displayModel.Parent);
        }

        private void PopulateTree(ServiceLineInputViewModel modelInput)
        {
            var groups = serviceLineGroupRepository.GetAll().ToList();

            modelInput.SerializedTree = Newtonsoft.Json.JsonConvert.SerializeObject(GetTree(groups, null));
        }

        private List<object> GetTree(List<ServiceLineGroup> groups, Guid? parentId)
        {
            var result = new List<object>();

            groups = serviceLineGroupRepository.GetAll().Where(m => m.ParentId == parentId).ToList();

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
