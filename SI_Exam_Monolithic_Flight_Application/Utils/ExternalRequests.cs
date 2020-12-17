using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;

namespace SI_Exam_Monolithic_Flight_Application.Utils
{
    public class ExternalRequests
    {
        private string EurekaUrl = "localhost:someport";
        public string GetCars()
        {
            HttpWebRequest webRequest =
                (HttpWebRequest)WebRequest.Create($@"{EurekaUrl}/car-catalog");
            webRequest.Method = "GET";
            webRequest.Accept = "application/xml";
            webRequest.ContentType = "application/xml";
            var response = webRequest.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                return responseFromServer;
            }
        }
    }
}
