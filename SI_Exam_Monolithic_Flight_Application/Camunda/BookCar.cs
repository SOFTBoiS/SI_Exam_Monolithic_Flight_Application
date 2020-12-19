using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient.Dto;
using CamundaClient.Worker;

namespace SI_Exam_Monolithic_Flight_Application.Camunda
{
    [ExternalTaskTopic("book_car")]
    [ExternalTaskVariableRequirements("bookedFlight", "wantsToBookCar", "bookingId")]
    public class BookCar: IExternalTaskAdapter
    {
        public void Execute(ExternalTask externalTask, ref Dictionary<string, object> resultVariables)
        {

        }
    }
}
