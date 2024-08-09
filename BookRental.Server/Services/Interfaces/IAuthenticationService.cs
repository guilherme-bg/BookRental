using BookRental.Server.Helpers;
using BookRental.Server.Models.ViewModels;

namespace BookRental.Server.Services.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// This method registers a new user.
        /// </summary>
        Task<ServiceResult> RegisterUserAsync(RegisterUserViewModel bookRequest);

        /// <summary>
        /// This method checks credentials to return a jwt to access other features.
        /// </summary>
        Task<ServiceResult<string>> LoginAsync(LoginViewModel loginViewModel);
    }
}
