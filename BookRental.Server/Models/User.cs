﻿using System.ComponentModel.DataAnnotations;

namespace BookRental.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "UserName is required!")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public required string PasswordHash { get; set; }
    }
}
