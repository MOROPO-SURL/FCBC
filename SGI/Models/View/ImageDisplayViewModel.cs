using MOROPO.FCBC.SGI.Extensions;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ImageDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Extension { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public void Import(SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence.Entities.Image entity)
        {
            Id = entity.Id;
            Extension = entity.ContentType.ContentTypeToExtension();
            Width = entity.Width;
            Height = entity.Height;
        }
    }
}
