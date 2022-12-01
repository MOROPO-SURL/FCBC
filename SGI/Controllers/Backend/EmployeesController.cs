using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.FBCB.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Models.Filter;
using MOROPO.FCBC.SGI.Models.View;
using MOROPO.FCBC.SGI.Persistence.EF.Repositories;
using MOROPO.FCBC.SGI.Security.Authorization;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.Repositories;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Backend.Controllers;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Security.Filters;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.EF.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.FCBC.SGI.Controllers.Backend
{
    [Authorize]
    [Area("institution")]
    public class EmployeesController : AbstractEntityManagementFilteredController<Employee, System.Guid, EmployeeInputViewModel, EmployeeDisplayViewModel, EmployeeFilterModel>
    {
        private readonly IImageFileManagerFacade imageFileManagerFacade;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<PersonIdentity, System.Guid> identityRepository;
        private readonly IEntityRepository<AdministrativeRole, System.Guid> administrativeRoleRepository;
       
        public EmployeesController(IEntityRepository<PersonIdentity, System.Guid> identityRepository, IEntityRepository<Image, System.Guid> imageRepository, IImageFileManagerFacade imageFileManagerFacade, IEntityRepository<Employee,System.Guid> repository, IEntityRepository<AdministrativeRole, System.Guid> administrativeRoleRepository, IStringLocalizer<EmployeesController> localizer, ILogger<EmployeesController> logger) : base(repository, localizer, logger)
        {
            this.imageFileManagerFacade = imageFileManagerFacade;
            this.imageRepository = imageRepository;
            this.identityRepository = identityRepository;
            this.administrativeRoleRepository = administrativeRoleRepository;
        }

        [RequiresRoleBasePermission(Roles.EmployeeViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequiresRoleBasePermission(Roles.CriticViewer)]
        public override IActionResult Filter(EmployeeFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "employee-management-filter";
        }

        [RequiresRoleBasePermission(Roles.EmployeeCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequiresRoleBasePermission(Roles.EmployeeCreator)]
        public override IActionResult Create(EmployeeInputViewModel inputViewModel)
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

        [RequiresRoleBasePermission(Roles.EmployeeEditor)]
        public override IActionResult Edit( System.Guid key)
        {
            return base.Edit(key);
        }

        [RequiresRoleBasePermission(Roles.EmployeeEditor)]
        public override IActionResult Edit(EmployeeInputViewModel inputViewModel)
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

        [RequiresRoleBasePermission(Roles.EmployeeRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequiresRoleBasePermission(Roles.EmployeeRemover)]
        public override IActionResult Delete(EmployeeDisplayViewModel displayViewModel)
        {
            return base.Delete(displayViewModel);
        }

        protected override void PreCreate(Employee entity, EmployeeInputViewModel inputViewModel)
        {
            base.PreCreate(entity, inputViewModel);

            foreach (var admRolId in inputViewModel.SelectedValues)
            {
                AdministrativeRole admRole = this.administrativeRoleRepository.GetAll().FirstOrDefault(r => r.Id == admRolId);
                AdministrativeRoleInputViewModel administrativeRoleInputViewModel = new AdministrativeRoleInputViewModel
                {
                    Id = admRolId,
                    Name = admRole.Name,
                    Description = admRole.Description
                };
                var EmployeeAdministrativeRoleBridge = new EmployeeAdministrativeRoleBridge { EmployeeId = inputViewModel.Id, AdministrativeRoleId = admRolId, AdministrativeRole = administrativeRoleInputViewModel.Export(), Employee = inputViewModel.Export() };
                entity.EmployeeAdministrativeRoleBridges.Add(EmployeeAdministrativeRoleBridge);
                
            }            

            if (inputViewModel.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                inputViewModel.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = inputViewModel.ImageFile.ContentType, Length = inputViewModel.ImageFile.Length, About = "Critic " + entity.Id };

                imageFileManagerFacade.SaveImageFileContent(image.Id, memoryStream.ToArray(), inputViewModel.ImageFile.ContentType);

                entity.PersonIdentity.Image = image;
            }
        }

        protected override void PreEdit(Employee entity, EmployeeInputViewModel inputViewModel)
        {
            base.PreEdit(entity, inputViewModel);

            entity.EmployeeAdministrativeRoleBridges.Clear();

            foreach (var admRolId in inputViewModel.SelectedValues)
            {
                AdministrativeRole admRole = this.administrativeRoleRepository.GetAll().FirstOrDefault(r => r.Id == admRolId);
                AdministrativeRoleInputViewModel administrativeRoleInputViewModel = new AdministrativeRoleInputViewModel
                {
                    Id = admRolId,
                    Name = admRole.Name,
                    Description = admRole.Description
                };
                var EmployeeAdministrativeRoleBridge = new EmployeeAdministrativeRoleBridge { EmployeeId = inputViewModel.Id, AdministrativeRoleId = admRolId, AdministrativeRole = administrativeRoleInputViewModel.Export(), Employee = inputViewModel.Export() };
                entity.EmployeeAdministrativeRoleBridges.Add(EmployeeAdministrativeRoleBridge);

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

        protected override void PostDelete(Employee entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.PersonIdentity, User.Identity.Name);

            if (entity.PersonIdentity.Image == null) return;

            imageRepository.Delete(entity.PersonIdentity.Image, User.Identity.Name);

            imageFileManagerFacade.DeleteImageFile(entity.PersonIdentity.Image);
        }

        private void PopulateWithAdministrativeRole(EmployeeInputViewModel modelInput)
        {
            List<SelectListItem> employeesItems = administrativeRoleRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name }).ToList();
            modelInput.AdministrativeRoles = new MultiSelectList(employeesItems.OrderBy(i => i.Text), "Value", "Text", modelInput.SelectedValues);
        }

        private void PopulateWithAdministrativeRoleEdit(EmployeeInputViewModel modelInput)
        {
            var SelectedValues = modelInput.SelectedValues;

            List<SelectListItem> employeesItems = administrativeRoleRepository.GetAll().Select(entity => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = entity.Id.ToString(), Text = entity.Name, Selected = SelectedValues.Any(s=>s == entity.Id) }).ToList();
            modelInput.AdministrativeRoles = new MultiSelectList(employeesItems.OrderBy(i => i.Text), "Value", "Text", modelInput.SelectedValues);
        }

        protected override void ConfigureCreate(EmployeeInputViewModel modelInput)
        {
            PopulateWithAdministrativeRole(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(EmployeeInputViewModel modelInput)
        {
            PopulateWithAdministrativeRole(modelInput);
        }

        protected override void ConfigureEdit(Employee entity, EmployeeInputViewModel modelInput)
        {
            PopulateWithAdministrativeRoleEdit(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(EmployeeInputViewModel modelInput)
        {
            PopulateWithAdministrativeRole(modelInput);
        }
    }
}
