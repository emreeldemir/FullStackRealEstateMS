using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstate.API.Context;
using RealEstate.API.DTOs.User;
using RealEstate.API.Entities.Identity;
using RealEstate.API.Models;
using System.Collections.Generic;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDTO userRegisterRequestDTO)
        {
            // Check if a user with the same email or username already exists
            var existingUserByEmail = await _userManager.FindByEmailAsync(userRegisterRequestDTO.Email);
            var existingUserByUsername = await _userManager.FindByNameAsync(userRegisterRequestDTO.UserName);

            if (existingUserByEmail != null)
            {
                return BadRequest(new { Message = "This email already exists!" });
            }

            if (existingUserByUsername != null)
            {
                return BadRequest(new { Message = "This username already exists!" });
            }

            var user = new ApplicationUser
            {
                FirstName = userRegisterRequestDTO.FirstName,
                LastName = userRegisterRequestDTO.LastName,
                UserName = userRegisterRequestDTO.UserName,
                Email = userRegisterRequestDTO.Email,
            };

            var result = await _userManager.CreateAsync(user, userRegisterRequestDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "User");
            return Ok(new { Message = "User Created Successfully!" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO userLoginRequestDTO)
        {
            ApplicationUser user;

            // Check if the input is an email or a username
            if (userLoginRequestDTO.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(userLoginRequestDTO.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(userLoginRequestDTO.UserNameOrEmail);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginRequestDTO.Password))
            {
                return Unauthorized(new { Message = "Invalid username/email or password." });
            }

            var token = await generateToken(user);
            return Ok(new { Token = token });
        }





        //[AllowAnonymous]
        //[HttpPost("Login")]
        //public IActionResult Login([FromBody] ApiUser apiUserInfos)
        //{

        //    var apiUser = Authenticate(apiUserInfos);
        //    if (apiUser == null) return NotFound("User can not found!");

        //    var token = generateToken(apiUser);
        //    return Ok(token);

        //}

        private async Task<string> generateToken(ApplicationUser applicationUser)
        {
            if (_jwtSettings.Key == null) throw new Exception("Jwt key can not be null!");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Token içine yazacağım kullanıcı infolarına claim diyorum, claim e hassas bilgi yazmamalıyız çünkü decode edilince görülebilir
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, applicationUser.UserName!),
                new Claim(ClaimTypes.Name, applicationUser.UserName!)
            };

            var roles = await _userManager.GetRolesAsync(applicationUser);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //private ApiUser Authenticate(ApiUser apiUserInfos)
        //{
        //    var hasher = new PasswordHasher<ApplicationUser>();
        //    var hashedPassword = hasher.HashPassword(null, apiUserInfos.Password);
        //    var user = _context.Users.FirstOrDefault(x =>
        //        apiUserInfos.UserName == x.UserName
        //        && hashedPassword == x.PasswordHash);

        //    if (user == null)
        //        return null;


        //    var roleId = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id).RoleId;
        //    var role = _context.Roles.FirstOrDefault(x => x.Id == roleId).Name;

        //    return new ApiUser
        //    {
        //        UserName = user?.UserName,
        //        Role = role
        //    };

        //    return ApiUsers
        //        .Users
        //        .FirstOrDefault(x =>
        //            x.UserName?.ToLower() == apiUserInfos.UserName
        //            && x.Password == apiUserInfos.Password

        //        );


        //}
    }
}
