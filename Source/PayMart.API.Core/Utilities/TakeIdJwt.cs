using PayMart.Domain.Core.Request.Login;
using PayMart.Domain.Core.Response.Login;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayMart.API.Core.Utilities;

public static class TakeIdJwt
{
    private static readonly JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

    public static string GetUserIdFromToken(string token)
    {
        var jwtToken = TokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken != null)
        {
            var nameidClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            return nameidClaim.Value;
        }
        return "";
    }

    public static RequestRegisterUserLogin GetAllFromToken(string token)
    {
        var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

        if (jwtToken != null)
        {
            var request = new RequestRegisterUserLogin();

            foreach (var claim in jwtToken.Claims)
            {
                switch (claim.Type)
                {
                    case "email":
                        request.Email = claim.Value;
                        break;
                    case "unique_name":
                        request.Name = claim.Value;
                        break;
                }
            }   
            return request;
        }

        return new RequestRegisterUserLogin();
    }

        
    
}
