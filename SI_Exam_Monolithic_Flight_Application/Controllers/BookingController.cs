using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SI_Exam_Monolithic_Flight_Application.Controllers
{
    public class BookingController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Flight(int id)
        {
            //Todo Create a flight booking
            HttpContext.Session.SetInt32("flightId", id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cars()
        {

            return RedirectToAction("Index", "Car");
        }

        [HttpPost]
        public IActionResult Confirm()
        {
            //Todo: Confirm booking and save it in DB
            return View("Confirmation");
        }

    }

}
