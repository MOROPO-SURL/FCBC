using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ContractorInputViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityInputModel<Contractor, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Identity")]
        public System.Guid IdentityId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Contract Name")]
        public string ContractName { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Contract Type")]
        public string ContractKindName { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "ContractKind")]
        public int ContractKindId { get; set; }

        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> OrgaizacionIdentities { get; set; }
        public System.Collections.Generic.ICollection<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ContractKinds { get; set; }

        public Contractor Export()
        {
            var entity = new Contractor();

            Mergue(entity);

            return entity;
        }

        public void Import(Contractor entity)
        {
            Id = entity.Id;
            IdentityId = entity.IdentityId;
            ContractKindId = entity.ContractorKindId;
        }

        public void Mergue(Contractor entity)
        {
            entity.Identity.Name = ContractName;
            entity.ContractorKind.Name = ContractKindName;
        }
    }
}
