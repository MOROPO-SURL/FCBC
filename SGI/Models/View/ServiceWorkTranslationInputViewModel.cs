using MOROPO.FCBC.SGI.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class ServiceWorkTranslationInputViewModel: LanguagesInputViewModel
    {       
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}
