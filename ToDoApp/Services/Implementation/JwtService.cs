using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.Db.Entities;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Implementation;

public class JwtService : IJwtService
{
    private readonly string _signature;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IConfiguration conf, IHttpContextAccessor httpContextAccessor)
    {
        _signature = conf.GetValue<string>("Jwt:Signature")
            ?? throw new ArgumentNullException("Jwt signature is null");

        _issuer = conf.GetValue<string>("Jwt:Issuer")
            ?? throw new ArgumentNullException("Jwt issuer is null");

        _audience = conf.GetValue<string>("Jwt:Audience")
            ?? throw new ArgumentNullException("Jwt audience is null");

        _httpContextAccessor = httpContextAccessor;

    }

    public string GenerateToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email),
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityTokenDescriptor descriptor = new()
        {
            Audience = _audience,
            Issuer = _issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signature)),
                SecurityAlgorithms.HmacSha256),

            Expires = DateTime.UtcNow.AddDays(10),
            Subject = new ClaimsIdentity(claims)
        };

        JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(descriptor);
        return tokenHandler.WriteToken(token);
    }


    public Guid GetCurrentUserId()
    {
        var claim = _httpContextAccessor.HttpContext?.User
           .FindFirst("id");

        if (claim == null)
            throw new UnauthorizedAccessException("User not authenticated");

        return Guid.Parse(claim.Value);
    }
}
