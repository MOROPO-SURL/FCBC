using Microsoft.EntityFrameworkCore;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.FCBC.SGI.Persistence.Enumerations;
using MOROPO.FCBC.SGI.RegionConst;
using MOROPO.SAPRESSC.Core.Security.Common.Persistence.EF.Extensions;
using MOROPO.SAPRESSC.Core.Security.Common.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System.Linq;

namespace MOROPO.FCBC.SGI.Helpers
{
    public sealed class ApplicationDataInitializationHelper : SAPRESSC.Core.Persistence.EF.Helpers.Facade.IDataInitializationHelper
    {
        private const string CUBA_COUNTRY_NAME = "Cuba";
        private const string CMG_STATE_NAME = "Camagüey";
        private const string SS_STATE_NAME = "Sancti Spíritus";

        public void InitialiseData(DbContext dbContext)
        {
            var permissionDbSet = dbContext.Set<Permission>();

            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.TraceViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.TraceViewer.ToPermissionEntity());
            }

            #region InstitutionalInformation
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.InstitutionalInformationViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.InstitutionalInformationViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.InstitutionalInformationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.InstitutionalInformationEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.InstitutionalInformationEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.InstitutionalInformationManagement));
            }
            #endregion

            #region ArtisticManifestation
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticManifestationViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticManifestationViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtisticManifestationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticManifestationCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticManifestationCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtisticManifestationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticManifestationEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticManifestationEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtisticManifestationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticManifestationRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticManifestationRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtisticManifestationManagement));
            }
            #endregion

            #region CraftManifestation
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationRemover.ToPermissionEntity());
            }
            #endregion

            #region DesignManifestation
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationRemover.ToPermissionEntity());
            }
            #endregion

            #region ServiceLine
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineRemover.ToPermissionEntity());
            }
            #endregion

            #region Award
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AwardViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AwardViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AwardCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AwardCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AwardEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AwardEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AwardRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AwardRemover.ToPermissionEntity());
            }
            #endregion

            #region Distinction
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.DistinctionViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DistinctionViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.DistinctionCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DistinctionCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.DistinctionEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DistinctionEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.DistinctionRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DistinctionRemover.ToPermissionEntity());
            }
            #endregion

            #region Recognition
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.RecognitionViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.RecognitionViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.RecognitionCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.RecognitionCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.RecognitionEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.RecognitionEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.RecognitionRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.RecognitionRemover.ToPermissionEntity());
            }
            #endregion

            #region Service
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceRemover.ToPermissionEntity());
            }
            #endregion

            #region Thematic
            if (!permissionDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Permission.ThematicViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ThematicViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Permission.ThematicCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ThematicCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Permission.ThematicEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ThematicEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Permission.ThematicRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ThematicRemover.ToPermissionEntity());
            }
            #endregion

            #region ArtisticTechnique
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticTechniqueViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticTechniqueViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticTechniqueCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticTechniqueCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticTechniqueEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticTechniqueEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticTechniqueRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticTechniqueRemover.ToPermissionEntity());
            }
            #endregion

            #region CraftTechnique
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftTechniqueViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftTechniqueViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftTechniqueCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftTechniqueCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftTechniqueEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftTechniqueEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftTechniqueRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftTechniqueRemover.ToPermissionEntity());
            }
            #endregion

            #region DesignTechnique
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignTechniqueViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignTechniqueViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignTechniqueCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignTechniqueCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignTechniqueEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignTechniqueEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignTechniqueRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignTechniqueRemover.ToPermissionEntity());
            }
            #endregion

            #region ServiceTechnique
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceTechniqueViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceTechniqueViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceTechniqueCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceTechniqueCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceTechniqueEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceTechniqueEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceTechniqueRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceTechniqueRemover.ToPermissionEntity());
            }
            #endregion

            #region ContractorKind
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorKindViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorKindViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorKindCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorKindCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorKindEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorKindEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorKindRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorKindRemover.ToPermissionEntity());
            }
            #endregion

            #region Currency
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CurrencyViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CurrencyViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CurrencyCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CurrencyCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CurrencyEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CurrencyEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CurrencyRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CurrencyRemover.ToPermissionEntity());
            }
            #endregion

            #region Critic
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CriticViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CriticViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CriticCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CriticCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CriticEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CriticEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CriticRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CriticRemover.ToPermissionEntity());
            }
            #endregion

            #region Contractor
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorRemover.ToPermissionEntity());
            }
            #endregion

            #region Artist
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtistViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtistViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtistCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtistCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtistEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtistEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtistRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtistRemover.ToPermissionEntity());
            }
            #endregion

            #region CreationGroup
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CreationGroupViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CreationGroupViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CreationGroupCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CreationGroupCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CreationGroupEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CreationGroupEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CreationGroupRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CreationGroupRemover.ToPermissionEntity());
            }
            #endregion

            #region Association
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AssociationViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AssociationViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AssociationCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AssociationCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AssociationEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AssociationEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AssociationRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AssociationRemover.ToPermissionEntity());
            }
            #endregion

            #region AdministrativeRole
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AdministrativeRolesViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AdministrativeRolesViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AdministrativeRolesCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AdministrativeRolesCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AdministrativeRolesEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AdministrativeRolesEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AdministrativeRolesRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AdministrativeRolesRemover.ToPermissionEntity());
            }
            #endregion

            #region Employee
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.EmployeeViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.EmployeeViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.EmployeeCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.EmployeeCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.EmployeeEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.EmployeeEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.EmployeeRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.EmployeeRemover.ToPermissionEntity());
            }
            #endregion

            #region Resolution
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ResolutionViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ResolutionViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ResolutionCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ResolutionCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ResolutionEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ResolutionEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ResolutionRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ResolutionRemover.ToPermissionEntity());
            }
            #endregion

            #region ArtisticManifestationGroup
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticManifestationGroupViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticManifestationGroupViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticManifestationGroupCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticManifestationGroupCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticManifestationGroupEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticManifestationGroupEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticManifestationGroupRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticManifestationGroupRemover.ToPermissionEntity());
            }
            #endregion

            #region CraftManifestationGroup
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationGroupViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationGroupViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationGroupCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationGroupCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationGroupEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationGroupEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationGroupRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationGroupRemover.ToPermissionEntity());
            }
            #endregion

            #region DesignManifestationGroup
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationGroupViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationGroupViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationGroupCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationGroupCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationGroupEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationGroupEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationGroupRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationGroupRemover.ToPermissionEntity());
            }
            #endregion

            #region ServiceLineGroup
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineGroupViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineGroupViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineGroupCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineGroupCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineGroupEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineGroupEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineGroupRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineGroupRemover.ToPermissionEntity());
            }
            #endregion

            #region ArtWork
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtWorkViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtWorkViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtWorkCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtWorkCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtWorkEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtWorkEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtWorkRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtWorkRemover.ToPermissionEntity());
            }
            #endregion

            #region CraftWork
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftWorkViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftWorkViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftWorkCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftWorkCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftWorkEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftWorkEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftWorkRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftWorkRemover.ToPermissionEntity());
            }
            #endregion

            #region DesignWork
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignWorkViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignWorkViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignWorkCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignWorkCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignWorkEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignWorkEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignWorkRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DesignWorkRemover.ToPermissionEntity());
            }
            #endregion

            #region UnitMeasurement
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.UnitMeasurementViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.UnitMeasurementViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.UnitMeasurementCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.UnitMeasurementCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.UnitMeasurementEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.UnitMeasurementEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.UnitMeasurementRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.UnitMeasurementRemover.ToPermissionEntity());
            }
            #endregion

            #region Material
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.MaterialViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.MaterialViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.MaterialCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.MaterialCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.MaterialEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.MaterialEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.MaterialRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.MaterialRemover.ToPermissionEntity());
            }
            #endregion

            #region ServiceWork
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceWorkViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceWorkViewer.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceWorkCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceWorkCreator.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceWorkEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceWorkEditor.ToPermissionEntity());
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceWorkRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceWorkRemover.ToPermissionEntity());
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

            if (dbContext is SAPRESSC.Core.Persistence.EF.Context.ITraceableDbContext)
            {
                (dbContext as SAPRESSC.Core.Persistence.EF.Context.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                dbContext.SaveChanges();
            }
        }
    }
}