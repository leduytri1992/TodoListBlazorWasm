using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Api.Entities;
using TodoList.Models;

namespace TodoList.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public LoginController(IConfiguration configuration,
							   SignInManager<User> signInManager,
							   UserManager<User> userInManager)
		{
			_configuration = configuration;
			_signInManager = signInManager;
			_userManager = userInManager;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginRequest login)
		{
			var user = await _userManager.FindByNameAsync(login.UserName);
			if (user == null)
			{
				return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });
			}

			var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded)
            {
				return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });
            }

			var claims = new[]
			{
				new Claim(ClaimTypes.Name, login.UserName),
				new Claim("UserId", user.Id.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expire = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpireInDays"]));

			var token = new JwtSecurityToken(
				_configuration["JwtIssuer"],
				_configuration["JwtAudience"],
				claims,
				expires: expire,
				signingCredentials: creds
			);

            return Ok(new LoginResponse { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
		}
	}
}
