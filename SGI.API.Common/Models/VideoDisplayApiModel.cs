namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class VideoDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<SAPRESSC.Middleware.Media.Persistence.Entities.Video, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Extension { get; set; }

        public void Import(SAPRESSC.Middleware.Media.Persistence.Entities.Video entity)
        {
            Id = entity.Id;
        }
    }
}
