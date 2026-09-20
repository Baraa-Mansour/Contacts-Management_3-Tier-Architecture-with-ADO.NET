using Contacts_Contracts.DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Contacts_DAL
{
    public class CountryDA
    {
        private static CountryDto _convertReader(SqlDataReader reader)
        {
            CountryDto dto = new CountryDto();

            dto.CountryID = (int)reader["CountryID"];
            dto.CountryName = (string)reader["CountryName"];

            return dto;
        }

        public static CountryDto FindById(int CountryId)
        {
            CountryDto country = null;

            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            string query = @"SELECT * FROM Countries WHERE CountryID = @CountryId";
            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@CountryId", CountryId);

                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    country = _convertReader(reader);
                }
            }
            catch
            {
                // Log The Exception
            }
            finally
            {
                reader?.Close();
                conn?.Close();
            }

            return country;
        }

        public static int Add(CountryDto country)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            string query = @"INSERT INTO Countries
                             VALUES (@countryName);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@countryName", country.CountryName);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }
            catch
            {
                // Log The Exception
            }
            finally
            {
                conn?.Close();
            }

            return -1;
        }

        public static bool Update(CountryDto country)
        {
            int rowsAffected = 0;

            SqlConnection conn = null;
            SqlCommand cmd = null;

            string query = @"UPDATE Countries
                             SET CountryName = @countryName
                             WHERE CountryID = @countryID;";

            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@countryName", country.CountryName);
                cmd.Parameters.AddWithValue("@countryID", country.CountryID);

                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch
            {
                // Log The Exception
            }
            finally
            {
                conn?.Close();
            }

            return rowsAffected != 0;
        }

        public static bool Delete(int countryId)
        {
            int rowsAffected = 0;

            SqlConnection conn = null;
            SqlCommand cmd = null;

            string query = @"DELETE Countries
                             WHERE CountryID = @countryID;";

            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@countryID", countryId);

                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch
            {
                // Log The Exception
            }
            finally
            {
                conn?.Close();
            }

            return rowsAffected != 0;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            string query = @"SELECT * FROM Countries";
            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch
            {
                // Log The Exception
            }
            finally
            {
                reader?.Close();
                conn?.Close();
            }

            return dt;
        }

        public static bool IsExist(int countryId)
        {
            bool IsFound = false;
            string query = @"SELECT 1 FROM Countries
                             WHERE CountryID = @countryId;";

            SqlConnection conn = new SqlConnection(DataAccessSettings.connectionString);
            SqlCommand cmd = new SqlCommand(@query, conn);

            cmd.Parameters.AddWithValue("@countryId", countryId);

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();
                IsFound = (result != null);
            }
            catch
            {
                // Log Error
            }
            finally
            {
                conn?.Close();
            }

            return IsFound;
        }
    }
}