using Microsoft.AspNetCore.Mvc.Rendering;
using MOROPO.FCBC.SGI.Extensions;
using MOROPO.FCBC.SGI.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class CreationGroupFilterModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<CreationGroup, Guid>
    {
        public string Name { get; set; }

        public string RegistrationNumber { get; set; }

        public string StateId { get; set; }
        public ICollection<SelectListItem> States { get; set; }

        public string ManisfestationId { get; set; }
        public ICollection<SelectListItem> Manisfestations { get; set; }

        public Expression<Func<CreationGroup, bool>> GetSelector()
        {
            Expression<Func<CreationGroup, bool>> expression = null;

            if (!string.IsNullOrEmpty(Name))
            {
                expression = entity => entity.Identity.Name.ToLower().Contains(Name.ToLower());
            }
            else if (!string.IsNullOrEmpty(RegistrationNumber))
            {
                expression = OrCondition.Combine(expression, entity => entity.RegistrationNumber.ToString().Contains(RegistrationNumber));
            }

            if (StateId != null)
            {
                if (StateId != "0")
                {
                    Expression<Func<CreationGroup, bool>> stateExpression = entity => entity.Identity.OrganizationPostalContactItemBridges.Any(bridge => ((MunicipalityRegion)bridge.ContactItem.Region).Municipality.Id == Guid.Parse(StateId));

                    if (expression == null)
                    {
                        expression = OrCondition.Combine(expression, stateExpression);
                    }
                }
            }

            if (ManisfestationId != null)
            {
                if (ManisfestationId != "0")
                {


                    Expression<Func<CreationGroup, bool>> ManisfestationExpression = entity => entity.ArtisticManifestationBrigdes.Any(bridge => bridge.ManifestationId == Guid.Parse(ManisfestationId)) ||
                        entity.CraftManifestationBridges.Any(bridge => bridge.ManifestationId == Guid.Parse(ManisfestationId)) ||
                        entity.DesignManifestationBridges.Any(bridge => bridge.ManifestationId == Guid.Parse(ManisfestationId)) ||
                        entity.ServiceLineBridges.Any(bridge => bridge.LineId == Guid.Parse(ManisfestationId));

                    if (expression == null)
                    {
                        expression = ManisfestationExpression;
                    }
                    else
                    {
                        var predicate = PredicateBuilder.True<CreationGroup>();

                        expression = predicate.And(ManisfestationExpression);

                    }
                }
            }

            return expression;
        }
    }
}
