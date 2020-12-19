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
        private static string EurekaUrl = "localhost:8080";
        public static CarList GetCars()
        {
            var URL = $"http://localhost:8081/mycars";
            var response = MakeRequest(URL, "GET", "application/xml");
            if (response.StatusCode == HttpStatusCode.OK)
            {

                var res = ReadResponse(response);
                CarList cars = XmlUtils<CarList>.DeserializeToType("<cars>" + res + "</cars>");
                return cars;
            }

            throw new Exception(ReadResponse(response));

        }

        public static CarOrderResponse BookCar(string body)
        {
            var URL = "http://localhost:8081/orders/";
            Debug.WriteLine(body);

            var response = MakeRequest(URL, "POST", "application/xml", body);
            var responseString = ReadResponse(response);
            var responseObj = XmlUtils<CarOrderResponse>.DeserializeToType(responseString);
            return responseObj;
        }

        public static void DeleteCarOrder(string URL)
        {
            // TODO: Error handling
            var response = MakeRequest(URL, "DELETE", "application/xml");
            Debug.WriteLine(response);
        }


        public static string CamundaBookFlight(int? bookingId, bool bookedFlight, bool bookedCar)
        {
            var URL = "http://localhost:8080/engine-rest/process-definition/key/Booking/start";

            var body = $"{{\"variables\": {{\"wantsToBookCar\": {{\"value\":\"{bookedCar}\",\"type\":\"Boolean\"}},";
            body += $"\"bookedFlight\":{{\"value\":\"{bookedFlight}\",\"type\":\"Boolean\"}},";
            body += $"\"bookingId\": {{\"value\":\"{bookingId}\",\"type\":\"Integer\"}}}}}}";

            var response = MakeRequest(URL, "POST", "application/json", body);
            var json = ReadResponse(response);
            var data = (JObject)JsonConvert.DeserializeObject(json);
            var processUrl = data["links"].First["href"];
            var processId = processUrl.ToString().Split('/')[^1];
            return processId;
        }
        public static void CamundaBookCar(string processId, string carBookingURL, bool bookedCar)
        {
            var URL = $"http://localhost:8080/engine-rest/process-instance/{processId}/variables/bookedCar";
            var body = $"{{\"value\": \"{bookedCar}\", \"type\": \"Boolean\"}}";
            MakeRequest(URL, "PUT", "application/json", body);

            URL = $"http://localhost:8080/engine-rest/process-instance/{processId}/variables/carBookingURL";
            body = $"{{\"value\": \"{carBookingURL}\", \"type\": \"String\"}}";
            MakeRequest(URL, "PUT", "application/json", body);


        }

        public static void CamundaConfirmOrder(string processId, bool confirmedOrder)
        {
            var URL = $"http://localhost:8080/engine-rest/process-instance/{processId}/variables/confirmedOrder";

            var body = $"{{\"value\": \"{confirmedOrder}\", \"type\": \"Boolean\"}}";

            MakeRequest(URL, "PUT", "application/json", body);

        }


        private static HttpWebResponse MakeRequest(string URL, string method, string contentType, string body = null)
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
