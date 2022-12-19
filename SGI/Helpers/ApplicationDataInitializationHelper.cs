using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
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

            #region InstitutionalInformation
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.InstitutionalInformationViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.InstitutionalInformationViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.InstitutionalInformationEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.InstitutionalInformationEditor.ToRoleEntity());
            }
            #endregion

            #region ArtisticManifestation
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationViewer.ToRoleEntity());
            }
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

            #region CraftManifestation
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftManifestationViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftManifestationViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftManifestationCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftManifestationCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftManifestationEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftManifestationEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftManifestationRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftManifestationRemover.ToRoleEntity());
            }
            #endregion

            #region DesignManifestation
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignManifestationViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignManifestationViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignManifestationCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignManifestationCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignManifestationEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignManifestationEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignManifestationRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignManifestationRemover.ToRoleEntity());
            }
            #endregion

            #region ServiceLine
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceLineViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceLineViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceLineCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceLineCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceLineEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceLineEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceLineRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceLineRemover.ToRoleEntity());
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

            #region ArtisticTechnique
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticTechniqueViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticTechniqueViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticTechniqueCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticTechniqueCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticTechniqueEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticTechniqueEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticTechniqueRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticTechniqueRemover.ToRoleEntity());
            }
            #endregion

            #region CraftTechnique
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftTechniqueViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftTechniqueViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftTechniqueCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftTechniqueCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftTechniqueEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftTechniqueEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftTechniqueRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftTechniqueRemover.ToRoleEntity());
            }
            #endregion

            #region DesignTechnique
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignTechniqueViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignTechniqueViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignTechniqueCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignTechniqueCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignTechniqueEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignTechniqueEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignTechniqueRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignTechniqueRemover.ToRoleEntity());
            }
            #endregion

            #region ServiceTechnique
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceTechniqueViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceTechniqueViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceTechniqueCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceTechniqueCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceTechniqueEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceTechniqueEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceTechniqueRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceTechniqueRemover.ToRoleEntity());
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

            #region Resolution
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ResolutionViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ResolutionViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ResolutionCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ResolutionCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ResolutionEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ResolutionEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ResolutionRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ResolutionRemover.ToRoleEntity());
            }
            #endregion

            #region ArtisticManifestationGroup
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationGroupViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationGroupViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationGroupCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationGroupCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationGroupEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationGroupEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtisticManifestationGroupRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtisticManifestationGroupRemover.ToRoleEntity());
            }
            #endregion

            #region CraftManifestationGroup
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftManifestationGroupViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftManifestationGroupViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftManifestationGroupCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftManifestationGroupCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftManifestationGroupEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftManifestationGroupEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftManifestationGroupRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftManifestationGroupRemover.ToRoleEntity());
            }
            #endregion

            #region DesignManifestationGroup
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignManifestationGroupViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignManifestationGroupViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignManifestationGroupCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignManifestationGroupCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignManifestationGroupEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignManifestationGroupEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignManifestationGroupRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignManifestationGroupRemover.ToRoleEntity());
            }
            #endregion

            #region ServiceLineGroup
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceLineGroupViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceLineGroupViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceLineGroupCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceLineGroupCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceLineGroupEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceLineGroupEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceLineGroupRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceLineGroupRemover.ToRoleEntity());
            }
            #endregion

            #region ArtWork
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtWorkViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtWorkViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtWorkCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtWorkCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtWorkEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtWorkEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ArtWorkRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ArtWorkRemover.ToRoleEntity());
            }
            #endregion

            #region CraftWork
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftWorkViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftWorkViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftWorkCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftWorkCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftWorkEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftWorkEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.CraftWorkRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.CraftWorkRemover.ToRoleEntity());
            }
            #endregion

            #region DesignWork
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignWorkViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignWorkViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignWorkCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignWorkCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignWorkEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignWorkEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.DesignWorkRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.DesignWorkRemover.ToRoleEntity());
            }
            #endregion

            #region UnitMeasurement
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.UnitMeasurementViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.UnitMeasurementViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.UnitMeasurementCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.UnitMeasurementCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.UnitMeasurementEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.UnitMeasurementEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.UnitMeasurementRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.UnitMeasurementRemover.ToRoleEntity());
            }
            #endregion

            #region Material
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.MaterialViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.MaterialViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.MaterialCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.MaterialCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.MaterialEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.MaterialEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.MaterialRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.MaterialRemover.ToRoleEntity());
            }
            #endregion

            #region ServiceWork
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceWorkViewer)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceWorkViewer.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceWorkCreator)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceWorkCreator.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceWorkEditor)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceWorkEditor.ToRoleEntity());
            }
            if (!roleDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Roles.ServiceWorkRemover)).Result)
            {
                roleDbSet.Add(Security.Authorization.Roles.ServiceWorkRemover.ToRoleEntity());
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

            var institutionInformationDbSet = dbContext.Set<InstitutionalInformation>();

            if (!institutionInformationDbSet.Any(entity => entity.Id == InstitutionalInformationKind.SocialObject))
            {
                institutionInformationDbSet.Add(new InstitutionalInformation
                {
                    Id = InstitutionalInformationKind.SocialObject,
                    Value = "Objeto social no especificado ..."
                });
            }

            if (!institutionInformationDbSet.Any(entity => entity.Id == InstitutionalInformationKind.Mission))
            {
                institutionInformationDbSet.Add(new InstitutionalInformation
                {
                    Id = InstitutionalInformationKind.Mission,
                    Value = "Misión no especificada ..."
                });
            }

            if (!institutionInformationDbSet.Any(entity => entity.Id == InstitutionalInformationKind.Vision))
            {
                institutionInformationDbSet.Add(new InstitutionalInformation
                {
                    Id = InstitutionalInformationKind.Vision,
                    Value = "Visión no especificada ..."
                });
            }

            if (!institutionInformationDbSet.Any(entity => entity.Id == InstitutionalInformationKind.History))
            {
                institutionInformationDbSet.Add(new InstitutionalInformation
                {
                    Id = InstitutionalInformationKind.History,
                    Value = "Historia no especificada ..."
                });
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