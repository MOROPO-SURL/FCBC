using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
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

        protected override void ConfigureCreate(ManifestationKindInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ManifestationKindInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigureEdit(ManifestationKind entity, ManifestationKindInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ManifestationKindInputViewModel modelInput)
        {
            PopulateTree(modelInput);
        }

        private void PopulateTree(ManifestationKindInputViewModel modelInput)
        {
            var manifestationKinds = manifestationKindRepository.GetAll().ToList();
            modelInput.SerializedTree = Newtonsoft.Json.JsonConvert.SerializeObject(GetTree(manifestationKinds, modelInput.Id, null));
        }

        private List<object> GetTree(List<ManifestationKind> manifestationKinds, System.Guid? id, System.Guid? parentId)
        {
            var result = new List<object>();
            
            manifestationKinds = manifestationKindRepository.GetAll().Where(m => m.ParentId == parentId).ToList();
            if (!(manifestationKinds.Count() == 0))
            {
                foreach (var manifestationKind in manifestationKinds)
                {
                    if(id != manifestationKind.Id)
                        result.Add(new
                        {
                            id = manifestationKind.Id,
                            text = manifestationKind.Name,
                            children = GetTree(manifestationKinds, id, manifestationKind.Id)
                        });
                }
            }
            return result;
        }
    }
}
