using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class CreationGroupsController : AbstractEntityManagementController<CreationGroup, Guid, CreationGroupInputViewModel, CreationGroupDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;
        public CreationGroupsController(IEntityRepository<CreationGroup, Guid> repository, IStringLocalizer<CreationGroupsController> localizer, ILogger<CreationGroupsController> logger, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
        }

        [RequiresRoleBasePermission(Roles.CreationGroupViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.CreationGroupCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.CreationGroupCreator)]
        public override IActionResult Create(CreationGroupInputViewModel inputViewModel)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("ImageFile");

                    inputViewModel.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(inputViewModel);

                    updateModelTask.Wait();
                }
            }
            return base.Create(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.CreationGroupEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.CreationGroupEditor)]
        public override IActionResult Edit(CreationGroupInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.CreationGroupRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.CreationGroupRemover)]
        public override IActionResult Delete(CreationGroupDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "CreationGroup " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PreEdit(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.OrganizationIdentity.Image != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.OrganizationIdentity.Image);

                    imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.OrganizationIdentity.Image != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.OrganizationIdentity.Image);

                imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "CreationGroup " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PostDelete(CreationGroup entity)
        {
            base.PostDelete(entity);

            organizationIdentityRepository.Delete(entity.OrganizationIdentity, User.Identity.Name);

            if (entity.OrganizationIdentity.Image == null) return;

            imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.OrganizationIdentity.Image);
        }
    }
}
