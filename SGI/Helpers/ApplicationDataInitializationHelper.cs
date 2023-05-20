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

            #region ApiKey

            if (!permissionDbSet.Any(item => item.Code.Equals(Security.Authorization.Permission.ApiKeyViewer)))
            {
                permissionDbSet.Add(Security.Authorization.Permission.ApiKeyViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ApiKeyManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Security.Authorization.Permission.ApiKeyCreator)))
            {
                permissionDbSet.Add(Security.Authorization.Permission.ApiKeyCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ApiKeyManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Security.Authorization.Permission.ApiKeyEditor)))
            {
                permissionDbSet.Add(Security.Authorization.Permission.ApiKeyEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ApiKeyManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Security.Authorization.Permission.ApiKeyRemover)))
            {
                permissionDbSet.Add(Security.Authorization.Permission.ApiKeyRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ApiKeyManagement));
            }
            #endregion

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
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftManifestationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftManifestationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftManifestationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftManifestationRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftManifestationRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftManifestationManagement));
            }
            #endregion

            #region DesignManifestation
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationViewer)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignManifestationManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationCreator)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignManifestationManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationEditor)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignManifestationManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignManifestationRemover)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignManifestationRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignManifestationManagement));
            //}
            #endregion

            #region ServiceLine
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceLineManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceLineManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceLineManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceLineRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceLineRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceLineManagement));
            }
            #endregion

            #region Award
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AwardViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AwardViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.AwardManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AwardCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AwardCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.AwardManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AwardEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AwardEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.AwardManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AwardRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AwardRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.AwardManagement));
            }
            #endregion

            #region Distinction
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.DistinctionViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DistinctionViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.DistinctionManagement));
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.DistinctionCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DistinctionCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.DistinctionManagement));
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.DistinctionEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DistinctionEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.DistinctionManagement));
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.DistinctionRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.DistinctionRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.DistinctionManagement));
            }
            #endregion

            #region Recognition
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.RecognitionViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.RecognitionViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.RecognitionManagement));
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.RecognitionCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.RecognitionCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.RecognitionManagement));
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.RecognitionEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.RecognitionEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.RecognitionManagement));
            }
            if (!permissionDbSet.AnyAsync(roleDisctinction => roleDisctinction.Code.Equals(Security.Authorization.Permission.RecognitionRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.RecognitionRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.RecognitionManagement));
            }
            #endregion

            #region Service
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceManagement));
            }
            #endregion

            #region Thematic
            if (!permissionDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Permission.ThematicViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ThematicViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ThematicManagement));
            }
            if (!permissionDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Permission.ThematicCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ThematicCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ThematicManagement));
            }
            if (!permissionDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Permission.ThematicEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ThematicEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ThematicManagement));
            }
            if (!permissionDbSet.AnyAsync(roleThematic => roleThematic.Code.Equals(Security.Authorization.Permission.ThematicRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ThematicRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ThematicManagement));
            }
            #endregion

            #region ArtisticTechnique
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticTechniqueViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticTechniqueViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtisticTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticTechniqueCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticTechniqueCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtisticTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticTechniqueEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticTechniqueEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtisticTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtisticTechniqueRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtisticTechniqueRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtisticTechniqueManagement));
            }
            #endregion

            #region CraftTechnique
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftTechniqueViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftTechniqueViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftTechniqueCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftTechniqueCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftTechniqueEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftTechniqueEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftTechniqueRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftTechniqueRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftTechniqueManagement));
            }
            #endregion

            #region DesignTechnique
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignTechniqueViewer)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignTechniqueViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignTechniqueManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignTechniqueCreator)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignTechniqueCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignTechniqueManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignTechniqueEditor)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignTechniqueEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignTechniqueManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignTechniqueRemover)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignTechniqueRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignTechniqueManagement));
            //}
            #endregion

            #region ServiceTechnique
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceTechniqueViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceTechniqueViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceTechniqueCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceTechniqueCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceTechniqueEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceTechniqueEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceTechniqueManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceTechniqueRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceTechniqueRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceTechniqueManagement));
            }
            #endregion

            #region ContractorKind
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorKindViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorKindViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ContractorKindManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorKindCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorKindCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ContractorKindManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorKindEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorKindEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ContractorKindManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorKindRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorKindRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ContractorKindManagement));
            }
            #endregion

            #region Currency
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CurrencyViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CurrencyViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.CurrencyManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CurrencyCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CurrencyCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.CurrencyManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CurrencyEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CurrencyEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.CurrencyManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CurrencyRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CurrencyRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.CurrencyManagement));
            }
            #endregion

            #region Critic
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CriticViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CriticViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.CriticManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CriticCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CriticCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.CriticManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CriticEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CriticEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.CriticManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CriticRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CriticRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.CriticManagement));
            }
            #endregion

            #region Contractor
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ContractorManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ContractorManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ContractorManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ContractorRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ContractorRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ContractorManagement));
            }
            #endregion

            #region Artist
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtistViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtistViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtistManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtistCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtistCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtistManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtistEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtistEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtistManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtistRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtistRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtistManagement));
            }
            #endregion

            #region CreationGroup
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CreationGroupViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CreationGroupViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.CreationGroupManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CreationGroupCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CreationGroupCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.CreationGroupManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CreationGroupEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CreationGroupEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.CreationGroupManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CreationGroupRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CreationGroupRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.CreationGroupManagement));
            }
            #endregion

            #region Association
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AssociationViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AssociationViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.AssociationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AssociationCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AssociationCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.AssociationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AssociationEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AssociationEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.AssociationManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AssociationRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AssociationRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.AssociationManagement));
            }
            #endregion

            #region AdministrativeRole
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AdministrativeRolesViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AdministrativeRolesViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.AdministrativeRolesManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AdministrativeRolesCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AdministrativeRolesCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.AdministrativeRolesManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AdministrativeRolesEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AdministrativeRolesEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.AdministrativeRolesManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AdministrativeRolesRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AdministrativeRolesRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.AdministrativeRolesManagement));
            }
            #endregion

            #region Employee
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.EmployeeViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.EmployeeViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.EmployeeManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.EmployeeCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.EmployeeCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.EmployeeManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.EmployeeEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.EmployeeEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.EmployeeManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.EmployeeRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.EmployeeRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.EmployeeManagement));
            }
            #endregion

            #region Resolution
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ResolutionViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ResolutionViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ResolutionManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ResolutionCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ResolutionCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ResolutionManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ResolutionEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ResolutionEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ResolutionManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ResolutionRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ResolutionRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ResolutionManagement));
            }
            #endregion

            #region ArtWork
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtWorkViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtWorkViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtWorkCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtWorkCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtWorkEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtWorkEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ArtWorkRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ArtWorkRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ArtWorkManagement));
            }
            #endregion

            #region CraftWork
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftWorkViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftWorkViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftWorkCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftWorkCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftWorkEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftWorkEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.CraftWorkRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.CraftWorkRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.CraftWorkManagement));
            }
            #endregion

            #region DesignWork
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignWorkViewer)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignWorkViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignWorkManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignWorkCreator)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignWorkCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignWorkManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignWorkEditor)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignWorkEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignWorkManagement));
            //}
            //if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.DesignWorkRemover)).Result)
            //{
            //    permissionDbSet.Add(Security.Authorization.Permission.DesignWorkRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.DesignWorkManagement));
            //}
            #endregion

            #region UnitMeasurement
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.UnitMeasurementViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.UnitMeasurementViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.UnitMeasurementManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.UnitMeasurementCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.UnitMeasurementCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.UnitMeasurementManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.UnitMeasurementEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.UnitMeasurementEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.UnitMeasurementManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.UnitMeasurementRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.UnitMeasurementRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.UnitMeasurementManagement));
            }
            #endregion

            #region Material
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.MaterialViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.MaterialViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.MaterialManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.MaterialCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.MaterialCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.MaterialManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.MaterialEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.MaterialEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.MaterialManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.MaterialRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.MaterialRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.MaterialManagement));
            }
            #endregion

            #region ServiceWork
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceWorkViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceWorkViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceWorkCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceWorkCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceWorkEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceWorkEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceWorkManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ServiceWorkRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ServiceWorkRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ServiceWorkManagement));
            }
            #endregion

            #region ActivityKind
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ActivityKindViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ActivityKindViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ActivityKindManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ActivityKindCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ActivityKindCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ActivityKindManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ActivityKindEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ActivityKindEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ActivityKindManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ActivityKindRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ActivityKindRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ActivityKindManagement));
            }
            #endregion

            #region Activity
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ActivityViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ActivityViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ActivityManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ActivityCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ActivityCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ActivityManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ActivityEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ActivityEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ActivityManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ActivityRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ActivityRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ActivityManagement));
            }
            #endregion

            #region Agreement
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AgreementViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AgreementViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.AgreementManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AgreementCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AgreementCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.AgreementManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AgreementEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AgreementEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.AgreementManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.AgreementRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.AgreementRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.AgreementManagement));
            }
            #endregion

            #region Exposure
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ExposureViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ExposureViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.ExposureManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ExposureCreator)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ExposureCreator.ToPermissionEntity(Security.Authorization.PermissionsFamily.ExposureManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ExposureEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ExposureEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.ExposureManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.ExposureRemover)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.ExposureRemover.ToPermissionEntity(Security.Authorization.PermissionsFamily.ExposureManagement));
            }
            #endregion

            #region Gallery
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.GalleryViewer)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.GalleryViewer.ToPermissionEntity(Security.Authorization.PermissionsFamily.GalleryManagement));
            }
            if (!permissionDbSet.AnyAsync(role => role.Code.Equals(Security.Authorization.Permission.GalleryEditor)).Result)
            {
                permissionDbSet.Add(Security.Authorization.Permission.GalleryEditor.ToPermissionEntity(Security.Authorization.PermissionsFamily.GalleryManagement));
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

            if (!dbContext.Set<Gallery>().Any())
            {
                dbContext.Set<Gallery>().Add(new Gallery
                {
                    Id = GalleryId.Amalia,
                    Identity = new SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity
                    {
                        Name = "Amalia",
                        About = "No especificado por el momento ..."
                    }
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