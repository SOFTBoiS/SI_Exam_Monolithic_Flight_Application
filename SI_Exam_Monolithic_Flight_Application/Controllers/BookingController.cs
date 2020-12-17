using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using SI_Exam_Monolithic_Flight_Application.Facade;
using SI_Exam_Monolithic_Flight_Application.Models.Data;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;
using SI_Exam_Monolithic_Flight_Application.Utils;

namespace SI_Exam_Monolithic_Flight_Application.Controllers
{
    public class BookingController : Controller
    {
        private static string _sqlServer = Environment.GetEnvironmentVariable("SI_EXAM_SERVER");
        private static string _database = Environment.GetEnvironmentVariable("SI_EXAM_DB_NAME");
        private static string _trustedConn = "true";
        private static DataAccessObject DAO = new DataAccessObject(_sqlServer, _database, _trustedConn);

        FlightFacade facade = new FlightFacade(DAO);
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Flight(int id)
        {

            //Todo Create a flight booking
            facade.BookFlight(1, 1, 100);

            HttpContext.Session.SetInt32("flightId", id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cars()
        {
            // TODO: Fetch cars from car microservice
            List<CarModel> tempCars = new List<CarModel>()
            {
                new CarModel("123435", "Ferrari", "", "2017", "100.000"),
                new CarModel("123436", "Ferrari", "", "2020", "1.000"),
                new CarModel("123437", "Ferrari", "", "2018", "5.000"),
                new CarModel("123438", "Ferrari", "", "2017", "100.000"),
            };
            TempData["Cars"] = tempCars;
            return View("Car");
        }

        [HttpPost]
        public IActionResult Confirm(string carId)
        {
            if (!String.IsNullOrEmpty(carId))
            {
                //TODO: Book a car
                var objTest = new CarBookingModel("etmongoidher", "adam", DateTime.Today, DateTime.Today, 10000);

                var res = XmlUtils.SerializeToString(objTest);
                TempData["TestXML"] = res;
                Debug.WriteLine(res);



            }

            //Todo: Confirm booking and save it in DB
            return View("Confirmation");
        }

    }

}
