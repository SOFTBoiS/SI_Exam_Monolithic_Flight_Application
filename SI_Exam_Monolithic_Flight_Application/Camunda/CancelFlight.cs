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
    [ExternalTaskVariableRequirements("bookedFlight", "bookingId")]
    public class CancelFlight : IExternalTaskAdapter
    {

        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            var bookingId = (long) externalTask.Variables["bookingId"].Value;
            FlightFacade.Singleton().UpdateBookingStatus(bookingId, FLIGHT_STATUS.CANCELLED);
        }
    }
}
