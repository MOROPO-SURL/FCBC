using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("complements")]
    public class ManifestationKindsController : AbstractEntityManagementController<ManifestationKind, Guid, ManifestationKindInputViewModel, ManifestationKindDisplayViewModel>
    {
        private IEntityRepository<ManifestationKind, Guid> manifestationKindRepository;

        public ManifestationKindsController(IEntityRepository<ManifestationKind, Guid> manifestationKindRepository, IEntityRepository<ManifestationKind, Guid> repository, IStringLocalizer<ManifestationKindsController> localizer, ILogger<ManifestationKindsController> logger) : base(repository, localizer, logger)
        {
            this.manifestationKindRepository = manifestationKindRepository;
        }

        [RequiresRoleBasePermission(Roles.ManifestationKindViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ManifestationKindCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ManifestationKindCreator)]
        public override IActionResult Create(ManifestationKindInputViewModel inputViewModel)
        {
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.ManifestationKindEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ManifestationKindEditor)]
        public override IActionResult Edit(ManifestationKindInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ManifestationKindRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ManifestationKindRemover)]
        public override IActionResult Delete(ManifestationKindDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateWithParents(ManifestationKindInputViewModel modelInput)
        {
            //modelInput.ParentsManifestationKinds = manifestationKindRepository.GetAll().Select(entity => new SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
            modelInput.ParentsManifestationKinds=PreOrdenParent("", manifestationKindRepository.GetAll(), null);
        }

        protected override void ConfigureCreate(ManifestationKindInputViewModel modelInput)
        {
            PopulateWithParents(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ManifestationKindInputViewModel modelInput)
        {
            PopulateWithParents(modelInput);
        }

        protected override void ConfigureEdit(ManifestationKind entity, ManifestationKindInputViewModel modelInput)
        {
            PopulateWithParents(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ManifestationKindInputViewModel modelInput)
        {
            PopulateWithParents(modelInput);
        }

        protected override void PreCreate(ManifestationKind entity, ManifestationKindInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.ParentId = modelInput.ParentId;
        }

        protected override void PreEdit(ManifestationKind entity, ManifestationKindInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);            
        }

        protected override void ValidateCreate(ManifestationKindInputViewModel modelInput)
        {
            base.ValidateCreate(modelInput);
            
        }

        protected override void ValidateEdit(ManifestationKindInputViewModel modelInput)
        {
            base.ValidateEdit(modelInput);
            
        }

        private List<SelectListItem> PreOrdenParent(string prefix, IQueryable<ManifestationKind> manifestationKinds, System.Guid? parentId)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            var childrens = manifestationKinds.Where( m => m.ParentId == parentId);
            foreach (var child in childrens)
            {
                if (child.Parent == null)
                {
                    result.Add(new SelectListItem { Value = child.Id.ToString(), Text = prefix + child.Name });
                    result.AddRange(PreOrdenParent(prefix + " - ", manifestationKinds, child.Id));
                }
                else
                {
                    result.Add(new SelectListItem { Value = child.Id.ToString(), Text = prefix + child.Parent.Name + prefix + child.Name });
                    result.AddRange(PreOrdenParent(prefix + "  ", manifestationKinds, child.Id));
                }
                
            }

            return result;
        }
    }
}
