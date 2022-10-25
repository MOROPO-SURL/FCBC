using Microsoft.EntityFrameworkCore;
using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories;
using System;

namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class ArtisticManifestationRepository : AbstractEntityRepository<ArtisticManifestation, Guid>
    {
        public ArtisticManifestationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<ArtisticManifestation, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
