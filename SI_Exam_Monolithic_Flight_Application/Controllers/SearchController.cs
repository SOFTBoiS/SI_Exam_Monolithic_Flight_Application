using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SI_Exam_Monolithic_Flight_Application.Facade;
using SI_Exam_Monolithic_Flight_Application.Models;
using SI_Exam_Monolithic_Flight_Application.Models.Data;

namespace SI_Exam_Monolithic_Flight_Application.Controllers
{
    public class SearchController : Controller
    {

        private static string _sqlServer = Environment.GetEnvironmentVariable("SI_EXAM_SERVER");
        private static string _database = Environment.GetEnvironmentVariable("SI_EXAM_DB_NAME");
        private static string _trustedConn = "true";
        private static DataAccessObject DAO = new DataAccessObject(_sqlServer, _database, _trustedConn);

        FlightFacade facade = new FlightFacade(DAO);

        [Route("search-flight/{departureAirport}-{arrivalAirport}/{departureDate}/{returnDate}/", Name="SearchFlight")]
        public ActionResult SearchFlight(string departureAirport, string arrivalAirport, string departureDate, string returnDate)
        {
            // Save values for graphical uses only. As this is a proof of concept we just want to show a lists of flights regardless of the date.
            TempData["departureDate"] = departureDate;
            TempData["returnDate"] = returnDate;

            //Contact DB
            var results = facade.SearchForFlight(departureAirport, arrivalAirport);
            var response = JsonConvert.SerializeObject(results);
            XmlSerializer serializer = new XmlSerializer(results.GetType());
            
            using (var myWriter = new StringWriter())
            {
                new XmlSerializer(results.GetType()).Serialize(myWriter, results);
                TempData["Flights"] = myWriter.ToString();
            }

            TempData["FlightObjects"] = results;

            
            return View("SearchResults");
        }

 


    }
}
