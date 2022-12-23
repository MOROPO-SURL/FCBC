using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class MaterialDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Material, System.Guid>
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public void Import(Material entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
