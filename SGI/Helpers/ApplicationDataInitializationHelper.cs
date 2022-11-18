using Microsoft.EntityFrameworkCore;
using SmartSolucionesCuba.SAPRESSC.Core.Security.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Web.SecurityLocal.Extensions;
using SmartSolucionesCuba.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Helpers
{
    public sealed class ApplicationDataInitializationHelper : SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper
    {
        private const string CUBA_COUNTRY_NAME = "Cuba";
        private const string CMG_STATE_NAME = "Camagüey";

        public void InitialiseData(DbContext dbContext)
        {
            var roleDbSet = dbContext.Set<Role>();

            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.TraceViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.TraceViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationViewer.ToRoleEntity());
            }
            #region ArtisticManifestation
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationRemover.ToRoleEntity());
            }
            #endregion

            #region Award
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AwardViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AwardCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AwardEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AwardRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AwardRemover.ToRoleEntity());
            }
            #endregion

            #region Distinction
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.DistinctionViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DistinctionViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.DistinctionCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DistinctionCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.DistinctionEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DistinctionEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.DistinctionRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DistinctionRemover.ToRoleEntity());
            }
            #endregion

            #region Recognition
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.RecognitionViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.RecognitionViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.RecognitionCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.RecognitionCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.RecognitionEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.RecognitionEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Roles.RecognitionRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.RecognitionRemover.ToRoleEntity());
            }
            #endregion

            #region Service
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceRemover.ToRoleEntity());
            }
            #endregion

            #region Thematic
            if (!roleDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Roles.ThematicViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ThematicViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Roles.ThematicCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ThematicCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Roles.ThematicEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ThematicEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Roles.ThematicRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ThematicRemover.ToRoleEntity());
            }
            #endregion

            #region Technique
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.TechniqueViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.TechniqueViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.TechniqueCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.TechniqueCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.TechniqueEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.TechniqueEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.TechniqueRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.TechniqueRemover.ToRoleEntity());
            }
            #endregion

            #region ContractorKind
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ContractorKindViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ContractorKindViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ContractorKindCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ContractorKindCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ContractorKindEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ContractorKindEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ContractorKindRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ContractorKindRemover.ToRoleEntity());
            }
            #endregion

            var countryDbSet = dbContext.Set<Country>();
            var stateDbSet = dbContext.Set<State>();
            var municipalitiesDbSet = dbContext.Set<Municipality>();

            var country = countryDbSet.FirstOrDefault(e => e.Name.Equals(CUBA_COUNTRY_NAME));

            if (country == null)
            {
                country = new Country { Name = CUBA_COUNTRY_NAME, IsoCode = "cu" };

                countryDbSet.Add(country);
            }

            var state = stateDbSet.FirstOrDefault(e => e.Name.Equals(CMG_STATE_NAME));

            if (state == null)
            {
                state = new State { Name = "Camagüey", CountryId = country.Id };

                stateDbSet.Add(state);
            }

            var cmgMunicipalitiesNames = new string[] { "Camagüey", "Florida", "Guáimaro", "Vertientes", "Santa Cruz del Sur", "Nuevitas", "Minas", "Sibanicú", "Esmeralda", "Carlos M. de Cespedes", "Jimaguayú", "Sierra de Cubitas", "Najasa" };

            foreach (string municipalityName in cmgMunicipalitiesNames)
            {
                if (municipalitiesDbSet.Any(e => e.Name.Equals(municipalityName))) continue;

                municipalitiesDbSet.Add(new Municipality { Name = municipalityName, State = state, StateId = state.Id });
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