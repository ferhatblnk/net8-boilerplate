using Core.Extensions;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Business.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private TokenOptions _tokenOptions;

        public JwtHelper(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public string CreateToken(TUser user)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            var claims = new List<Claim>
            {
                new Claim(ClaimNames.Id.Description(), user.Id.ToString()),
                new Claim(ClaimNames.Email.Description(), user.Email),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public bool Validate(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidAudience = _tokenOptions.Audience,
                    IssuerSigningKey = mySecurityKey,
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public string GetClaim(string token, ClaimNames claimType)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType.Description()).Value;
                return stringClaimValue;
            }
            catch
            {
                return "";
            }
        }

        public bool Verify(TUser user)
        {
            return user != null &&
                    !string.IsNullOrWhiteSpace(user.Token?.Trim()) &&
                    user.TokenExpiredAt.HasValue &&
                    user.TokenExpiredAt.Value >= System.DateTime.Now;
        }
    }
}
