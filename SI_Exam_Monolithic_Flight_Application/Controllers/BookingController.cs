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
using SI_Exam_Monolithic_Flight_Application.Models;
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
        public IActionResult Flight(int id, string departureAirport, string arrivalAirport, string departureDate, string returnDate, string time, long price, string image, int passengers)
        {

            // Make a booking in the database
            var bookingId = facade.BookFlight(1, id, price, passengers);
            HttpContext.Session.SetInt32("bookingId", bookingId);

            // Serialize Flight
            var flight = new FlightSearchModel(id, departureAirport, arrivalAirport, image, time, price, departureDate, returnDate);
            var serializedFlight = XmlUtils<FlightSearchModel>.SerializeToString(flight);
            HttpContext.Session.SetString("BookedFlight", serializedFlight);
    
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cars()
        {
            // TODO: Fetch cars from car microservice
            //var carsXML = ExternalRequests.GetCars();
            //var cars = XmlUtils<List<CarModel>>.DeserializeToType(carsXML);
            List<CarModel> tempCars = new List<CarModel>()
            {
                new CarModel("123435", "Ferrari", "", "2017", "100.000", 1_000_000),
                new CarModel("123436", "Ferrari", "", "2020", "1.000", 5_000_000),
                new CarModel("123437", "Ferrari", "", "2018", "5.000", 1_500_000),
                new CarModel("123438", "Ferrari", "", "2017", "100.000", 2_750_000),
            };
            TempData["Cars"] = tempCars;
            return View("Car");
        }

        [HttpPost]
        public IActionResult Additional(string carId, string brand, string year, string km, long carPrice, string image)
        {
            var bookingId = HttpContext.Session.GetInt32("bookingId");
            if (!String.IsNullOrEmpty(carId))
            {
                //TODO: Book a car
                var startDate = HttpContext.Session.GetString("departureDate");
                var endDate = HttpContext.Session.GetString("returnDate");

                
                var bookedCar = new CarModel(carId, brand, image, year, km, startDate, endDate, carPrice);

                //var res = XmlUtils<CarBookingModel>.SerializeToString(bookedCar);
                TempData["BookedCar"] = bookedCar;
            }
            else
            {
                var camundaProcessId = ExternalRequests.CamundaBookFlight(bookingId, true, false);
                HttpContext.Session.SetString("camundaProcessId", camundaProcessId);
                Debug.WriteLine("===========");
                Debug.WriteLine(camundaProcessId);
                Debug.WriteLine("===========");
            }
            return View("Confirmation");
        }

        [HttpPost]
        public IActionResult Confirm(string status)
        {
            var processId = HttpContext.Session.GetString("camundaProcessId");
            TempData["OrderStatus"] = status;
            if (status == "Confirmed")
            {
                ExternalRequests.CamundaConfirmORder(processId, true);

            }
            else
            {
                ExternalRequests.CamundaConfirmORder(processId, false);

            }
            return View("Confirmation");
        }

    }

}
