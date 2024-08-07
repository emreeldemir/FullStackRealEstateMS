using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstate.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Admin")]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }


        //[AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] ApiUser apiUserInfos)
        {
            var apiUser = Authenticate(apiUserInfos);
            if (apiUser == null) return NotFound("User can not found!");

            var token = generateToken(apiUser);
            return Ok(token);
        }

        private string generateToken(ApiUser apiUser)
        {
            if (_jwtSettings.Key == null) throw new Exception("Jwt key can not be null!");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Token içine yazacağım kullanıcı infolarına claim diyorum, claim e hassas bilgi yazmamalıyız çünkü decode edilince görülebilir
            var claimArray = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, apiUser.UserName!),
                new Claim(ClaimTypes.Role, apiUser.Role!)
            };

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimArray,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ApiUser? Authenticate(ApiUser apiUserInfos)
        {
            return ApiUsers
                .Users
                .FirstOrDefault(x => 
                    x.UserName?.ToLower() == apiUserInfos.UserName
                    && x.Password == apiUserInfos.Password
                 
                );
        }
    }
}
