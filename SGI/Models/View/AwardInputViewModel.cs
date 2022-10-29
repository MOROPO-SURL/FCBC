using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AwardInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<FBCB.SGI.Persistence.Entities.Award, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Country")]
        public System.Guid? CountryId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Countries { get; set; }

        public FBCB.SGI.Persistence.Entities.Award Export()
        {
            var entity = new FBCB.SGI.Persistence.Entities.Award();

            Mergue(entity);

            return entity;
        }

        public void Import(FBCB.SGI.Persistence.Entities.Award entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            CountryId = entity.Country.Id;
        }

        public void Mergue(FBCB.SGI.Persistence.Entities.Award entity)
        {
            entity.Name = Name;
        }
    }
}
