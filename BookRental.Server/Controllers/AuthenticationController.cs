using BookRental.Server.Models;
using BookRental.Server.Models.ViewModels;
using BookRental.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;


        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

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
