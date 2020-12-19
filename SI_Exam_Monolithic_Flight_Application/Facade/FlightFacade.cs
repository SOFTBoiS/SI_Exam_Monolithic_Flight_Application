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
        private static DataAccessObject DAO;
        private static FlightFacade _instance;


        /// <summary>
        /// Default constructor, use this for production.
        /// </summary>
        public FlightFacade()
        {

            DAO = new DataAccessObject();
        }

        /// <summary>
        /// Use this constructor for testing purposes or if you need to connect to a different database.
        /// </summary>
        /// <param name="dataAccessObject"></param>
        public FlightFacade(DataAccessObject dataAccessObject)
        {

            _instance = new FlightFacade(dataAccessObject);

        }

        public static FlightFacade Singleton()
        {
            if (_instance == null)
            {
                _instance = new FlightFacade();
            }

            return _instance;
        }
        public static FlightFacade Singleton(DataAccessObject dataAccessObject)
        {
            if (_instance == null)
            {
                _instance = new FlightFacade(dataAccessObject);
            }

            return _instance;
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
