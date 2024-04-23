using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class UnitMeasurementInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.UnitMeasurement, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Name { get; set; }

        [Display(Name = "Symbol")]
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        public string Symbol { get; set; }

        [Display(Name = "MilimetersRatio")]
        public decimal? MilimetersRatio { get; set; }

        [Display(Name = "GramsRatio")]
        public decimal? GramsRatio { get; set; }

        public Persistence.Entities.UnitMeasurement Export()
        {
            var entity = new Persistence.Entities.UnitMeasurement();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.UnitMeasurement entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Symbol = entity.Symbol;
            MilimetersRatio = entity.MilimetersRatio;
            GramsRatio = entity.GramsRatio;
        }

        public void Mergue(Persistence.Entities.UnitMeasurement entity)
        {
            entity.Id = Id;
            entity.Name = Name;
            entity.Symbol = Symbol;
            entity.MilimetersRatio = MilimetersRatio;
            entity.GramsRatio = GramsRatio;
        }
    }
}
