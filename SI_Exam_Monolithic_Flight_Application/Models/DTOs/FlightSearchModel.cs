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
        public int id { get; }
        [Required]
        [DisplayName("Departure Airport")]
        public string departureAirport { get;  }
        [Required]
        public string arrivalAirport { get;  }
        [Required]
        [DisplayName("Departure Date")]
        [DataType(DataType.Date)]
        public DateTime departureDate { get;  }
        [Required]
        [DisplayName("Return Date")]
        [DataType(DataType.Date)]
        public DateTime returnDate { get;  }

        public string image { get;  }
        public string time { get;  }

        public FlightSearchModel(int id, string departureAirport, string arrivalAirport, string image, string time)
        {
            this.id = id;
            this.departureAirport = departureAirport;
            this.arrivalAirport = arrivalAirport;
            this.image = image;
            this.time = time;
        }

        public FlightSearchModel()
        {
        }


    }
}
