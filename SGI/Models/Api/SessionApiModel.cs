using System.Linq;

namespace MOROPO.FCBC.SGI.Models.Api
{
    public class SessionApiModel : SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Models.IApiKeySessionModel
    {
        public SessionApiModel(Persistence.Entities.ApiKey entity)
        {
            ApiKey = entity;

            var claims = new System.Collections.Generic.List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.AuthenticationMethod, SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)
            };

            foreach (var bridge in entity.PermissionBridges)
            {
                claims.AddRange(entity.PermissionBridges.Select(item => new System.Security.Claims.Claim(System.Security.Claims.ClaimsIdentity.DefaultRoleClaimType, item.Permission.Code)).ToArray());
            }

            Claims = claims;
        }

        public string Key => ApiKey.Token;

        public string OwnerName => ApiKey.Id.ToString();

        public System.Collections.Generic.IReadOnlyCollection<System.Security.Claims.Claim> Claims { get; private set; }

        public Persistence.Entities.ApiKey ApiKey { get; private set; }
    }
}
