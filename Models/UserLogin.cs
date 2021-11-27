using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Launchpad.Models;

namespace Launchpad.Models {

    public class UserLogin : DbContext {

        private int SUCCESS = 0;
        private int ERROR_NULL_ENTRY = 1;
        private int ERROR_PASS_MATCH = 2;
        private int ERROR_USER_EXISTS = 3;
        private int ERROR_BAD_INPUT = 4;



        // -------------------------------------------------- properties
        private DbSet<User> tblUsers{ get; set; }

        public LoginForm loginForm { get; set;}

        public RegisterForm registerForm { get; set; }

        public User user { get; set; }


        // -------------------------------------------------- Public Methods
        // Login the user
        public User Login(HttpContext httpContext) {
            httpContext.Session.Clear(); // Clear the context

            // Get the user
            Console.WriteLine("Username: " + loginForm);
            user = tblUsers.Where(u => u.username == loginForm.username).FirstOrDefault();

            // If the user exists
            if (user != null) {
                // If the password is correct
                if (VerifyPassword(loginForm.password, user.salt, user.hashedPassword) == SUCCESS) {
                    // Return the user
                    httpContext.Session.SetString("username", user.username);
                    httpContext.Session.SetString("auth", "true");
                    return user;
                }
            }

            // If the user doesn't exist or the password is incorrect
            return null;
        }

        public User Register(HttpContext httpContext) {
            httpContext.Session.Clear(); // Clear the context
            
            if (VerifyNewUser(registerForm.username, registerForm.password, registerForm.passwordConfirm) == SUCCESS) {
                // Create a new user
                User user = new User();
                user.username = registerForm.username;
                // user.Password = password; // Don't store the password in plain text Derp 
                user.salt = GenerateSalt();
                user.hashedPassword = HashPassword(registerForm.password, user.salt);

                // Add the user to the database
                tblUsers.Add(user);
                SaveChanges();

                // Return the user
                return user;
            };
            Console.WriteLine("User not registered");
            return null;
        }





        // -------------------------------------------------- private methods

        // Verify Password
        private int VerifyPassword(string password, string salt, string hashedPassword) {
            // Hash the password
            string hashedPasswordAttempt = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            
            // If the hashes match
            if (hashedPassword == hashedPasswordAttempt) {
                return SUCCESS; // Passwords match
            } else {
                return ERROR_PASS_MATCH; // Password doesn't match
            }
        }

        private bool ValidateInputs(string username, string password, string passwordConfirm) {
            if (username == null || password == null || passwordConfirm == null) {
                return false;
            } else if (username.Length > 100 || password.Length > 100 || passwordConfirm.Length > 100) {
                return false;
            } else {
                return true;
            }
        }

        // Verify new user
        private int VerifyNewUser(string username, string password, string passwordConfirm) {
            if (username == null || password == null || passwordConfirm == null) {
                Console.WriteLine("Null entry");
                return ERROR_NULL_ENTRY; // If the username or password is null
            } else if (!ValidateInputs(username, password, passwordConfirm)) {
                Console.WriteLine("Invalid entry");  
                return ERROR_BAD_INPUT; // If the username or password is invalid
            } else if (password != passwordConfirm) {
                Console.WriteLine("Passwords don't match");
                return ERROR_PASS_MATCH ; // If the passwords don't match
            } else if (tblUsers.Where(u => u.username == username).FirstOrDefault() != null) {
                Console.WriteLine("User exists");
                return ERROR_USER_EXISTS; // If the username is already taken
            } else {
                return SUCCESS; // If the user is valid
            }
        }

        private string GenerateSalt(){
            byte[] salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create()) {
				rng.GetBytes(salt);
			}
            return Convert.ToBase64String(salt);
        }

        private string HashPassword(string password, string salt) {
            byte[] byteSalt = Convert.FromBase64String(salt); // Kosher only please

            // Literal Magic
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: byteSalt,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

                return hashed; // Store the hashed password
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
           optionsBuilder.UseMySql(Connection.CON_STRING, new MySqlServerVersion(new Version(8, 0, 11)));
        }

    }

}