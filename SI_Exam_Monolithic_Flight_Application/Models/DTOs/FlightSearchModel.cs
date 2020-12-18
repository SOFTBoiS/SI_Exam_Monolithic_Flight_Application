using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_Exam_Monolithic_Flight_Application.Models
{

    [XmlRoot]  public class FlightSearchModel
    {
        public int id { get; set;  }
        [Required]
        [DisplayName("Departure Airport")]
        public string departureAirport { get; set;  }
        [Required]
        public string arrivalAirport { get; set;  }
        [Required]
        [DisplayName("Departure Date")]
        [DataType(DataType.Date)]
        public string departureDate { get; set;  }
        [Required]
        [DisplayName("Return Date")]
        [DataType(DataType.Date)]
        public string returnDate { get; set;  }

        public string image { get; set;  }
        public string time { get; set;  }

        public long price { get; set;  }

        public long PriceCalculated => price / 100;

        public FlightSearchModel(int id, string departureAirport, string arrivalAirport, string image, string time, long price, string departureDate, string returnDate)
        {
            this.id = id;
            this.departureAirport = departureAirport;
            this.arrivalAirport = arrivalAirport;
            this.image = image;
            this.time = time;
            this.price = price;
            this.departureDate = departureDate;
            this.returnDate = returnDate;
        }

        public FlightSearchModel()
        {
        }


    }
}
