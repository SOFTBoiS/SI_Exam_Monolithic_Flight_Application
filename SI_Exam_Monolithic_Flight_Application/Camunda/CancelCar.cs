using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient.Dto;
using CamundaClient.Worker;

namespace SI_Exam_Monolithic_Flight_Application.Camunda
{
    [ExternalTaskTopic("cancel_car")]
    [ExternalTaskVariableRequirements("bookedFlight", "bookedCar", "bookingId")]
    public class CancelCar : IExternalTaskAdapter
    {
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {
            // TODO: Cancel car reservation
            throw new NotImplementedException();
        }
    }
}
