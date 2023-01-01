using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AwardInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Award, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Country")]
        public System.Guid? CountryId { get; set; }

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
