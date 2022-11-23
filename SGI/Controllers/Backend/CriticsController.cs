using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("complements")]
    public class CriticsController : AbstractEntityManagementFilteredController<Critic, System.Guid, CriticInputViewModel, CriticDisplayViewModel, CriticFilterModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<PersonIdentity, System.Guid> identityRepository;
        public CriticsController(IEntityRepository<PersonIdentity, System.Guid> identityRepository, IEntityRepository<Image, System.Guid> imageRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Critic, Guid> repository, IStringLocalizer<CriticsController> localizer, ILogger<CriticsController> logger) : base(repository, localizer, logger)
        {
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.identityRepository = identityRepository;
        }

        [RequiresRoleBasePermission(Roles.CriticViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.CriticViewer)]
        public override IActionResult Filter(CriticFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "critic-management-filter";
        }

        [RequiresRoleBasePermission(Roles.CriticCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.CriticCreator)]
        public override IActionResult Create(CriticInputViewModel inputViewModel)
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

        [RequiresRoleBasePermission(Roles.CriticEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.CriticEditor)]
        public override IActionResult Edit(CriticInputViewModel inputViewModel)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    inputViewModel.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(inputViewModel);

                    updateModelTask.Wait();
                }
            }
            return base.Edit(inputViewModel);
        }

        [RequiresRoleBasePermission(Roles.CriticRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.CriticRemover)]
        public override IActionResult Delete(CriticDisplayViewModel displayViewModel)
        {
            return base.Delete(displayViewModel);
        }

        protected override void PreCreate(Critic entity, CriticInputViewModel inputViewModel)
        {
            base.PreCreate(entity, inputViewModel);

            if (inputViewModel.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Critic " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.PersonIdentity.Image = image;
            }
        }

        protected override void PreEdit(Critic entity, CriticInputViewModel inputViewModel)
        {
            base.PreEdit(entity, inputViewModel);

            if (inputViewModel.ImageFile == null)
            {
                if (inputViewModel.ResetImage && entity.PersonIdentity.Image != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.PersonIdentity.Image);

                    imageRepository.Delete(entity.PersonIdentity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.PersonIdentity.Image != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.PersonIdentity.Image);

                imageRepository.Delete(entity.PersonIdentity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Critic " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.PersonIdentity.Image = image;
            }
        }

        protected override void PostDelete(Critic entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.PersonIdentity, User.Identity.Name);

            if (entity.PersonIdentity.Image == null) return;

            imageRepository.Delete(entity.PersonIdentity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.PersonIdentity.Image);
        }
    }
}
