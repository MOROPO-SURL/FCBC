using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Backend.Controllers;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System;
using MOROPO.FCBC.SGI.Security.Authorization;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.EF.Repositories;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.EF.Repositories;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("institution")]
    public class ActivitiesController : AbstractEntityManagementController<Activity, Guid, ActivityInputViewModel, ActivityDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<ActivityKind, Guid> activityKindRepository;
        public ActivitiesController(IEntityRepository<Activity, Guid> repository, IStringLocalizer<ActivitiesController> localizer, ILogger<ActivitiesController> logger, IEntityRepository<ActivityKind, Guid> activityKindRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository) : base(repository, localizer, logger)
        {
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.activityKindRepository = activityKindRepository;
        }

        [RequirePermissions(Permission.ActivityViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Permission.ActivityCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Permission.ActivityCreator)]
        public override IActionResult Create(ActivityInputViewModel inputViewModel)
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

        [RequirePermissions(Permission.ActivityEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Permission.ActivityEditor)]
        public override IActionResult Edit(ActivityInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequirePermissions(Permission.ActivityRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        private void PopulateWithActivityKind(ActivityInputViewModel modelInput)
        {
            modelInput.ActivityKinds = activityKindRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();
        }

        private void PopulateWithActivityKindEdit(ActivityInputViewModel modelInput)
        {
            modelInput.ActivityKinds = activityKindRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();
        }

        protected override void ConfigureCreate(ActivityInputViewModel modelInput)
        {
            PopulateWithActivityKind(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ActivityInputViewModel modelInput)
        {
            PopulateWithActivityKind(modelInput);
        }

        protected override void ConfigureEdit(Activity entity, ActivityInputViewModel modelInput)
        {
            PopulateWithActivityKindEdit(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ActivityInputViewModel modelInput)
        {
            PopulateWithActivityKind(modelInput);
        }

        protected override void PreCreate(Activity entity, ActivityInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.ActivityKind = activityKindRepository.Get(modelInput.ActivityKindId);

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Activity " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.FeatureImage = image;
            }
        }

        protected override void PreEdit(Activity entity, ActivityInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.ActivityKind.Id != modelInput.ActivityKindId)
            {
                entity.ActivityKind = activityKindRepository.Get(modelInput.ActivityKindId);
            }

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.FeatureImage != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.FeatureImage);

                    imageRepository.Delete(entity.FeatureImage, User.Identity.Name);
                }

                return;
            }

            if (entity.FeatureImage != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.FeatureImage);

                imageRepository.Delete(entity.FeatureImage, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Activity " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.FeatureImage = image;
            }
        }

        protected override void PostDelete(Activity entity)
        {
            base.PostDelete(entity);

            if (entity.FeatureImage == null) return;

            imageRepository.Delete(entity.FeatureImage, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.FeatureImage);
        }
    }
}

