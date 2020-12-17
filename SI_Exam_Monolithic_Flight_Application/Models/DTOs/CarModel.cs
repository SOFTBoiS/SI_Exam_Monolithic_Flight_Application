using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    public class CarModel
    {
        public string _id { get; }
        public string brand { get; }
        public string image { get; }
        public string year { get; }
        public string km { get; }

        public CarModel()
        {
        }

        public CarModel(string id, string brand, string image, string year, string km)
        {
            _id = id;
            this.brand = brand;
            this.image = image;
            this.year = year;
            this.km = km;
        }
    }
}
