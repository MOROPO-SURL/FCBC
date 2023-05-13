namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class ImageDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Extension { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public void Import(SAPRESSC.Middleware.Media.Persistence.Entities.Image entity)
        {
            Id = entity.Id;
            Width = entity.Width;
            Height = entity.Height;
        }
    }
}
