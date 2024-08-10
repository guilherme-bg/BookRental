using BookRental.Server.Controllers;
using BookRental.Server.Helpers;
using BookRental.Server.Models;
using BookRental.Server.Models.ViewModels;
using BookRental.Server.Services.Interfaces;
using BookRentalTests.TestData;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace BookRentalTests.Controller
{
    public class AuthenticationControllerTests
    {
        private readonly IAuthenticationService _authenticationService;

        private readonly AuthenticationController _authenticationController;


        public AuthenticationControllerTests()
        {
            _authenticationService = Substitute.For<IAuthenticationService>();
            _authenticationController = new AuthenticationController(_authenticationService);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnStatusCode500_WhenLoginFails()
        {
            // Arrange
            var loginViewModel = AuthenticationTestData.ValidLoginViewModel();

            _authenticationService.LoginAsync(loginViewModel).Returns(ServiceResult<string>.Failure(Constants.INVALID_PASSWORD_ERROR));

            // Act
            var result = await _authenticationController.LoginAsync(loginViewModel);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal(Constants.INVALID_PASSWORD_ERROR, statusCodeResult.Value);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnOk_WhenLoginSucceeds()
        {
            // Arrange
            var loginViewModel = AuthenticationTestData.ValidLoginViewModel();
            var token = "valid-token";
            _authenticationService.LoginAsync(loginViewModel).Returns(ServiceResult<string>.Success(token));

            // Act
            var result = await _authenticationController.LoginAsync(loginViewModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedToken = Assert.IsType<string>(okResult.Value);
            Assert.Equal(token, returnedToken);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var _model = new RegisterUserViewModel
            {
                UserName = null,
                Password = null,
                ConfirmPassword = null
            };
            _authenticationController.ModelState.AddModelError("Username", "Required");

            // Act
            var result = await _authenticationController.RegisterUserAsync(_model);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task RegisterUserAsync_ShouldReturnOk_WhenRegistrationSucceeds()
        {
            // Arrange
            var _model = AuthenticationTestData.ValidRegiserUserViewModel();

            _authenticationService.RegisterUserAsync(_model).Returns(ServiceResult<string>.Success(Constants.USER_SUCCESSFULLY_REGISTERED_MESSAGE));

            // Act
            var result = await _authenticationController.RegisterUserAsync(_model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(Constants.USER_SUCCESSFULLY_REGISTERED_MESSAGE, okResult.Value);
        }


    }
}
