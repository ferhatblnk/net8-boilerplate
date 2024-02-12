using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        string CreateToken(TUser user);
        string GetClaim(string token, ClaimNames claimType);

        bool Validate(string token);
        bool Verify(TUser user);
    }
}
