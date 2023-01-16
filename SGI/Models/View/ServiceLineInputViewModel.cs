using MOROPO.FCBC.SGI.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceLineInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<ServiceLine, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "ServiceLineGroup")]
        public System.Guid? GroupId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public string SerializedTree { get; set; }

        public ServiceLine Export()
        {
            var entity = new ServiceLine();

            Mergue(entity);

            return entity;
        }

        public void Import(ServiceLine entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            GroupId = entity.GroupId;
            Description = entity.Description;
        }

        public void Mergue(ServiceLine entity)
        {
            entity.Name = Name;
            entity.GroupId = GroupId;
            entity.Description = Description;
        }
    }
}
