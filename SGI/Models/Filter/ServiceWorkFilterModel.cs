using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FCBC.SGI.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class ServiceWorkFilterModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<Persistence.Entities.ServiceWork, System.Guid>
    {

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "ServiceLine")]
        public string ManifestationId { get; set; }
        public ICollection<SelectListItem> Manifestation { get; set; }

        [Display(Name = "Author")]
        public string AuthorId { get; set; }
        public ICollection<SelectListItem> Author { get; set; }

        public Expression<System.Func<Persistence.Entities.ServiceWork, bool>> GetSelector()
        {
            Expression<System.Func<Persistence.Entities.ServiceWork, bool>> expression = null;

            if (!string.IsNullOrEmpty(Title))
            {
                expression = entity => entity.Title.ToLower().Contains(Title.ToLower());
            }

            if ((ManifestationId != null) && (ManifestationId != "0"))
            {
                Expression<System.Func<Persistence.Entities.ServiceWork, bool>> ManisfestationExpression = entity => entity.LineId == System.Guid.Parse(ManifestationId);
                if (expression == null)
                {
                    expression = ManisfestationExpression;
                }

            }

            if ((AuthorId != null) && (AuthorId != "0"))
            {
                Expression<System.Func<Persistence.Entities.ServiceWork, bool>> AuthorExpression = entity => entity.CreatorBridges.Any(bridge => bridge.CreatorId == System.Guid.Parse(AuthorId)) ||
                entity.CreationGroupBridges.Any(bridge => bridge.CreationGroupId == System.Guid.Parse(AuthorId));
                if (expression == null)
                {
                    expression = AuthorExpression;
                }
                else
                {
                    var predicate = PredicateBuilder.True<Persistence.Entities.ServiceWork>();

                    expression = predicate.And(AuthorExpression);

                }
            }

            return expression;
        }
    }
}
