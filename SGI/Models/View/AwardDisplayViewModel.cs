namespace MOROPO.FCBC.SGI.Models.View
{
    public class AwardDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Award, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        //[Display(Name = "Country")]
        //public CountryDisplayViewModel Country { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public void Import(Persistence.Entities.Award entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;

            //Country.Import(entity.Country);
        }
    }
}
