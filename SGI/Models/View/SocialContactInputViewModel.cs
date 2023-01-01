using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class SocialContactInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<SocialContactItem, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Url(ErrorMessage = "UrlValidationErrorMessage")]
        [Display(Name = "Url")]
        public string SocialId { get; set; }

        public SocialContactItem Export()
        {
            var entity = new SocialContactItem();

            Mergue(entity);

            return entity;
        }

        public void Import(SocialContactItem entity)
        {
            Label = entity.Label;
            SocialId = entity.SocialId;
        }

        public void Mergue(SocialContactItem entity)
        {
            entity.Label = Label;
            entity.SocialId = SocialId;
        }
    }
}
