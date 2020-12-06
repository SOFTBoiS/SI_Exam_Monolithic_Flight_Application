using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SI_Exam_Monolithic_Flight_Application.Models;
using SI_Exam_Monolithic_Flight_Application.Models.Data;

namespace SI_Exam_Monolithic_Flight_Application.Facade
{
    public class FlightFacade
    {
        private DataAccessObject DAO;

        public FlightFacade(DataAccessObject dao)
        {
            DAO = dao;
        }

        public Collection<FlightSearchModel> SearchForFlight(string departureAirport, string arrivalAirport)
        {
            var result = DAO.GetFlights(departureAirport, arrivalAirport);
            return result;
        }

    }
}
