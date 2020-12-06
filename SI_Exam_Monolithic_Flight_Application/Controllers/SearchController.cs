using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SI_Exam_Monolithic_Flight_Application.Facade;
using SI_Exam_Monolithic_Flight_Application.Models;
using SI_Exam_Monolithic_Flight_Application.Models.Data;

namespace SI_Exam_Monolithic_Flight_Application.Controllers
{
    public class SearchController : Controller
    {

        private static string _sqlServer = "LAPTOP-DDB3EQTP";
        private static string _database = "si_exam";
        private static string _trustedConn = "true";
        private static DataAccessObject DAO = new DataAccessObject(_sqlServer, _database, _trustedConn);

        FlightFacade facade = new FlightFacade(DAO);

        [Route("search-flight/{departureAirport}/{arrivalAirport}/{departureDate}/{returnDate}/", Name="SearchFlight")]
        public ActionResult SearchFlight(string departureAirport, string arrivalAirport, string departureDate, string returnDate)
        {
            // Save values for graphical uses only. As this is a proof of concept we just want to show a lists of flights regardless of the date.
            TempData["departureDate"] = departureDate;
            TempData["returnDate"] = returnDate;

            //Contact DB
            var results = facade.SearchForFlight(departureAirport, arrivalAirport);
            var response = JsonConvert.SerializeObject(results);



            // If error
            //TempData["ErrorMessage"] = "TEST ERROR REE";
            //return RedirectToAction("Index", "Home");


            //return new FlightSearchModel[]
            //{
            //    new FlightSearchModel()
            //    {
            //        arrivalAirport = "CPH",
            //        departureAirport = "LAX",
            //        departureDate = new DateTime(2020, 12, 12),
            //        returnDate = new DateTime(2020, 12, 14)
            //    }
            //};
            TempData["Flights"] = response;
            return View("SearchResults");
        }

 


    }
}
