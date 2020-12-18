using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamundaClient.Worker;

namespace SI_Exam_Monolithic_Flight_Application.Camunda
{
    [ExternalTaskTopic("book_car")]
    [ExternalTaskVariableRequirements("bookedFlight", "bookedCar", "flightId")]
    public class BookCar
    {
    }
}
