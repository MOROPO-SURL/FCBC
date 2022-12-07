using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
using MOROPO.FCBC.SGI.RegionConst;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.EF.Repositories;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class ContractorsController : AbstractEntityManagementController<Contractor, Guid, ContractorInputViewModel, ContractorDisplayViewModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository;
        private readonly IEntityRepository<ContractorKind, int> contractKindRepository;        
        private readonly IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository;
        private readonly IEntityRepository<State, Guid> stateRepository;
        private readonly IEntityRepository<Municipality, Guid> municipalityRepository;
        public ContractorsController(IEntityRepository<Contractor, Guid> repository, IStringLocalizer<ContractorsController> localizer, ILogger<ContractorsController> logger, IEntityRepository<OrganizationIdentity, Guid> organizationIdentityRepository, IEntityRepository<ContractorKind, int> contractKindRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Image, System.Guid> imageRepository,
             IEntityRepository<MunicipalityRegion, Guid> municipalityRegionRepository, IEntityRepository<State, Guid> stateRepository, IEntityRepository<Municipality, Guid> municipalityRepository) : base(repository, localizer, logger)
        {
            this.organizationIdentityRepository = organizationIdentityRepository;
            this.contractKindRepository = contractKindRepository;
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;            
            this.municipalityRegionRepository = municipalityRegionRepository;
            this.stateRepository = stateRepository;
            this.municipalityRepository = municipalityRepository;
        }

        [RequiresRoleBasePermission(Roles.ContractorViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.ContractorCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.ContractorCreator)]
        public override IActionResult Create(ContractorInputViewModel inputViewModel)
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

        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        public override IActionResult Edit(Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        public override IActionResult Edit(ContractorInputViewModel inputModel)
        {
            return base.Edit(inputModel);
        }

        [RequiresRoleBasePermission(Roles.ContractorRemover)]
        public override IActionResult Delete(Guid key)
        {
            return base.Delete(key);
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

        [Route("edit/{key}/contacts")]
        [RequiresRoleBasePermission(Roles.ContractorEditor)]
        public IActionResult Contacts()
        {
            return View();
        }
        
        [RequiresRoleBasePermission(Roles.ContractorRemover)]
        public override IActionResult Delete(ContractorDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        private void PopulateWithContractKind(ContractorInputViewModel modelInput)
        {
            modelInput.ContractKinds = contractKindRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Name == RegionInitConstant.CMG_STATE_NAME }).OrderByDescending(en => en.Selected).ToArray();

            modelInput.Municipalities = municipalityRepository.GetAll().Where(entity => entity.StateId == RegionInitConstant.MY_OWN_SATE).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToArray();
        }

        private void PopulateWithContractKindEdit(ContractorInputViewModel modelInput)
        {
            modelInput.ContractKinds = contractKindRepository.GetAll().Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToArray();

            modelInput.StateId = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId).StateId;

            modelInput.Municipalities = municipalityRepository.GetAll().Where(s => s.StateId == modelInput.StateId).Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.MunicipalityId }).ToArray();

            modelInput.States = stateRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = entity.Id == modelInput.StateId }).ToArray();

        }

        protected override void ConfigureCreate(ContractorInputViewModel modelInput)
        {
            PopulateWithContractKind(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ContractorInputViewModel modelInput)
        {
            PopulateWithContractKind(modelInput);
        }

        protected override void ConfigureEdit(Contractor entity, ContractorInputViewModel modelInput)
        {
            PopulateWithContractKindEdit(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(ContractorInputViewModel modelInput)
        {
            PopulateWithContractKind(modelInput);
        }

        protected override void PreCreate(Contractor entity, ContractorInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.ContractorKind = contractKindRepository.Get(modelInput.ContractKindId);

            var region = municipalityRegionRepository.FindOneBy(r => r.MunicipalityId == modelInput.MunicipalityId);
            PostalContactItem postalContact = new PostalContactItem
            {
                Address = modelInput.AddressPostal,
                RegionId = region.Id,
                Region = region
            };

            OrganizationPostalContactItemBridge organizationPostalContactItemBridge = new OrganizationPostalContactItemBridge
            {
                OrganizationId = modelInput.Export().OrganizationIdentity.Id,
                Organization = modelInput.Export().OrganizationIdentity,
                ContactItemId = postalContact.Id,
                ContactItem = postalContact
            };
            entity.OrganizationIdentity.OrganizationPostalContactItemBridges.Add(organizationPostalContactItemBridge);


            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Contractor " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PreEdit(Contractor entity, ContractorInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            var contact = entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem;
            contact.Address = modelInput.AddressPostal;
            var region = municipalityRegionRepository.GetAll().FirstOrDefault(p => p.MunicipalityId == modelInput.MunicipalityId);
            var municipality = municipalityRepository.GetAll().FirstOrDefault(p => p.Id == modelInput.MunicipalityId);
            modelInput.StateId = municipality.StateId;
            if (region.Id != entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId)
            {
                entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.Region = region;
                entity.OrganizationIdentity.OrganizationPostalContactItemBridges.FirstOrDefault().ContactItem.RegionId = region.Id;

            }

            if (entity.ContractorKind.Id != modelInput.ContractKindId)
            {
                entity.ContractorKind = contractKindRepository.Get(modelInput.ContractKindId);
            }

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

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Contractor " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PostDelete(Contractor entity)
        {
            base.PostDelete(entity);

            organizationIdentityRepository.Delete(entity.OrganizationIdentity, User.Identity.Name);

            if (entity.OrganizationIdentity.Image == null) return;

            imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.OrganizationIdentity.Image);
        }

        
    }
}
