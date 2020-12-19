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
    public class UserDataAccessObject
    {
        private string _sqlServer, _database, _trustedConn, _user, _password, _connString;

        /// <summary>
        /// The default constructor using environment variables to setup a database connection. Use this for production.
        /// </summary>
        public UserDataAccessObject()
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
        public UserDataAccessObject(string sqlServer, string database, string trustedConn, string user = null, string password = null)
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

        public (int, string) CreateUser(string username, string password, string name, string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    var query = new SqlCommand(
                        "INSERT INTO [User] (username, name, password, email) VALUES (@username, @name, @password, @email);SELECT CAST(SCOPE_IDENTITY() AS INT) AS LAST_IDENTITY",
                        conn);
                    query.Parameters.AddWithValue("@username", username);
                    query.Parameters.AddWithValue("@name", name);
                    query.Parameters.AddWithValue("@password", password);
                    query.Parameters.AddWithValue("@email", email);
                    conn.Open();
                    // Return the ID of the newly inserted booking
                    var id = (int) query.ExecuteScalar();

                    return (id, username);
                }
            }
            catch (SqlException sqlE)
            {
                if (sqlE.Number == 2601)
                {
                    throw new UserTakenException("A user with that username or email already exists");
                }
                throw;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine(e.GetType());
                throw new Exception("Something went wrong. Please try again.");
            }
        }

        public (int, string) RetrieveUser(string username, string password)
        {
            try
            {
                int id = -1;
                string usrname = "";
                using (SqlConnection conn = new SqlConnection(_connString))
                {

                    var query = new SqlCommand(
                        "SELECT id, username FROM [User] where username = @username AND password = @password",
                        conn);
                    query.Parameters.AddWithValue("@username", username);
                    query.Parameters.AddWithValue("@password", password);
                    conn.Open();
                    var result = query.ExecuteReader();
                    if (result.HasRows)
                    {
                        result.Read();
                        id = result.GetInt32(result.GetOrdinal("id"));
                        usrname = result.GetString(result.GetOrdinal("username"));
                    }
                    else
                    {
                        throw new UserNotFoundException("Username or password incorrect.");
                    }
                }
                return (id, usrname);

            }

            catch(UserNotFoundException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                Debug.Write(e);
                throw new Exception("Something went wrong. Please try again.");
            }

        }

        public Collection<(string, string)> GetUsersBasedOnParameter(string parameter, string parameterValue)
        {
            try
            {

                var users = new Collection<(string, string)>();
                using (SqlConnection conn = new SqlConnection(_connString))
                {

                    var query = new SqlCommand(
                        $"SELECT email FROM [User] JOIN [Booking] ON {parameter} = @parameterValue",
                        conn);
                    query.Parameters.AddWithValue("@parameterValue", parameterValue);
                    conn.Open();
                    var result = query.ExecuteReader();
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            var name = result.GetString(result.GetOrdinal("name"));
                            var email = result.GetString(result.GetOrdinal("email"));

                            users.Add((email, name));
                        }
                    }

                }

                return users;
            }
            catch (Exception e)
            {
                Debug.Write(e);
                throw new Exception("Something went wrong fetching flights. Please try again.");
            }

        }
    }
}
