using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;

namespace MOROPO.FCBC.SGI.Helpers
{
    public sealed class ApplicationDataInitializationHelper : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(DbContext dbContext)
        {
            var roleDbSet = dbContext.Set<Role>();

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.TraceViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.TraceViewer.ToRoleEntity());
            }

            var countryDbSet = dbContext.Set<Country>();

            if (!countryDbSet.AnyAsync().Result)
            {
                Country country = new Country { Name = "Cuba", IsoCode = "3166" };
                countryDbSet.Add(country);

                var stateDbSet = dbContext.Set<State>();
                if (!stateDbSet.AnyAsync().Result)
                {
                    State state = new State { Name = "Camagüey", Country = country, CountryId = country.Id };
                    stateDbSet.Add(state);
                    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    var listMunicipalities = configuration.GetValue<string[]>("Region:Municipalities");
                    var municipalitiesDbSet = dbContext.Set<Municipality>();
                    if (!municipalitiesDbSet.AnyAsync().Result)
                    {
                       foreach(string municipality in listMunicipalities)
                        {
                            municipalitiesDbSet.Add(new Municipality { Name = municipality, State = state, StateId = state.Id });
                        }
                    }
                }
            }


            if (dbContext is SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Context.ITraceableDbContext)
            {
                (dbContext as SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Context.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                dbContext.SaveChanges();
            }
        }
    }
}