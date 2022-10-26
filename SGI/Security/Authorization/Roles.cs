namespace MOROPO.FCBC.SGI.Security.Authorization
{
    public static class Roles
    {
        public const string TraceViewer = "security-trace-viewer";

        #region
        public const string ArtisticManifestationViewer = "artistic-manifestation-viewer";
        public const string ArtisticManifestationCreator = "artistic-manifestation-creator";
        public const string ArtisticManifestationEditor = "artistic-manifestation-editor";
        public const string ArtisticManifestationRemover = "artistic-manifestation-remover";
        #endregion

        #region
        public const string AwardViewer = "award-viewer";
        public const string AwardCreator = "award-creator";
        public const string AwardEditor = "award-editor";
        public const string AwardRemover = "award-remover";
        #endregion
    }
}
