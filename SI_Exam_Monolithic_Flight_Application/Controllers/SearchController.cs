﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SI_Exam_Monolithic_Flight_Application.Facade;
using SI_Exam_Monolithic_Flight_Application.Models;
using SI_Exam_Monolithic_Flight_Application.Models.Data;

namespace SI_Exam_Monolithic_Flight_Application.Controllers
{
    public class SearchController : Controller
    {


        [Route("search-flight/{departureAirport}-{arrivalAirport}/{departureDate}/{returnDate}/{passengers}", Name="SearchFlight")]
        public ActionResult SearchFlight(string departureAirport, string arrivalAirport, string departureDate, string returnDate, int passengers)
        {
            //Contact DB
            var results = FlightFacade.Singleton().SearchForFlight(departureAirport, arrivalAirport);


            // Save values for graphical uses only. As this is a proof of concept we just want to show a lists of flights regardless of the date.
            HttpContext.Session.SetString("departureDate", departureDate);
            HttpContext.Session.SetString("returnDate", returnDate);
            HttpContext.Session.SetInt32("Passengers", passengers);

            // TODO: Throw and catch custome FlightsNotFound Exception
            if (results.Count < 1)
            {
                TempData["ErrorMessage"] = "No flights found";
                return RedirectToAction("Index");
            }
            // Save result data to retrieve it in the View
            TempData["FlightObjects"] = results;
            
            return View("SearchResults");
        }


        public IActionResult Index()
        {
            return View("search-flight");
        }

 


    }
}
