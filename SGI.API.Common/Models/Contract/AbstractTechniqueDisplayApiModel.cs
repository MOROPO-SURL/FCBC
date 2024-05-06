namespace MOROPO.FCBC.SGI.API.Common.Models.Contract
{
    public abstract class AbstractTechniqueDisplayApiModel
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
