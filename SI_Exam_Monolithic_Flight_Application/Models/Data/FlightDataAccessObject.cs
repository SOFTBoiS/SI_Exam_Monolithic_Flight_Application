using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SI_Exam_Monolithic_Flight_Application.Models.DTOs;

namespace SI_Exam_Monolithic_Flight_Application.Models.Data
{
    public class FlightDataAccessObject
    {
        private string _sqlServer, _database, _trustedConn, _user, _password, _connString;

        /// <summary>
        /// The default constructor using environment variables to setup a database connection. Use this for production.
        /// </summary>
        public FlightDataAccessObject()
        {
            _sqlServer = Environment.GetEnvironmentVariable("SI_EXAM_SERVER");
            _database = Environment.GetEnvironmentVariable("SI_EXAM_DB_NAME");
            _trustedConn = "true";
            _connString = $"Data Source={_sqlServer};Initial Catalog={_database};"
                          + $"Integrated Security={_trustedConn};";
        }

        /// <summary>
        /// Use this constructor for testing purposes or if you need to connect to a database other than the default one.
        /// </summary>
        /// <param name="sqlServer"></param>
        /// <param name="database"></param>
        /// <param name="trustedConn"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public FlightDataAccessObject(string sqlServer, string database, string trustedConn, string user = null, string password = null)
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
            try
            {

                var flights = new Collection<FlightSearchModel>();
                using (SqlConnection conn = new SqlConnection(_connString))
                {

                    var query = new SqlCommand(
                        "SELECT * FROM dbo.Flight where departure_airport = @departure AND arrival_airport = @arrival",
                        conn);
                    query.Parameters.AddWithValue("@departure", departureAirport);
                    query.Parameters.AddWithValue("@arrival", arrivalAirport);
                    conn.Open();
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
                            var price = result.GetInt64(result.GetOrdinal("price"));
                            var flight = new FlightSearchModel(id, depAirport, arrAirport, image, time, price, null, null);
                            flights.Add(flight);
                        }
                    }

                }

                return flights;
            }
            catch (Exception e)
            {
                Debug.Write(e);
                throw new Exception("Something went wrong fetching flights. Please try again.");
            }

        }

        public int ReserveFlight(int userId, int flightId, long price, int passengers)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    var query = new SqlCommand(
                        "INSERT INTO dbo.Booking (user_id, flight_id, price, passengers, status) VALUES (@userId, @flightId, @price, @passengers, @status);SELECT CAST(SCOPE_IDENTITY() AS INT) AS LAST_IDENTITY",
                        conn);
                    query.Parameters.AddWithValue("@userId", userId);
                    query.Parameters.AddWithValue("@flightId", flightId);
                    query.Parameters.AddWithValue("@price", price);
                    query.Parameters.AddWithValue("@passengers", passengers);
                    query.Parameters.AddWithValue("@status", FLIGHT_STATUS.RESERVED.ToString());
                    conn.Open();
                    // Return the ID of the newly inserted booking
                    var id = query.ExecuteScalar();
                    Debug.WriteLine("=======================");
                    Debug.WriteLine(id);
                    Debug.WriteLine("=======================");
                    return (int) id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw new Exception("Something went wrong with the booking. Please try again.");
            }
        }

        public int UpdateBookingStatus(long bookingId, FLIGHT_STATUS newStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    var query = new SqlCommand(
                        "Update dbo.Booking SET status = @newStatus WHERE id = @bookingId;",
                        conn);
                    query.Parameters.AddWithValue("@newStatus", newStatus.ToString());
                    query.Parameters.AddWithValue("@bookingId", bookingId);
                    conn.Open();
                    // Return the ID of the newly inserted booking
                    var status = query.ExecuteNonQuery();
                    if (status != 1)
                    {
                        throw new Exception("Tried to cancel a non-existing booking.");   
                    }

                    return status;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw new Exception("Something went wrong with the booking. Please try again.");
            }
        }

    }
}
