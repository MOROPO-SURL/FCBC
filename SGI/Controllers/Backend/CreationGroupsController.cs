using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class CreationGroupsController : AbstractEntityManagementController<CreationGroup, Guid, CreationGroupInputViewModel, CreationGroupDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;
        private readonly IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository;
        private readonly IEntityRepository<Artist, Guid> artistRepository;
        public CreationGroupsController(IEntityRepository<Artist, Guid> artistRepository, IEntityRepository<ArtisticManifestation, Guid> artisticManifestationRepository, IEntityRepository<CreationGroup, Guid> repository, IStringLocalizer<CreationGroupsController> localizer, ILogger<CreationGroupsController> logger, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.artisticManifestationRepository = artisticManifestationRepository;
            this.artistRepository = artistRepository;
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

        private void PopulateWithArtisticManifestation(CreationGroupInputViewModel modelInput)
        {
            List<SelectListItem> artisticManifestationItems = artisticManifestationRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToList();
            modelInput.ArtisticManifestations = new MultiSelectList(artisticManifestationItems.OrderBy(i => i.Text), "Value", "Text", modelInput.SelectedValues);
        }

        private void PopulateWithArtisticManifestationEdit(CreationGroupInputViewModel modelInput)
        {
            var SelectedValues = modelInput.SelectedValues;

            List<SelectListItem> artisticManifestationItems = artisticManifestationRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = SelectedValues.Any(s => s == entity.Id) }).ToList();
            modelInput.ArtisticManifestations = new MultiSelectList(artisticManifestationItems.OrderBy(i => i.Text), "Value", "Text", modelInput.SelectedValues);
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

                entity.Identity.Image = image;
            }
        }

        protected override void PreEdit(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {

            base.PreEdit(entity, modelInput);

            modelInput.CreationGroupArtisticManifestationBrigdes.Clear();

            foreach (var artisticManifestationId in modelInput.SelectedValues)
            {
                ArtisticManifestation artisticMani = this.artisticManifestationRepository.GetAll().FirstOrDefault(r => r.Id == artisticManifestationId);
                ArtisticManifestationInputViewModel artisticManifestationInputViewModel = new ArtisticManifestationInputViewModel
                {
                    Id = artisticManifestationId,
                    Name = artisticMani.Name,

                };
                var CreationGroupArtisticManifestationBrigde = new CreationGroupArtisticManifestationBridge { GroupId = modelInput.Id, ManifestationId = artisticManifestationId, Manifestation = artisticManifestationInputViewModel.Export(), Group = modelInput.Export() };
                entity.ArtisticManifestationBrigdes.Add(CreationGroupArtisticManifestationBrigde);

            }

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.Identity.Image != null)
                {
                    imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.Identity.Image != null)
            {
                imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);

                imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "CreationGroup " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void ConfigureCreate(CreationGroupInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);
        }

        protected override void ConfigureEdit(CreationGroup entity, CreationGroupInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);
        }

        protected override void PostDelete(CreationGroup entity)
        {
            base.PostDelete(entity);

            organizationIdentityRepository.Delete(entity.Identity, User.Identity.Name);

            if (entity.Identity.Image == null) return;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.Identity.Image);
        }

        [Route("edit/{key}/members")]
        [RequiresRoleBasePermission(Roles.CreationGroupEditor)]
        public IActionResult Members(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);
                var members = entity.CreationGroupArtistBridges.Select(b => b.Artist).ToList();
                var notMemberArtist = artistRepository.GetAll().ToList();
                if (members.Count() != 0)
                {
                    notMemberArtist = notMemberArtist.Except(members).ToList();
                }
                
                var resultModel = new CreationGroupInputViewModel ();
                resultModel.Import(entity);
                resultModel.NotMembers = notMemberArtist.Select(n => new SelectListItem { Value = n.Id.ToString(), Text = n.Identity.Name+" "+n.Identity.LastName }).ToArray();
                resultModel.Members = members;
                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/members/add")]
        [RequiresRoleBasePermission(Roles.CreationGroupEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMember(CreationGroupMembersInputViewModel modelInput)
        {
            CreationGroup entity = null;

            try
            {
                entity = Repository.Get(modelInput.CreationGroupId);
            }
            catch { }

            if (entity == null)
            {
                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }

            if (ModelState.IsValid)
            {
                entity.CreationGroupArtistBridges.Add(new CreationGroupArtistBridge { CreationGroupId = entity.Id, ArtistId = modelInput.ArtistId });

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());
            }
            else
            {
                var messages = new System.Collections.Generic.List<string>();

                foreach (var item in ModelState)
                {
                    if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                    foreach (var error in item.Value.Errors)
                    {
                        messages.Add(error.ErrorMessage);
                    }
                }

                SetDefaultNotification(SmartSolucionesCuba.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, messages.ToArray());
            }

            return RedirectToAction("members", new { key = entity.Id });
        }
    }
}
