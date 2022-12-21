using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ArtistInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.Artist, System.Guid>
    {
        public System.Guid Id { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 3145728, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public bool ResetImage { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "BornDate")]
        public System.DateTime BornDate { get; set; } = System.DateTime.Now.Date.AddYears(-16);

        [Display(Name = "Sex")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Display(Name = "CivilStatus")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        [Display(Name = "Stage name")]
        public string Alias { get; set; }

        [Display(Name = "Observations")]
        public string About { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Address")]
        public string AddressPostal { get; set; }

        public string Context { get; set; }

        public Persistence.Enumerations.AdmitionMode AdmitionModel { get; set; }        
        
        public System.Guid? AssociationId { get; set; }

        public ICollection<SelectListItem> Associations { get; set; } = new List<SelectListItem>();

        public System.Guid? ResolutionId { get; set; }

        public ICollection<SelectListItem> Resolutions { get; set; } = new List<SelectListItem>();
        
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "State")]
        public System.Guid StateId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Municipality")]
        public System.Guid MunicipalityId { get; set; }

        public State State { get; set; }

        public Municipality Municipality { get; set; }

        public ICollection<SelectListItem> States { get; set; } = new List<SelectListItem>();

        public ICollection<SelectListItem> Municipalities { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [RegularExpression("[0-9]{11}")]
        [Display(Name = "CitizenId")]
        public string CitizenId { get; set; }

        public Persistence.Enumerations.WorkPlace Workplace { get; set; }

        public Persistence.Enumerations.SchoolLevel SchoolLevel { get; set; }

        [Display(Name = "specify training")]
        public string SpecificEducation { get; set; }

        [Display(Name = "Número de registro del creador")]
        public int RegistrationNumber { get; set; }

        [Display(Name = "Fecha en que expira")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public System.DateTime? RegistrationEndAt { get; set; }

        public ArtistInputViewModel()
        {
            BornDate = System.DateTime.Now.AddYears(-16);
        }
        
        public Persistence.Entities.Artist Export()
        {
            var entity = new Persistence.Entities.Artist();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Artist entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            BornDate = entity.Identity.BornDate;
            Sex = entity.Identity.Sex;
            CivilStatus = entity.Identity.CivilStatus;
            Alias = entity.Identity.Alias;
            About = entity.Identity.About;
            AddressPostal = entity.Identity.PersonPostalContactItemBridges.Select(b => b.ContactItem.Address).First();
            RegistrationNumber = entity.RegistrationNumber;
            RegistrationEndAt = entity.RegistrationEndAt;
            
            var region = entity.Identity.PersonPostalContactItemBridges.FirstOrDefault().ContactItem.Region;
            
            MunicipalityId = (System.Guid)((MunicipalityRegion)region).MunicipalityId;
            CitizenId = entity.CitizenId;
            Workplace = entity.Workplace;
            SchoolLevel = entity.SchoolLevel;
            SpecificEducation = entity.SpecificEducation;
            AdmitionModel = entity.AdmitionMode;
            Context = entity.AdmitionModeContext;            
        }

        public void Mergue(Persistence.Entities.Artist entity)
        {
            entity.Identity.Name = Name;
            entity.Identity.LastName = LastName;
            entity.Identity.BornDate = BornDate;
            entity.Identity.Sex = Sex;
            entity.Identity.CivilStatus = CivilStatus;
            entity.Identity.Alias = Alias;
            entity.Identity.About = About;
            entity.CitizenId = CitizenId;
            entity.Workplace = Workplace;
            entity.SpecificEducation = SpecificEducation;
            entity.SchoolLevel = SchoolLevel;
            entity.AdmitionMode = AdmitionModel;
            entity.AdmitionModeContext = Context;
            entity.RegistrationNumber = RegistrationNumber;
            entity.RegistrationEndAt = RegistrationEndAt;
        }
    }
}
