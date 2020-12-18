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
        private static string EurekaUrl = "localhost:someport";
        public static string GetCars()
        {
            var URL = $@"{EurekaUrl}/car-catalog";
            var response = MakeRequest(URL, "GET", "application/xml");
            if (response.StatusCode == HttpStatusCode.OK)
            {

                return ReadResponse(response);

            }

            throw new Exception(ReadResponse(response));


        }

        private static HttpWebResponse MakeRequest(string URL, string method, string contentType)
        {
            HttpWebRequest webRequest =
                (HttpWebRequest)WebRequest.Create(URL);
            webRequest.Method = method;
            webRequest.Accept = "application/xml";
            webRequest.ContentType = "application/xml";
            return (HttpWebResponse)webRequest.GetResponse();
        }

        private static string ReadResponse(HttpWebResponse response)
        {
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
