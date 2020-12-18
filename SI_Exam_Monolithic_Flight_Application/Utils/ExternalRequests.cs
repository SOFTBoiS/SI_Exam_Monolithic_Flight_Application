using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public static string CamundaBookFlight(int? bookingId, bool bookedFlight, bool bookedCar)
        {
            var URL = "http://localhost:8080/engine-rest/process-definition/key/Booking/start";

            var body = $"{{\"variables\": {{\"bookedCar\": {{\"value\":\"{bookedCar}\",\"type\":\"Boolean\"}},";
            body += $"\"bookedFlight\":{{\"value\":\"{bookedFlight}\",\"type\":\"Boolean\"}},";
            body += $"\"bookingId\": {{\"value\":\"{bookingId}\",\"type\":\"Integer\"}}}}}}";

            var response = MakeRequest(URL, "POST", "application/json", body);
            var json = ReadResponse(response);
            var data = (JObject) JsonConvert.DeserializeObject(json);
            var processUrl = data["links"].First["href"];
            var processId = processUrl.ToString().Split('/')[^1];
            return processId;
        }

        public static void CamundaConfirmORder(string processId, bool confirmedOrder)
        {
            var URL = $"http://localhost:8080/engine-rest/process-instance/{processId}/variables/confirmedOrder";

            var body = $"{{\"value\": \"{confirmedOrder}\", \"type\": \"Boolean\"}}";

            MakeRequest(URL, "PUT", "application/json", body);

        }

        private static HttpWebResponse MakeRequest(string URL, string method, string contentType, string body=null)
        {
            HttpWebRequest webRequest =
                (HttpWebRequest)WebRequest.Create(URL);
            webRequest.Method = method;
            webRequest.Accept = contentType;
            webRequest.ContentType = contentType;
            if (body != null)
            {
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.Write(body);
                }
            }
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
