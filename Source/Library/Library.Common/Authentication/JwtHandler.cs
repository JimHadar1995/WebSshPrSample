using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Library.Common.Authentication.Models;
using Microsoft.IdentityModel.Tokens;

namespace Library.Common.Authentication
{
    public class JwtHandler : IJwtHandler
    {
        private static readonly ISet<string> DefaultClaims = new HashSet<string>
        {
            JwtRegisteredClaimNames.Sub,
            JwtRegisteredClaimNames.UniqueName,
            JwtRegisteredClaimNames.Jti,
            JwtRegisteredClaimNames.Iat,
            ClaimTypes.Role,
        };

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(JwtOptions options)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = options.Issuer,
                ValidAudience = options.ValidAudience,
                ValidateAudience = options.ValidateAudience,
                ValidateLifetime = options.ValidateLifetime
            };
        }

        public JsonWebTokenPayload? GetTokenPayload(string accessToken)
        {
            _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
                out var validatedSecurityToken);
            if (!(validatedSecurityToken is JwtSecurityToken jwt))
            {
                return null;
            }

            var claims = jwt.Claims.Where(x => !DefaultClaims.Contains(x.Type))
                .ToDictionary(k => k.Type, v => v.Value);

            var roles = claims[CustomClaims.RolesClaim].Split(';');

            var privileges = claims[CustomClaims.PrivilegesClaim].Split(';');

            var needResetPassword = claims.FirstOrDefault(_ => _.Key == nameof(JsonWebTokenPayload.NeedResetPassword)).Value;

            return new JsonWebTokenPayload
            {
                UserId = jwt.Claims.First(_ => _.Type == ClaimTypes.NameIdentifier).Value,
                UserName = jwt.Subject,
                Expires = jwt.ValidTo.ToTimestamp(),
                Claims = claims,
                Roles = roles,
                Privileges = privileges,
                NeedResetPassword = Convert.ToBoolean(needResetPassword),
                TokenId = claims[CustomClaims.TokenIdClaim]
            };
        }

    }
}
