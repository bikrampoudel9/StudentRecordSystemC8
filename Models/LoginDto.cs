﻿using System.ComponentModel.DataAnnotations;

namespace StudentMangementSystemC8.Models
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}
