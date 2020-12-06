using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SI_Exam_Monolithic_Flight_Application.Models
{
    public class FlightSearchModel
    {
        [Required]
        [DisplayName("Departure Airport")]
        public string departureAirport { get; set; }
        [Required]
        public string arrivalAirport { get; set; }
        [Required]
        [DisplayName("Departure Date")]
        [DataType(DataType.Date)]
        public DateTime departureDate { get; set; }
        [Required]
        [DisplayName("Return Date")]
        [DataType(DataType.Date)]
        public DateTime returnDate { get; set; }

        public string image { get; set; }
        public string time { get; set; }

        public FlightSearchModel(string departureAirport, string arrivalAirport, string image, string time)
        {
            this.departureAirport = departureAirport;
            this.arrivalAirport = arrivalAirport;
            this.image = image;
            this.time = time;
        }
    }
}
