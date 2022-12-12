namespace MOROPO.FCBC.SGI.Persistence.EF.Repositories
{
    public class InstitutionInformationRepository : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<FBCB.SGI.Persistence.Entities.InstitutionInformation, FBCB.SGI.Persistence.Enumerations.InstitutionInformationKind>
    {
        public InstitutionInformationRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
