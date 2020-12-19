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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Flight(int id, string departureAirport, string arrivalAirport, string departureDate, string returnDate, string time, long price, string image, int passengers)
        {
            var userId = (int) HttpContext.Session.GetInt32("userId");
            // Make a booking in the database
            var bookingId = FlightFacade.Singleton().BookFlight(userId, id, price, passengers);
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
            var cars = ExternalRequests.GetCars();
            var bookingId = HttpContext.Session.GetInt32("bookingId");
            var camundaProcessId = ExternalRequests.CamundaBookFlight(bookingId, true, true);
            HttpContext.Session.SetString("camundaProcessId", camundaProcessId);
            TempData["Cars"] = cars;
            return View("Car");
        }


        [HttpPost]
        public IActionResult Additional(string carId, string brand, string year, string km, long carPrice)
        {
            var bookingId = HttpContext.Session.GetInt32("bookingId");
            if (!String.IsNullOrEmpty(carId))
            {
                //TODO: Book a car
                var startDate = HttpContext.Session.GetString("departureDate");
                var endDate = HttpContext.Session.GetString("returnDate");
                var username = HttpContext.Session.GetString("username");

                var order = new CarOrder(username, carId, startDate, endDate, carPrice);
                var serializedOrder = XmlUtils<CarOrder>.SerializeToString(order);
                var response = ExternalRequests.BookCar(serializedOrder);
                var linkToCar = response._links[0].href;
                var processId = HttpContext.Session.GetString("camundaProcessId");
                ExternalRequests.CamundaBookCar(processId, linkToCar, true);
                // carBookingURL

                Debug.WriteLine("Booked car: " + linkToCar);
                TempData["BookedCar"] = new CarModel(int.Parse(carId), brand, year, km, carPrice, startDate, endDate);
            }
            else
            {
                var camundaProcessId = ExternalRequests.CamundaBookFlight(bookingId, true, false);
                HttpContext.Session.SetString("camundaProcessId", camundaProcessId);

            }
            return View("Confirmation");
        }

        [HttpPost]
        public IActionResult Confirm(string status)
        {
            var processId = HttpContext.Session.GetString("camundaProcessId");
            TempData["OrderStatus"] = status;

            var orderConfirmed = status == "Confirmed";
            ExternalRequests.CamundaConfirmOrder(processId, orderConfirmed);

            return View("Confirmation");
        }

    }

}
