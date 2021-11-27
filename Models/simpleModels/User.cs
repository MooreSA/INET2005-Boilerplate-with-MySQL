using System;
using System.ComponentModel.DataAnnotations;

namespace Launchpad.Models {
    public class User {


        [Key]
        public string username {get; set;}

        public string salt {get; set;}

        public string hashedPassword {get; set;}
    }
}