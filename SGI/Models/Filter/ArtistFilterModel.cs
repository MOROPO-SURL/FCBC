using System.Linq.Expressions;
using System;
using MOROPO.FCBC.SGI.Persistence.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.EF.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.ObjectModel;
using MOROPO.FCBC.SGI.Extensions;

namespace MOROPO.FCBC.SGI.Models.Filter
{
    public class ArtistFilterModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<Artist, System.Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string RegistrationNumber { get; set; }

        public string StateId { get; set; }
        public ICollection<SelectListItem> States { get; set; }

        public string ManisfestationId { get; set; }
        public ICollection<SelectListItem> Manisfestations { get; set; }



        public Expression<Func<Artist, bool>> GetSelector()
        {
            Expression<System.Func<Persistence.Entities.Artist, bool>> expression = null;

            if (!string.IsNullOrEmpty(Name))
            {
                expression = entity => entity.Identity.Name.ToLower().Contains(Name.ToLower());
            }
            else if (!string.IsNullOrEmpty(LastName))
            {
                expression = OrCondition.Combine(expression, entity => entity.Identity.LastName.ToLower().Contains(LastName.ToLower()));
            }            
            else if (!string.IsNullOrEmpty(Alias))
            {
                expression = OrCondition.Combine(expression, entity => entity.Identity.Name.ToLower().Contains(Alias.ToLower()));
            }
            else if (!string.IsNullOrEmpty(RegistrationNumber))
            {
                expression = OrCondition.Combine(expression, entity => entity.RegistrationNumber.ToString().Contains(RegistrationNumber));
            }
                      

            if (StateId != null)
            {
                if (StateId != "0")
                {
                    Expression<System.Func<Persistence.Entities.Artist, bool>> stateExpression = entity => entity.Identity.PersonPostalContactItemBridges.Any(bridge => ((MunicipalityRegion)bridge.ContactItem.Region).Municipality.Id == System.Guid.Parse(StateId));

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


                    Expression<System.Func<Persistence.Entities.Artist, bool>> ManisfestationExpression = entity => entity.ArtisticManifestationBridges.Any(bridge => bridge.ManifestationId == System.Guid.Parse(ManisfestationId)) ||
                        entity.CraftManifestationBridges.Any(bridge => bridge.ManifestationId == System.Guid.Parse(ManisfestationId)) ||
                        entity.DesignManifestationBridges.Any(bridge => bridge.ManifestationId == System.Guid.Parse(ManisfestationId)) ||
                        entity.ServiceLineBridges.Any(bridge => bridge.LineId == System.Guid.Parse(ManisfestationId));

                    if (expression == null)
                    {
                        expression = ManisfestationExpression;
                    }
                    else
                    {
                        var predicate = PredicateBuilder.True<Artist>();

                        expression = predicate.And(ManisfestationExpression);
                            
                    }
                }
            }
            return expression;
        }
    }
}
