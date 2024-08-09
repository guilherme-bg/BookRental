﻿using System.ComponentModel.DataAnnotations;

namespace BookRental.Server.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserName is required!")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
