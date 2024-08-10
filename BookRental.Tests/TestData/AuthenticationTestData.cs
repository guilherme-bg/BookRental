using BookRental.Server.Models.ViewModels;

namespace BookRentalTests.TestData
{
    public class AuthenticationTestData
    {
        public static LoginViewModel ValidLoginViewModel()
        {
            return new LoginViewModel { UserName = "username", Password = "password" };
        }

        public static RegisterUserViewModel ValidRegiserUserViewModel()
        {
            return new RegisterUserViewModel { UserName = "user", Password = "password", ConfirmPassword = "password" };
        }

    }
}
