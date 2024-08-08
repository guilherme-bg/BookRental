using BookRental.Server.Helpers;
using BookRental.Server.Models.ViewModels;
using BookRental.Server.Services.Interfaces;

namespace BookRental.Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<ServiceResult> LoginAsync(LoginViewModel loginViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> RegisterUserAsync(RegisterUserViewModel bookRequest)
        {
            throw new NotImplementedException();
        }
    }
}
