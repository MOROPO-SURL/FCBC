using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.FCBC.SGI.Models.View
{
    public class CreationGroupSocialContactInputViewModel : SocialContactInputViewModel
    {
        public System.Guid CreationGroupId { get; set; }
    }
}

