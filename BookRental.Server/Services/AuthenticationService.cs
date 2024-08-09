using BookRental.Server.Data;
using BookRental.Server.Helpers;
using BookRental.Server.Models;
using BookRental.Server.Models.Responses;
using BookRental.Server.Models.UI;
using BookRental.Server.Models.ViewModels;
using BookRental.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookRental.Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JWTCredentialsSettings _jwtSettings;

        public AuthenticationService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, JWTCredentialsSettings jwtSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtSettings = jwtSettings;
        }

        public async Task<ServiceResult<string>> LoginAsync(LoginViewModel loginViewModel)
        {
            var result = await ValidateUserAsync(loginViewModel);

            if (!result.IsSuccess)
            {
                return ServiceResult<string>.Failure(result.ErrorMessage);
            }

            var generateToken = GenerateToken();

            if (generateToken.Data is null)
            {
                return ServiceResult<string>.Failure(result.ErrorMessage);
            }

            return ServiceResult<string>.Success(generateToken.Data);
        }

        public async Task<ServiceResult> RegisterUserAsync(RegisterUserViewModel userViewModel)
        {
            var user = new User
            {
                UserName = userViewModel.UserName,
                PasswordHash = userViewModel.Password,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, userViewModel.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ServiceResult.Success();
        }

        private async Task<ServiceResult> ValidateUserAsync(LoginViewModel loginViewModel)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginViewModel.UserName);

            if (user is null)
            {
                return ServiceResult.Failure(Constants.USER_NOT_FOUND_ERROR);
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginViewModel.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return ServiceResult.Failure(Constants.INVALID_PASSWORD_ERROR);
            }

            return ServiceResult.Success();
        }

        private ServiceResult<string> GenerateToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: [],
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            if (tokenString.IsNullOrEmpty())
            {
                return ServiceResult<string>.Failure(Constants.UNABLE_TO_GENERATE_TOKEN);
            }

            return ServiceResult<string>.Success(tokenString);
        }
    }
}
