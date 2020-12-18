using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    public class CarModel
    {
        public string _id { get; set; }
        public string brand { get; set; }
        public string image { get; set; }
        public string year { get; set; }
        public string km { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public long price { get; set; }
        public long PriceCalculated => price / 100;
        public CarModel()
        {
        }

        public CarModel(string id, string brand, string image, string year, string km, long price)
        {
            _id = id;
            this.brand = brand;
            this.image = image;
            this.year = year;
            this.km = km;
            this.price = price;
        }
        public CarModel(string id, string brand, string image, string year, string km, string startDate, string endDate, long price)
        {
            _id = id;
            this.brand = brand;
            this.image = image;
            this.year = year;
            this.km = km;
            this.price = price;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
