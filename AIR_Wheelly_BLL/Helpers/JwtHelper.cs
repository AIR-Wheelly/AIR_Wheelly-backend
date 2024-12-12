using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AIR_Wheelly_Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AIR_Wheelly_BLL.Helpers;

public class JwtHelper
{
    private readonly string _jwtKey;
    private readonly string _audience;
    private readonly string _issuer;
    public JwtHelper(IConfiguration configuration)
    {
        _jwtKey = configuration["JWT:Key"] ?? throw new Exception("Missing JWT configuration");
        _audience = configuration["JWT:Audience"] ?? throw new Exception("Missing JWT configuration");
        ;
        _issuer = configuration["JWT:Issuer"] ?? throw new Exception("Missing JWT configuration");
        ;
    }

    public string GetUserIdFromJwt(string jwtToken)
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

    public string GenerateJwtToken(int Id)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _issuer,
            Audience = _audience,
            Subject = new ClaimsIdentity(new[] { new Claim("id", Id.ToString()) }),
            Expires = DateTime.Now.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);

    }
}