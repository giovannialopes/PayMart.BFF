using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayMart.Application.Core.Utilities;

public static class TakeIdJwt
{
    private static readonly JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

    public static string GetUserIdFromToken(string token)
    {
        var jwtToken = TokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken != null)
        {
            var nameidClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            return nameidClaim!.Value;
        }
        return "";
    }
}
