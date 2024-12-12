using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AIR_Wheelly_Common.Interfaces;

namespace AIR_Wheelly_BLL.Helpers;

public static class JwtHelper{
    public  static  string GetUserIdFromJwt(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadToken(jwtToken) as JwtSecurityToken;
        if (token == null)
        {
            throw new ArgumentException("Invalid token");
        }
        var userIdClaims = token.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaims == null)
        {
            throw new ArgumentNullException("No id claim found in jwt token");
        }

        return userIdClaims.Value;
    }
}