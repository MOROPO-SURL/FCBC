using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.RegionConst;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class ArtistsController : AbstractEntityManagementFilteredController<Artist, System.Guid, ArtistInputViewModel, ArtistDisplayViewModel, ArtistFilterModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<PersonIdentity, System.Guid> identityRepository;
        private readonly IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository;
        private readonly IEntityRepository<State, Guid> stateRepository;
        private readonly IEntityRepository<Municipality, Guid> municipalityRepository;
        public ArtistsController(IEntityRepository<PersonIdentity, System.Guid> identityRepository, IEntityRepository<Image, System.Guid> imageRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository, IEntityRepository<State, Guid> stateRepository, IEntityRepository<Municipality, Guid> municipalityRepository, IEntityRepository<Artist, Guid> repository, IStringLocalizer<ArtistsController> localizer, ILogger<ArtistsController> logger) : base(repository, localizer, logger)
        {
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.identityRepository = identityRepository;
            this.municipalityRegionRepository = municipalityRegionRepository;
            this.stateRepository = stateRepository;
            this.municipalityRepository = municipalityRepository;
        }

        [RequiresRoleBasePermission(Roles.ArtistViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ArtistViewer)]
        public override IActionResult Filter(ArtistFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "artist-management-filter";
        }

        [RequiresRoleBasePermission(Roles.ArtistCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ArtistCreator)]
        public override IActionResult Create(ArtistInputViewModel inputViewModel)
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

        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ArtistEditor)]
        public override IActionResult Edit(ArtistInputViewModel inputViewModel)
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

        [RequiresRoleBasePermission(Roles.ArtistRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.ArtistRemover)]
        public override IActionResult Delete(ArtistDisplayViewModel displayViewModel)
        {
            return base.Delete(displayViewModel);
        }

        [HttpGet("PopulateStateToMunicipality/{stateId}")]
        public IActionResult PopulateStateToMunicipality(string stateId)
        {
            var stateIdParse = System.Guid.Parse(stateId);

            var municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == stateIdParse).ToList();
            return Ok(municipalities);
        }

        [HttpGet("edit/PopulateStateToMunicipalityEdit/{stateId}")]
        public IActionResult PopulateStateToMunicipalityEdit(string stateId)
        {
            var stateIdParse = System.Guid.Parse(stateId);

            var municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == stateIdParse).ToList();
            return Ok(municipalities);
        }

        private void PopulateWithCountries(ArtistInputViewModel modelInput)
        {
            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Name == RegionInitConstant.CMG_STATE_NAME }).OrderByDescending(en => en.Selected).ToArray();

            modelInput.Municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == RegionInitConstant.MY_OWN_SATE).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        private void PopulateWithCountriesEdit(ArtistInputViewModel modelInput)
        {
            modelInput.StateId = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId).StateId;

            modelInput.Municipalities = municipalityRepository.GetAll().Where(s => s.StateId == modelInput.StateId).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.MunicipalityId }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.StateId }).ToArray();
        }

        protected override void PreCreate(Artist entity, ArtistInputViewModel inputViewModel)
        {
            base.PreCreate(entity, inputViewModel);

            var region = municipalityRegionRepository.FindOneBy(r => r.MunicipalityId == inputViewModel.MunicipalityId);
            PostalContactItem postalContact = new PostalContactItem
            {
                Address = inputViewModel.AddressPostal,
                RegionId = region.Id,
                Region = region
            };

            PersonPostalContactItemBridge personPostalContactItemBridge = new PersonPostalContactItemBridge
            {
                PersonId = inputViewModel.Export().PersonIdentity.Id,
                Person = inputViewModel.Export().PersonIdentity,
                ContactItemId = postalContact.Id,
                ContactItem = postalContact
            };
            entity.PersonIdentity.PersonPostalContactItemBridges.Add(personPostalContactItemBridge);

            var getRegionRegister = entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            inputViewModel.Municipality = ((MunicipalityRegion)getRegionRegister).Municipality;
            inputViewModel.State = inputViewModel.Municipality.State;

            if (inputViewModel.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Critic " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.PersonIdentity.Image = image;
            }
        }

        protected override void PreEdit(Artist entity, ArtistInputViewModel inputViewModel)
        {
            base.PreEdit(entity, inputViewModel);

            var contact = entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem;
            contact.Address = inputViewModel.AddressPostal;
            var region = municipalityRegionRepository.GetAll().FirstOrDefault(p => p.MunicipalityId == inputViewModel.MunicipalityId);
            var municipality = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == inputViewModel.MunicipalityId);
            inputViewModel.StateId = municipality.StateId;
            if (region.Id != entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId)
            {
                entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region = region;
                entity.PersonIdentity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId = region.Id;

            }

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

        protected override void PostDelete(Artist entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.PersonIdentity, User.Identity.Name);

            if (entity.PersonIdentity.Image == null) return;

            imageRepository.Delete(entity.PersonIdentity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.PersonIdentity.Image);
        }

        protected override void ConfigureCreate(ArtistInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ArtistInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }

        protected override void ConfigureEdit(Artist entity, ArtistInputViewModel modelInput)
        {
            PopulateWithCountriesEdit(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ArtistInputViewModel modelInput)
        {
            PopulateWithCountries(modelInput);
        }
    }
}
