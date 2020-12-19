using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SI_Exam_Monolithic_Flight_Application.Models.DTOs
{
    [XmlRoot("order")]
    public class CarOrderResponse
    {


        [XmlArray("_links")]
        [XmlArrayItem("self")]
        public List<SelfClass> _links { get; set; }

        [XmlElement("price")]
        public long price { get; set; }
        [XmlElement("startDate")]
        public string startDate { get; set; }
        [XmlElement("endDate")]
        public string endDate { get; set; }
        [XmlElement("username")]
        public string username { get; set; }
        [XmlElement("carId")]
        public string carId { get; set; }

        public CarOrderResponse()
        {
            _links = new List<SelfClass>();
        }

        public void AddLink(SelfClass link)
        {
            _links.Add(link);
        }

        public class SelfClass
        {
            [XmlElement("href")]
            public string href { get; set; }

            public SelfClass()
            {
            }
        }
    }
}
