using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.RegionConst;
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
        private const string SS_STATE_NAME = "Sancti Spíritus";

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

            #region Currency
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CurrencyViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CurrencyViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CurrencyCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CurrencyCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CurrencyEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CurrencyEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CurrencyRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CurrencyRemover.ToRoleEntity());
            }
            #endregion

            #region Critic
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CriticViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CriticViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CriticCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CriticCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CriticEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CriticEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CriticRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CriticRemover.ToRoleEntity());
            }
            #endregion

            #region Contractor
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ContractorViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ContractorViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ContractorCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ContractorCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ContractorEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ContractorEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ContractorRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ContractorRemover.ToRoleEntity());
            }
            #endregion

            #region Artist
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtistViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtistViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtistCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtistCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtistEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtistEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtistRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtistRemover.ToRoleEntity());
            }
            #endregion

            #region CreationGroup
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CreationGroupViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CreationGroupViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CreationGroupCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CreationGroupCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CreationGroupEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CreationGroupEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CreationGroupRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CreationGroupRemover.ToRoleEntity());
            }
            #endregion

            #region Association
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AssociationViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AssociationViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AssociationCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AssociationCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AssociationEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AssociationEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AssociationRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AssociationRemover.ToRoleEntity());
            }
            #endregion

            #region AdministrativeRole
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AdministrativeRolesViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AdministrativeRolesViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AdministrativeRolesCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AdministrativeRolesCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AdministrativeRolesEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AdministrativeRolesEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.AdministrativeRolesRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.AdministrativeRolesRemover.ToRoleEntity());
            }
            #endregion

            #region Employee
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.EmployeeViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployeeViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.EmployeeCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployeeCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.EmployeeEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployeeEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.EmployeeRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.EmployeeRemover.ToRoleEntity());
            }
            #endregion
                        

            var countryDbSet = dbContext.Set<Country>();
            var stateDbSet = dbContext.Set<State>();
            var municipalitiesDbSet = dbContext.Set<Municipality>();

            var country = countryDbSet.FirstOrDefault(e => e.Name.Equals(CUBA_COUNTRY_NAME));

            if (country == null)
            {
                country = new Country { Name = CUBA_COUNTRY_NAME, IsoCode = "cu", Id = RegionInitConstant.MY_COUNTRY };

                countryDbSet.Add(country);
            }

            var state = stateDbSet.FirstOrDefault(e => e.Name.Equals(CMG_STATE_NAME));
            var stateSS = stateDbSet.FirstOrDefault(e => e.Name.Equals(SS_STATE_NAME));

            if (state == null)
            {
                state = new State { Name = "Camagüey", CountryId = RegionInitConstant.MY_COUNTRY, Id = RegionInitConstant.MY_OWN_SATE };

                stateDbSet.Add(state);
            }

            if (stateSS == null)
            {
                stateSS = new State { Name = "Sancti Spíritus", CountryId = RegionInitConstant.MY_COUNTRY, Id = RegionInitConstant.TEST_SATE };

                stateDbSet.Add(stateSS);
            }

            var cmgMunicipalitiesNames = new string[] { "Camagüey", "Florida", "Guáimaro", "Vertientes", "Santa Cruz del Sur", "Nuevitas", "Minas", "Sibanicú", "Esmeralda", "Carlos M. de Cespedes", "Jimaguayú", "Sierra de Cubitas", "Najasa" };
            var ssMunicipalitiesNames = new string[] { "Sancti Spíritus" };

            foreach (string municipalityName in cmgMunicipalitiesNames)
            {
                if (municipalitiesDbSet.Any(e => e.Name.Equals(municipalityName))) continue;

                municipalitiesDbSet.Add(new Municipality { Name = municipalityName, State = state, StateId = state.Id });
            }

            foreach (string municipalityName in ssMunicipalitiesNames)
            {
                if (municipalitiesDbSet.Any(e => e.Name.Equals(municipalityName))) continue;

                municipalitiesDbSet.Add(new Municipality { Name = municipalityName, State = state, StateId = stateSS.Id });
            }

            var municipalityRegionDbSet = dbContext.Set<MunicipalityRegion>();
            foreach (var currentMunicipality in municipalitiesDbSet)
            {
                var municipalityRegion = municipalityRegionDbSet.FirstOrDefault(r => r.MunicipalityId == currentMunicipality.Id);
                if(municipalityRegion == null)
                {
                    municipalityRegion = new MunicipalityRegion { MunicipalityId = currentMunicipality.Id, Municipality = currentMunicipality };
                    municipalityRegionDbSet.Add(municipalityRegion);
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