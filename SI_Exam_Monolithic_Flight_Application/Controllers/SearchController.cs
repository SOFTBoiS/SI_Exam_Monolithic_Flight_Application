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
using SI_Exam_Monolithic_Flight_Application.KafkaConnect;
using Confluent.Kafka;

namespace SI_Exam_Monolithic_Flight_Application.Controllers
{
    public class SearchController : Controller
    {

        private static string _sqlServer = Environment.GetEnvironmentVariable("SI_EXAM_SERVER");
        private static string _database = Environment.GetEnvironmentVariable("SI_EXAM_DB_NAME");
        private static string _trustedConn = "true";
        private static DataAccessObject DAO = new DataAccessObject(_sqlServer, _database, _trustedConn);
        
        // Kafka Connect
        private static ClientConfig config;
        private string topic = "SI_exam.searchHistory";

        public SearchController()
        {
            
        }

        FlightFacade facade = new FlightFacade(DAO);

        [Route("search-flight/{departureAirport}-{arrivalAirport}/{departureDate}/{returnDate}/", Name="SearchFlight")]
        public async Task<ActionResult> SearchFlightAsync(string departureAirport, string arrivalAirport, string departureDate, string returnDate)
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

            

            try
            {
                var config = await SearchHistory.LoadConfig();
                await SearchHistory.CreateTopicMaybe(topic, 1, 3, config);
                SearchHistory.Produce(departureAirport,$"{arrivalAirport},{departureDate},{returnDate}", topic, config);
                //SearchHistory.PrintUsage();

            }
            catch(Exception e)
            {
                Debug.WriteLine("Did not work ", e.Message);
            }

            return View("SearchResults");
        }

 


    }
}
