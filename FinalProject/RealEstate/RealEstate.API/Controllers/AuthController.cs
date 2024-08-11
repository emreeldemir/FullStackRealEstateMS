using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstate.API.Context;
using RealEstate.API.DTOs.User;
using RealEstate.API.Entities.Identity;
using RealEstate.API.Models;
using RealEstate.API.Services;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(IOptions<JwtSettings> jwtSettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
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

            // Kullanıcı adı ya da email kontrolü
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

            // Token, Username ve Email'in döndürülmesi
            return Ok(new
            {
                Token = token,
                Username = user.UserName,
                Email = user.Email
            });
        }



        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            var userInfo = new
            {
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName
            };

            return Ok(userInfo);
        }


        [HttpPut("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoRequestDTO updateUserInfoRequestDTO)
        {
            var user = await _userManager.FindByIdAsync(updateUserInfoRequestDTO.UserId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found!" });
            }

            // Check if new UserName is already taken (only if it's being changed)
            if (!string.IsNullOrEmpty(updateUserInfoRequestDTO.UserName) &&
                !updateUserInfoRequestDTO.UserName.Equals(user.UserName) &&
                await _userManager.FindByNameAsync(updateUserInfoRequestDTO.UserName) != null)
            {
                return BadRequest(new { Message = "Username is already taken!" });
            }

            // Check if new Email is already used (only if it's being changed)
            if (!string.IsNullOrEmpty(updateUserInfoRequestDTO.Email) &&
                !updateUserInfoRequestDTO.Email.Equals(user.Email) &&
                await _userManager.FindByEmailAsync(updateUserInfoRequestDTO.Email) != null)
            {
                return BadRequest(new { Message = "Email is already in use!" });
            }

            // Update fields
            if (!string.IsNullOrEmpty(updateUserInfoRequestDTO.UserName))
            {
                user.UserName = updateUserInfoRequestDTO.UserName;
            }
            if (!string.IsNullOrEmpty(updateUserInfoRequestDTO.Email))
            {
                user.Email = updateUserInfoRequestDTO.Email;
            }
            if (!string.IsNullOrEmpty(updateUserInfoRequestDTO.NewPassword))
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, updateUserInfoRequestDTO.NewPassword);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult.Errors);
            }

            // Create response DTO
            var responseDTO = new UpdateUserInfoResponseDTO
            {
                UserName = user.UserName,
                Email = user.Email
                // Note: Password is not returned in the response
            };

            return Ok(responseDTO);
        }




        //[HttpPost("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO forgotPasswordRequestDTO)
        //{
        //    var user = await _userManager.FindByEmailAsync(forgotPasswordRequestDTO.Email);
        //    if (user == null)
        //    {
        //        return NotFound(new { Message = "User not found." });
        //    }

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    var resetLink = Url.Action("ResetPassword", "Account", new { token, email = forgotPasswordRequestDTO.Email }, Request.Scheme);

        //    var emailService = new EmailService("smtp.gmail.com", 587, "your-email@gmail.com", "your-email-password");
        //    var subject = "Password Reset Request";
        //    var body = $"Please reset your password by clicking <a href='{resetLink}'>here</a>.";

        //    await emailService.SendEmailAsync(user.Email, subject, body);

        //    return Ok(new { Message = "Password reset link has been sent to your email." });
        //}



        


        //[Authorize(Roles = "Admin")]
        //[HttpGet("GetAllUsers")]
        //public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        //{
        //    var users = _userManager.Users
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .Select(user => new
        //        {
        //            user.Id,
        //            user.UserName,
        //            user.Email,
        //            user.FirstName,
        //            user.LastName
        //        })
        //        .ToList();

        //    return Ok(users);
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


    }
}
