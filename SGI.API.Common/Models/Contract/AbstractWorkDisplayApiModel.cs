namespace MOROPO.FCBC.SGI.API.Common.Models.Contract
{
    public abstract class AbstractWorkDisplayApiModel<TEntity> : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<TEntity, System.Guid> where TEntity : Persistence.Entities.Contract.AbstractWork, new()
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public ImageDisplayApiModel FeatureImage { get; set; }

        public string About { get; set; }

        public CreatorDisplayApiModel[] Creators { get; set; }

        public ThemathicDisplayApiModel[] Themathics { get; set; }

        public MaterialDisplayApiModel[] Materials { get; set; }

        public ImageDisplayApiModel[] Images { get; set; }

        public VideoDisplayApiModel[] Videos { get; set; }

        public ValorationDisplayApiModel[] Valorations { get; set; }

        public virtual void Import(TEntity entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Year = entity.BornYear;
            About = entity.About;
        }
    }
}
