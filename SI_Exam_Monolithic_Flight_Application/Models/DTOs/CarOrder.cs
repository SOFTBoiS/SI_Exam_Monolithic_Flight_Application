using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    [XmlRoot("order")]
    public class CarOrder
    {
        [XmlElement("username")]
        public string username { get; set; }        
        [XmlElement("carId")]
        public string carId { get; set; }        
        [XmlElement("startDate")]
        public string startDate { get; set; }        
        [XmlElement("endDate")]
        public string endDate { get; set; }        
        [XmlElement("price")]
        public long price { get; set; }

        public CarOrder()
        {
        }

        public CarOrder(string username, string carId, string startDate, string endDate, long price)
        {
            this.username = username;
            this.carId = carId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.price = price;
        }
    }
}
