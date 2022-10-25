using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.EF;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MOROPO.FCBC.SGI.Extensions
{
    public static class ApplicationDbContextExtensions
    {
        public static void createRegions<TDbContext>(this TDbContext target, string[] municipalitiesNames) where TDbContext : DbContext
        {
            var countryDbSet = target.Set<Country>();
            Country country = new Country { Name = "Cuba", IsoCode = "3166" };
            countryDbSet.Add(country);
            var stateDbSet = target.Set<State>();
            State state = new State { Name = "Camagüey", Country = country, CountryId = country.Id };
            stateDbSet.Add(state);
            var municipalitiesDbSet = target.Set<Municipality>();
            foreach(string municipalityName in municipalitiesNames)
            {
                municipalitiesDbSet.Add(new Municipality { Name = municipalityName, State = state, StateId = state.Id });
            }
            target.SaveChanges();
        }
    }
}
