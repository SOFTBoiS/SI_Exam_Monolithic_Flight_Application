using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SI_Exam_Monolithic_Flight_Application.Models.Data
{
    public class DataAccessObject
    {
        private string _sqlServer, _database, _trustedConn, _user, _password, _connString;

        public DataAccessObject(string sqlServer, string database, string trustedConn, string user = null, string password = null)
        {
            _sqlServer = sqlServer;
            _database = database;
            _trustedConn = trustedConn;
            _user = user;
            _password = password;
            _connString = $"Data Source={_sqlServer};Initial Catalog={_database};"
                          + $"Integrated Security={_trustedConn};";
            //if there is a username and password, add them to the connection string
            if (!(String.IsNullOrEmpty(user) && String.IsNullOrEmpty(password)))
            {
                _connString += $"User Id={user}; Password={password};";
            }
        }

        public Collection<FlightSearchModel> GetFlights(string departureAirport, string arrivalAirport)
        {
            var flights = new Collection<FlightSearchModel>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
          
                    conn.Open();
                    var query = new SqlCommand(
                        "SELECT * FROM dbo.Flight where departure_airport = @departure AND arrival_airport = @arrival", conn);
                    query.Parameters.AddWithValue("@departure", departureAirport);
                    query.Parameters.AddWithValue("@arrival", arrivalAirport);
                    var result = query.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            var id = result.GetInt32(result.GetOrdinal("id"));
                            var depAirport = result.GetString(result.GetOrdinal("departure_airport"));
                            var arrAirport = result.GetString(result.GetOrdinal("arrival_airport"));
                            var image = result.GetString(result.GetOrdinal("image"));
                            var time = result.GetString(result.GetOrdinal("time"));
                            var flight = new FlightSearchModel(id, depAirport, arrAirport, image, time);
                            flights.Add(flight);
                        }
                    }

      
            }

            return flights;
        }
    }
}
