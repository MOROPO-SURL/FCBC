using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using MOROPO.FCBC.SGI.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class CraftWorkFilterModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<MOROPO.FCBC.SGI.Persistence.Entities.CraftWork, System.Guid>
    {

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "CraftManifestation")]
        public string ManifestationId { get; set; }
        public ICollection<SelectListItem> Manifestation { get; set; }

        [Display(Name = "Author")]
        public string AuthorId { get; set; }
        public ICollection<SelectListItem> Author { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Persistence.Entities.CraftWork, bool>> GetSelector()
        {
            Expression<System.Func<Persistence.Entities.CraftWork, bool>> expression = null;

            if (!string.IsNullOrEmpty(Title))
            {
                expression = entity => entity.Title.ToLower().Contains(Title.ToLower());
            }

            if((ManifestationId != null)&&(ManifestationId != "0"))
            {
                Expression<System.Func<Persistence.Entities.CraftWork, bool>> ManisfestationExpression = entity => entity.ManifestationId == System.Guid.Parse(ManifestationId);
                if (expression == null)
                {
                    expression = ManisfestationExpression;
                }
                
            }

            if((AuthorId != null)&&(AuthorId != "0"))
            {
                Expression<System.Func<Persistence.Entities.CraftWork, bool>> AuthorExpression = entity => entity.CreatorBridges.Any(bridge => bridge.CreatorId == System.Guid.Parse(AuthorId))||
                entity.CreationGroupBridges.Any(bridge => bridge.CreationGroupId == System.Guid.Parse(AuthorId));
                if (expression == null)
                {
                    expression = AuthorExpression;
                }
                else
                {
                    var predicate = PredicateBuilder.True<Persistence.Entities.CraftWork>();

                    expression = predicate.And(AuthorExpression);

                }
            }

            return expression;
        }
    }
}
