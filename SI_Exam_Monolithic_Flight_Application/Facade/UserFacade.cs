using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SI_Exam_Monolithic_Flight_Application.Models.Data;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;

namespace SI_Exam_Monolithic_Flight_Application.Facade
{
    public class UserFacade
    {
        private static UserDataAccessObject DAO;
        private static UserFacade _instance;


        /// <summary>
        /// Default constructor, use this for production.
        /// </summary>
        private UserFacade()
        {
            DAO = new UserDataAccessObject();
        }

        /// <summary>
        /// Use this constructor for testing purposes or if you need to connect to a different database.
        /// </summary>
        /// <param name="flightDataAccessObject"></param>
        private UserFacade(FlightDataAccessObject flightDataAccessObject)
        {

            _instance = new UserFacade(flightDataAccessObject);

        }

        public static UserFacade Singleton()
        {
            if (_instance == null)
            {
                _instance = new UserFacade();
            }

            return _instance;
        }
        public static UserFacade Singleton(FlightDataAccessObject flightDataAccessObject)
        {
            if (_instance == null)
            {
                _instance = new UserFacade(flightDataAccessObject);
            }

            return _instance;
        }

        public (int, string) RetrieveUser(string username, string password)
        {
            try
            {

                var (id, usrname) = DAO.RetrieveUser(username, password);
                return (id, usrname);
            }
            catch (UserNotFoundException unfe)
            {
                throw unfe;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public (int, string) CreateUser(string username, string password, string name, string email)
        {
            var (id, usrname) = DAO.CreateUser(username, password, name, email);
            return (id, usrname);
        }

    }
}
