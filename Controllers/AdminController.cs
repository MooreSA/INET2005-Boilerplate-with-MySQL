using System;
using Microsoft.AspNetCore.Mvc;
using Launchpad.Models;
using Microsoft.AspNetCore.Http;

namespace Launchpad.Controllers {

    public class AdminController : Controller {

        public AdminMangager adminMangager;

        public AdminController(AdminMangager adminMangager) {
            this.adminMangager = adminMangager; // Dependency injection
        }

        public IActionResult Index() {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to view this page";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            return View("Index", adminMangager);
        }

        [Route("/logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        [Route("admin/edit-category/{id:int}")]
        public IActionResult EditCategory(int id) {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to edit a category";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            adminMangager.catId = id;
            if (adminMangager.checkValidCatId(id)) {
                adminMangager.catId = id;
            } else {
                TempData["Error"] = "Invalid category id";
                return RedirectToAction("Index", "Admin");
            }
            Console.WriteLine("Editing Category Id: " + id);
            return View(adminMangager);
        }

        [HttpPost]
        [Route("admin/edit-category/{id:int}")]
        public IActionResult EditCategory(AdminMangager adminMangager, int id) {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to edit a category";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            if (adminMangager.checkValidCatId(id)) {
                adminMangager.catId = id;
            } else {
                TempData["Error"] = "Invalid category id";
                return RedirectToAction("Index", "Admin");
            }
            if (!ModelState.IsValid) {
                TempData["Error"] = "Invalid form data";
                return RedirectToAction("Index", "Admin");
            }
            try {
                adminMangager.EditCategory(); 
            } catch (Exception e) {
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Admin");
            }
            TempData["Message"] = "Category edited successfully";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Route("admin/add-link/{id:int}")]
        public IActionResult AddLink(int id) {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to add a link";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            if (adminMangager.checkValidCatId(id)) {
                adminMangager.catId = id;
            } else {
                TempData["Error"] = "Invalid category id";
                return RedirectToAction("Index", "Admin");
            }
            return View("AddLink", adminMangager);
        }

        [HttpPost]
        [Route("admin/add-link/{id:int}")]
        public IActionResult AddLink(AdminMangager adminMangager, int id) {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to add a link";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            if (adminMangager.checkValidCatId(id)) {
                adminMangager.catId = id;
            } else {
                TempData["Error"] = "Invalid category id";
                return RedirectToAction("Index", "Admin");
            } if (!ModelState.IsValid) {
                TempData["Error"] = "Invalid form data";
                return RedirectToAction("Index", "Admin");
            }
            adminMangager.AddLink();
            TempData["Message"] = "Link added successfully";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Route("admin/edit-link/{id:int}")]
        public IActionResult EditLink(int id) {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to edit a link";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            if (adminMangager.checkValidLinkId(id)) {
                adminMangager.linkId = id;
                adminMangager.PopulateLinkForm();
            } else {
                TempData["Error"] = "Invalid link id";
                return RedirectToAction("Index", "Admin");
            }
            return View("EditLink", adminMangager);
        }
        [HttpPost]
        [Route("admin/edit-link/{id:int}")]
        public IActionResult EditLink(AdminMangager adminMangager, int id) {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to edit a link";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            if (adminMangager.checkValidLinkId(id)) {
                adminMangager.linkId = id;
            } else {
                TempData["Error"] = "Invalid link id";
                return RedirectToAction("Index", "Admin");
            }
            if (!ModelState.IsValid) {
                TempData["Error"] = "Invalid form data";
                return RedirectToAction("Index", "Admin");
            }
            adminMangager.EditLink();
            TempData["Message"] = "Link edited successfully";
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Route("admin/delete-link/{id:int}")]
        public IActionResult DeleteLink(int id) {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to delete a link";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            if (adminMangager.checkValidLinkId(id)) {
                adminMangager.linkId = id;
                adminMangager.PopulateLinkForm();
            } else {
                TempData["Error"] = "Invalid link id";
                return RedirectToAction("Index", "Admin");
            }
            return View("DeleteLink", adminMangager);
        }

        [HttpPost]
        [Route("admin/delete-link/{id:int}")]
        public IActionResult DeleteLink(AdminMangager adminMangager, int id) {
            if (HttpContext.Session.GetString("auth") != "true") {
                TempData["Error"] = "You must be logged in to delete a link";
                return RedirectToAction("Index", "Login");
            }
            if (TempData["Error"] != null) {
                ViewBag.Error = TempData["Error"];
            }
            if (adminMangager.checkValidLinkId(id)) {
                adminMangager.linkId = id;
            } else {
                TempData["Error"] = "Invalid link id";
                return RedirectToAction("Index", "Admin");
            } if (!ModelState.IsValid) {
                TempData["Error"] = "Invalid form data";
                return RedirectToAction("Index", "Admin");
            }
            adminMangager.DeleteLink();
            TempData["Message"] = "Link deleted successfully";
            return RedirectToAction("Index", "Admin");
        }
    }
}
