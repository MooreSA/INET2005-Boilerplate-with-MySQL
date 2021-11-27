using System;
using Microsoft.AspNetCore.Mvc;
using Launchpad.Models;
using Microsoft.AspNetCore.Http;

namespace Launchpad.Controllers {

    public class LoginController : Controller {

        private UserLogin userLogin;

        public LoginController(UserLogin userLogin) {
            this.userLogin = userLogin; // Dependency injection
        }

        [HttpGet]
        public IActionResult Index() {
            if (TempData["NewUser"] != null) {
                ViewBag.NewUser = TempData["NewUser"];
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            return View(userLogin);
        }

        [HttpPost]
        public IActionResult Login(UserLogin userLogin) {
            // TODO: Validate userLogin
            User user = userLogin.Login(HttpContext);
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }

            if (user != null) {
                return RedirectToAction("Index", "Admin"); // Redirect to the admin page
            } else {
                ViewBag.Error = "Invalid username or password";
                return View("Index", userLogin); // Bad login, return to login page
            }
        }
        [HttpGet]
        public IActionResult Register() {
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }

            return View(userLogin);
        }

        [HttpPost]
        public IActionResult Register(UserLogin userLogin) {
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }

            User user = userLogin.Register(HttpContext);
            if (user == null) {
                return View("Register", userLogin);
            }
            TempData["NewUser"] = user.username;
            return RedirectToAction("Index", "Admin");
        }
    }
}