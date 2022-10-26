namespace MOROPO.FCBC.SGI.Security.Authorization
{
    public static class Roles
    {
        public const string TraceViewer = "security-trace-viewer";

        #region ArtisticManifestation
        public const string ArtisticManifestationViewer = "artistic-manifestation-viewer";
        public const string ArtisticManifestationCreator = "artistic-manifestation-creator";
        public const string ArtisticManifestationEditor = "artistic-manifestation-editor";
        public const string ArtisticManifestationRemover = "artistic-manifestation-remover";
        #endregion

        #region Distinction
        public const string DistinctionCreator = "distinction-creator";
        public const string DistinctionEditor = "distinction-editor";
        public const string DistinctionViewer = "distinction-viewer";
        public const string DistinctionRemover = "distinction-remover";
        #endregion
    }
}
