using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    public enum FLIGHT_STATUS
    {
        RESERVED,
        PAID,
        CANCELLED
    };
    public class FlightBookingModel
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int flight_id { get; set; }
        public int price { get; set; }
        public FLIGHT_STATUS status { get; set; }
    }
}
