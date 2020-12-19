using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{

    public class CarModel
    {
        [XmlElement]
        public int id { get; set; }
        [XmlElement]

        public string brand { get; set; }
        [XmlElement]

        public string year { get; set; }
        [XmlElement]

        public string km { get; set; }
        [XmlElement]
        public long price { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }
        //public long PriceCalculated => price / 100;
        public CarModel()
        {
        }

        public CarModel(int id, string brand, string year, string km, long price)
        {
            this.id = id;
            this.brand = brand;
            this.year = year;
            this.km = km;
            this.price = price;
        }
        public CarModel(int id, string brand, string year, string km, long price, string startDate, string endDate)
        {
            this.id = id;
            this.brand = brand;
            this.year = year;
            this.km = km;
            this.price = price;
            this.startDate = startDate;
            this.endDate = endDate;
        }

    }
}
