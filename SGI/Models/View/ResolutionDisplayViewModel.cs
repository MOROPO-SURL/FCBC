namespace MOROPO.FCBC.SGI.Models.View
{
    public class ResolutionDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Resolution, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int NumberResolution { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public System.DateTime ResolutionDate { get; set; }

        public void Import(Persistence.Entities.Resolution entity)
        {
            Id = entity.Id;
            NumberResolution = entity.Number;
            Title = entity.Title;
            Description = entity.Description;
            ResolutionDate = entity.ReleaseAt;
        }
    }
}
