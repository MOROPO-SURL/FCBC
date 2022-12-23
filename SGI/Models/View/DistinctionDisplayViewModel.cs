using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class DistinctionDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Distinction, Guid>
    {
        public Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        public void Import(Distinction entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
