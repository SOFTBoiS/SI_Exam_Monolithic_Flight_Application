using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SI_Exam_Monolithic_Flight_Application.Models;
using SI_Exam_Monolithic_Flight_Application.Models.Data;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;

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

        public int BookFlight(int userId, int flightId, long price, int passengers)
        {
            
            var bookingId = DAO.ReserveFlight(userId, flightId, price, passengers);
            return bookingId;
        }

        public int UpdateBookingStatus(long bookingId, FLIGHT_STATUS newStatus)
        {
            var affectedRows = DAO.UpdateBookingStatus(bookingId, newStatus);
            return affectedRows;
        }
    }
}
