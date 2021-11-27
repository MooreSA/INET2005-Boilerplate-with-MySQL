using System;
using Microsoft.AspNetCore.Mvc;
using Launchpad.Models;
using Microsoft.AspNetCore.Http;

namespace Launchpad.Controllers {

    public class HomeController : Controller {

        private PublicManager publicManager;

        public HomeController(PublicManager publicManager) {
            this.publicManager = publicManager; // Dependency injection
        }

        public IActionResult Index() {
            return View(publicManager);
        }
    }
}
