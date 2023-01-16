using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DesignManifestationInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<DesignManifestation, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "DesignManifestationGroup")]
        public System.Guid? GroupId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public string SerializedTree { get; set; }

        public DesignManifestation Export()
        {
            var entity = new DesignManifestation();

            Mergue(entity);

            return entity;
        }

        public void Import(DesignManifestation entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            GroupId = entity.GroupId;
            Description = entity.Description;
        }

        public void Mergue(DesignManifestation entity)
        {
            entity.Name = Name;
            entity.GroupId = GroupId;
            entity.Description = Description;
        }
    }
}
