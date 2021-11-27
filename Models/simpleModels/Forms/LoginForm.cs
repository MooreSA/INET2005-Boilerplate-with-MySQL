using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;

namespace Launchpad.Models {
    public class LoginForm {

        [Key]
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string password { get; set; }
    }
}