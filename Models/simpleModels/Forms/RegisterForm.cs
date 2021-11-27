using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;

namespace Launchpad.Models {
    public class RegisterForm : LoginForm {

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string passwordConfirm {get; set;}
    }
}