namespace MOROPO.FCBC.SGI.API.Common.Models
{
    public class CraftWorkDisplayApiModel : Contract.AbstractWorkDisplayApiModel<Persistence.Entities.CraftWork>
    {
        public CraftWorkManifestationDisplayApiModel Manifestation { get; set; }

        public CraftWorkTechniqueDisplayApiModel[] Techniques { get; set; }

        public override void Import(Persistence.Entities.CraftWork entity)
        {
            base.Import(entity);

            Manifestation = new CraftWorkManifestationDisplayApiModel();

            Manifestation.Import(entity.Manifestation);
        }
    }
}
