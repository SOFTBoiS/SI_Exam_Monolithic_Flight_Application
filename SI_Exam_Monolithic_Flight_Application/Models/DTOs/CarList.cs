using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    
    [XmlRoot("cars")]
    public class CarList
    {
        [XmlArray("Collection")][XmlArrayItem("item", typeof(CarModel))]
        public List<CarModel> carList { get; set; }

        // Constructor
        public CarList()
        {
            carList = new List<CarModel>();
        }

        public void AddCar(CarModel flight)
        {
            carList.Add(flight);
        }



    }
}
