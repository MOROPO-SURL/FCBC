namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ExposureDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Exposure, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public string Description { get; set; }

        public System.DateTime? StartAt { get; set; }

        public System.DateTime? EndAt { get; set; }

        public void Import(Persistence.Entities.Exposure entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            CreatedAt = entity.CreatedAt;
            Description = entity.Description;
            StartAt = entity.StartAt;
            EndAt = entity.EndAt;
        }
    }
}
