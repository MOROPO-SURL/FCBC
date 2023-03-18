using MOROPO.FCBC.SGI.Persistence.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class UnitMeasurementInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<UnitMeasurement, System.Guid>
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Symbol")]
        public string Symbol { get; set; }

        public UnitMeasurement Export()
        {
            var entity = new UnitMeasurement();

            Mergue(entity);

            return entity;
        }

        public void Import(UnitMeasurement entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Symbol = entity.Symbol;
        }

        public void Mergue(UnitMeasurement entity)
        {
            entity.Id = Id;
            entity.Name = Name;
            entity.Symbol = Symbol;
        }
    }
}
