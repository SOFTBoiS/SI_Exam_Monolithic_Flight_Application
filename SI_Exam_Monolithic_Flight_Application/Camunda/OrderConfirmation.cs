using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient.Dto;
using CamundaClient.Worker;
using SI_Exam_Monolithic_Flight_Application.Facade;
using SI_Exam_Monolithic_Flight_Application.Models.Data;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;

namespace SI_Exam_Monolithic_Flight_Application.Camunda
{
    [ExternalTaskTopic("order_confirmation")]
    [ExternalTaskVariableRequirements("confirmedOrder", "bookingId")]
    public class OrderConfirmation : IExternalTaskAdapter
    {
        //TODO: Move this into another class. Is used in different places
        private static string _sqlServer = Environment.GetEnvironmentVariable("SI_EXAM_SERVER");
        private static string _database = Environment.GetEnvironmentVariable("SI_EXAM_DB_NAME");
        private static string _trustedConn = "true";
        private static DataAccessObject DAO = new DataAccessObject(_sqlServer, _database, _trustedConn);
        FlightFacade facade = new FlightFacade(DAO);
        //TODO: If confirmed change status of flight
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            externalTask.Variables.TryGetValue("confirmedOrder", out var orderIsConfirmed);
            if (orderIsConfirmed != null)
            {
                var isConfirmed = (bool) orderIsConfirmed.Value;
                if (isConfirmed)
                {
                    var bookingId = (long) externalTask.Variables["bookingId"].Value;
                    facade.UpdateBookingStatus(bookingId, FLIGHT_STATUS.PAID);
                }

            }

        }
    }
}
