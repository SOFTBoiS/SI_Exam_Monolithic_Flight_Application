using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SI_Exam_Monolithic_Flight_Application.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SI_Exam_Monolithic_Flight_Application.Facade;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;

namespace SI_Exam_Monolithic_Flight_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
                return RedirectToAction("Index", "Search");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Register(string username, string name, string email, string password)
        {
            try
            {
                var (id, usrname) = UserFacade.Singleton().CreateUser(username, password, name, email);
                HttpContext.Session.SetInt32("userId", id);
                HttpContext.Session.SetString("username", usrname);
                return RedirectToAction("Index", "Search");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View("Index");
            }
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {

                var (id, usrname) = UserFacade.Singleton().RetrieveUser(username, password);
                HttpContext.Session.SetInt32("userId", id);
                HttpContext.Session.SetString("username", usrname);
                return RedirectToAction("Index", "Search");
            }
            catch (UserNotFoundException unfe)
            {
                TempData["ErrorMessage"] = unfe.Message;
                return View("Index");
            }


        }
    }
}
