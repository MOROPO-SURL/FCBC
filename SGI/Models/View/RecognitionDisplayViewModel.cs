using MOROPO.FBCB.SGI.Persistence.Entities;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class RecognitionDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Recognition, Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Import(Recognition entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
