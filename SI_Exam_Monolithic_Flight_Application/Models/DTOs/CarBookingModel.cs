using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    [XmlRoot(ElementName = "carBooking")]  
    public class CarBookingModel
    {
        public string carId { get; set; }
        public string username { get; set;}
        public DateTime startDate { get;set; }
        public DateTime endDate { get; set;}
        public long price { get; set; }

        public CarBookingModel()
        {
        }

        public CarBookingModel(string carId, string username, DateTime startDate, DateTime endDate, long price)
        {
            this.carId = carId;
            this.username = username;
            this.startDate = startDate;
            this.endDate = endDate;
            this.price = price;
        }

        public override string ToString()
        {
            return carId + username + startDate + endDate + price;
        }
    }
}
