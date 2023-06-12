namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ArtWorkAwardDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtWorkAward, System.Guid>
    {
        public System.Guid Id { get; set; }

        public AwardDisplayApiModel Award { get; set; }

        public string Context { get; set; }

        public void Import(Persistence.Entities.ArtWorkAward entity)
        {
            Id = entity.Id;
            Context = entity.Context;
            Award = new AwardDisplayApiModel();

            Award.Import(entity.Award);
        }
    }
}
