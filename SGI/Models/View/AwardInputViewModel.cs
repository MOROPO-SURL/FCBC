using MOROPO.FBCB.SGI.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AwardInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<Award, Guid>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Country")]
        public System.Guid CountryId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Countries { get; set; }

        public Award Export()
        {
            var entity = new Award();

            Mergue(entity);

            return entity;
        }

        public void Import(Award entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            CountryId = entity.Country.Id;
        }

        public void Mergue(Award entity)
        {
            entity.Name = Name;
        }
    }
}
