using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    [Microsoft.AspNetCore.Mvc.ModelMetadataType(typeof(SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace))]
    public class TraceDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace, long>
    {
        [Key]
        public long Id { get; set; }

        public Microsoft.EntityFrameworkCore.EntityState Type { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Registered")]
        public System.DateTime Registered { get; private set; }

        [Required]
        [Display(Name = "EntityName")]
        public string EntityName { get; set; }

        [Required]
        [Display(Name = "BeforeData")]
        public string BeforeData { get; set; }

        [Display(Name = "AfterData")]
        public string AfterData { get; set; }        

        public void Import(SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace entity)
        {
            Id = entity.Id;
            Type = entity.Type;
            Author = entity.Author;
            Registered = entity.Registered;
            EntityName = entity.EntityName;
            BeforeData = entity.BeforeData;
            AfterData = entity.AfterData;            
        }
    }
}
