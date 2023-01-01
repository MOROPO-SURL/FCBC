using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Web.Backend.Models.View;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class AwardDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Award, Guid>
    {
        public AwardDisplayViewModel()
        {
            Country = new CountryDisplayViewModel();
        }

        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Country")]
        public CountryDisplayViewModel Country { get; set; }

        public void Import(Award entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            Country.Import(entity.Country);
        }
    }
}
