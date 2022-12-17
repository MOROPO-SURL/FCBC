using MOROPO.FCBC.SGI.Persistence.Entities;
using System;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class UnitMeasurementDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<UnitMeasurement, System.Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public void Import(UnitMeasurement entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Symbol = entity.Symbol;
        }
    }
}
