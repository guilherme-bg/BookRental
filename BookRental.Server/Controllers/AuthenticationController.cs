using BookRental.Server.Models;
using BookRental.Server.Models.UI;
using BookRental.Server.Models.ViewModels;
using BookRental.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookRental.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JWTCredentialsSettings _jwtSettings;
        private readonly IAuthenticationService _authenticationService;


        public AuthenticationController(IOptions<JWTCredentialsSettings> jwtSettings, IAuthenticationService authenticationService)
        {
            _jwtSettings = jwtSettings.Value;
            _authenticationService = authenticationService;
        }

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginViewModel user)
        //{
        //    if (user is null)
        //    {
        //        return BadRequest("Invalid user request!!!");
        //    }
        //    if (user.UserName == "Johndoe" && user.Password == "40028922")
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        //        var tokeOptions = new JwtSecurityToken(
        //            issuer: _jwtSettings.ValidIssuer,
        //            audience: _jwtSettings.ValidAudience,
        //            claims: new List<Claim>(),
        //            expires: DateTime.Now.AddMinutes(6),
        //            signingCredentials: signinCredentials
        //        );
        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        return Ok(new JWTResponse { Token = tokenString });
        //    }
        //    return Unauthorized();
        //}

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authenticationService.LoginAsync(user);

            if (!result.IsSuccess)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _authenticationService.RegisterUserAsync(user);

            return Ok(Constants.USER_SUCCESSFULLY_REGISTERED_MESSAGE);
        }
    }
}
