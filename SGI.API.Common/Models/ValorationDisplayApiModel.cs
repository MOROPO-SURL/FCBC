namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ValorationDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ArtWorkValoration, System.Guid>
    {
        public System.Guid Id { get; set; }

        public CriticDisplayApiModel Critic { get; set; }

        public string Declaration { get; set; }

        public System.DateTime? DeclaredAt { get; set; }

        public void Import(Persistence.Entities.ArtWorkValoration entity)
        {
            Id = entity.Id;
            Declaration = entity.Declaration;
            DeclaredAt = entity.DeclaredAt;
            Critic = new CriticDisplayApiModel();

            Critic.Import(entity.Critic);
        }
    }
}
