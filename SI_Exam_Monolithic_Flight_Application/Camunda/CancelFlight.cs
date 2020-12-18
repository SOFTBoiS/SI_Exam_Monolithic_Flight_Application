using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient.Dto;
using CamundaClient.Worker;
using SI_Exam_Monolithic_Flight_Application.Facade;
using SI_Exam_Monolithic_Flight_Application.Models.Data;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;

namespace SI_Exam_Monolithic_Flight_Application.Camunda
{
    [ExternalTaskTopic("cancel_flight")]
    [ExternalTaskVariableRequirements("bookedFlight", "bookedCar", "bookingId")]
    public class CancelFlight : IExternalTaskAdapter
    {
        //TODO: Move this into another class. Is used in different places
        private static string _sqlServer = Environment.GetEnvironmentVariable("SI_EXAM_SERVER");
        private static string _database = Environment.GetEnvironmentVariable("SI_EXAM_DB_NAME");
        private static string _trustedConn = "true";
        private static DataAccessObject DAO = new DataAccessObject(_sqlServer, _database, _trustedConn);
        FlightFacade facade = new FlightFacade(DAO);
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var bookingId = (long) externalTask.Variables["bookingId"].Value;
            facade.UpdateBookingStatus(bookingId, FLIGHT_STATUS.CANCELLED);
        }
    }
}
