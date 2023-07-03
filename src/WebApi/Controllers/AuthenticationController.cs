using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers;

[Route("api/v{verion:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }

    public record AuthenticationData(string? UserName, string? Password);       

    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromForm] AuthenticationData data)
    {
        // TODO: Validate user
        // ...
      

        //generate token
        string token = GenerateToken();

        return Ok(token);
    }

    [HttpPost("refresh")]
    public ActionResult<string> RefreshToken()
    {
        // TODO: Validate the token, check expiration, etc.
        // ...

        // If the token is valid, generate a new access token with an updated expiration
        var newAccessToken = GenerateToken();

        // Return the new access token to the client
        return Ok(newAccessToken);
    }

    private string GenerateToken()
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                _config.GetValue<string>("Authentication:SecretKey")));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new();

        // TODO: Add user claims, userID, userName, userSurname itp etc.
        // ...

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(5),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
