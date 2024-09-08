using PayMart.Domain.Core.Model;
using System.IdentityModel.Tokens.Jwt;
using static PayMart.Domain.Core.Model.ModelLogin;

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
            return nameidClaim!.Value;
        }
        return "";
    }

    public static ModelLogin.RequestUserLogin GetAllFromToken(string token)
    {
        var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

        if (jwtToken != null)
        {
            var request = new RequestUserLogin();

            foreach (var claim in jwtToken.Claims)
            {
                switch (claim.Type)
                {
                    case "email":
                        request.Email = claim.Value;
                        break;
                }
            }   
            return request;
        }

        return new RequestUserLogin();
    }

        
    
}
