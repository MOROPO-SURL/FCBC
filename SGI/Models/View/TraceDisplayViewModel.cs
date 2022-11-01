using MOROPO.FBCB.SGI.Persistence.Entities;
using SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    [Microsoft.AspNetCore.Mvc.ModelMetadataType(typeof(SmartSolucionesCuba.SAPRESSC.Core.Persistence.EF.Entities.Trace))]
    public class TraceDisplayViewModel : SmartSolucionesCuba.SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Trace, long>
    {
        [Key]
        public long Id { get; set; }

        public Microsoft.EntityFrameworkCore.EntityState Type { get; set; }

        public string Author { get; set; }

        [Required]
        public DateTime Registered { get; private set; }

        [Required]
        public string EntityName { get; set; }

        [Required]
        public string BeforeData { get; set; }

        public string AfterData { get; set; }

        [Required]
        public long NextId { get; set; }
        

        public void Import(Trace entity)
        {
            var objectTrace = entity.GetSignableEntityModel();

            Id = (long)objectTrace.GetType().GetProperty("Id").GetValue(objectTrace);

            Type = (Microsoft.EntityFrameworkCore.EntityState)objectTrace.GetType().GetProperty("Type").GetValue(objectTrace);

            Author = (string)objectTrace.GetType().GetProperty("Author").GetValue(objectTrace);

            Registered = (DateTime)objectTrace.GetType().GetProperty("Registered").GetValue(objectTrace);

            EntityName = (string)objectTrace.GetType().GetProperty("EntityName").GetValue(objectTrace);

            BeforeData = (string)objectTrace.GetType().GetProperty("BeforeData").GetValue(objectTrace);

            AfterData = (string)objectTrace.GetType().GetProperty("AfterData").GetValue(objectTrace);

            NextId = (long)objectTrace.GetType().GetProperty("NextId").GetValue(objectTrace);
            
        }

    }
}
