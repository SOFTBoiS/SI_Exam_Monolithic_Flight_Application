using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient.Dto;
using CamundaClient.Worker;
using SI_Exam_Monolithic_Flight_Application.Utils;

namespace SI_Exam_Monolithic_Flight_Application.Camunda
{
    [ExternalTaskTopic("cancel_car")]
    [ExternalTaskVariableRequirements("bookedFlight", "bookedCar", "bookingId", "carBookingURL")]
    public class CancelCar : IExternalTaskAdapter
    {
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {

            var carBookingURL = (string) externalTask.Variables["carBookingURL"].Value;
            ExternalRequests.DeleteCarOrder(carBookingURL);
        }
    }
}
