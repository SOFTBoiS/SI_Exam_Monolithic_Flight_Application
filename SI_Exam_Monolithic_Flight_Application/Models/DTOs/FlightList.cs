using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    [XmlRoot("Flights")]
    public class FlightList
    {
        [XmlArray("FlightListing")] [XmlArrayItem("Flight", typeof(FlightSearchModel))]
        public List<FlightSearchModel> flightList;

        // Constructor
        public FlightList()
        {
            flightList = new List<FlightSearchModel>();
        }

        public void AddEmployee(FlightSearchModel flight)
        {
            flightList.Add(flight);
        }



    }
}
