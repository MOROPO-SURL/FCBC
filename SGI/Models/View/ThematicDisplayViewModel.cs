using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ThematicDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Thematic, System.Guid>
    {
        public System.Guid Id { get ; set ; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public void Import(Thematic entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
