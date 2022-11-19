using MOROPO.FCBC.SGI.Extensions;
using SmartSolucionesCuba.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CriticInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<FBCB.SGI.Persistence.Entities.Critic, System.Guid>
    {
        public Guid Id { get; set; }

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
        public System.DateTime BornDate { get; set; }

        [Display(Name = "Sex")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        [Display(Name = "CivilStatus")]
        public SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        public CriticInputViewModel()
        {
            BornDate = System.DateTime.Now.Date.AddYears(-16);
        }

        public FBCB.SGI.Persistence.Entities.Critic Export()
        {
            var entity = new FBCB.SGI.Persistence.Entities.Critic();

            Mergue(entity);

            return entity;
        }

        public void Import(FBCB.SGI.Persistence.Entities.Critic entity)
        {
            Id = entity.Id;
            ImageId = entity.PersonIdentity.Image?.Id ?? System.Guid.Empty;
            ImageExtension = entity.PersonIdentity.Image?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.PersonIdentity.Name;
            LastName = entity.PersonIdentity.LastName;
            BornDate = entity.PersonIdentity.BornDate;
            Sex = entity.PersonIdentity.Sex;
            CivilStatus = entity.PersonIdentity.CivilStatus;
            Alias = entity.PersonIdentity.Alias;
            About = entity.PersonIdentity.About;
        }

        public void Mergue(FBCB.SGI.Persistence.Entities.Critic entity)
        {
            entity.PersonIdentity.Name = Name;
            entity.PersonIdentity.LastName = LastName;
            entity.PersonIdentity.BornDate = BornDate;
            entity.PersonIdentity.Sex = Sex;
            entity.PersonIdentity.CivilStatus = CivilStatus;
            entity.PersonIdentity.Alias = Alias;
            entity.PersonIdentity.About = About;
        }
    }
}
