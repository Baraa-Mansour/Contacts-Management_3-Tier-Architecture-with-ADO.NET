using Contacts_Contracts.DTOs;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Contacts_DAL
{
    public class ContactDA
    {
        private static ContactDto _convertReader(SqlDataReader reader)
        {
            ContactDto dto = new ContactDto();

            dto.ContactID = (int)reader["ContactID"];
            dto.FirstName = (string)reader["FirstName"];
            dto.LastName = (string)reader["LastName"];
            dto.Email = (string)reader["Email"];
            dto.Phone = (string)reader["Phone"];
            dto.Address = (string)reader["Address"];
            dto.DateOfBirth = (DateTime)reader["DateOfBirth"];
            dto.CountryId = (int)reader["CountryID"];

            object ImagePath = reader["ImagePath"];
            dto.ImagePath = ImagePath == DBNull.Value ? null : (string)ImagePath;

            return dto;
        }

        public static ContactDto FindById(int ContactId)
        {
            ContactDto contact = null;

            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            string query = @"SELECT * FROM Contacts WHERE ContactID = @ContactId";
            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ContactId", ContactId);

                conn.Open();
                reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    contact = _convertReader(reader);
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

            return contact;
        }

        public static int Add(ContactDto contact)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            string query = @"INSERT INTO Contacts
                             VALUES (@firstName, @lastName, @email, @phone, @address, @dateOfBirth, @countryID, @imagePath);
                             SELECT SCOPE_IDENTITY();";
                             
            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@firstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@lastName", contact.LastName);
                cmd.Parameters.AddWithValue("@email", contact.Email);
                cmd.Parameters.AddWithValue("@phone", contact.Phone);
                cmd.Parameters.AddWithValue("@address", contact.Address);
                cmd.Parameters.AddWithValue("@dateOfBirth", contact.DateOfBirth);
                cmd.Parameters.AddWithValue("@countryID", contact.CountryId);
                cmd.Parameters.AddWithValue(
                    "@imagePath",
                    string.IsNullOrWhiteSpace(contact.ImagePath) ? DBNull.Value : (object)contact.ImagePath
                );

                conn.Open();
                object result = cmd.ExecuteScalar();

                if(result != DBNull.Value)
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

        public static bool Update(ContactDto contact)
        {
            int rowsAffected = 0;

            SqlConnection conn = null;
            SqlCommand cmd = null;

            string query = @"UPDATE Contacts
                             SET FirstName = @firstName,
                                 LastName = @lastName,
                                 Email = @email,
                                 Phone = @phone,
                                 Address = @address,
                                 DateOfBirth = @dateOfBirth,
                                 CountryID = @countryID, 
                                 ImagePath = @imagePath
                             WHERE ContactID = @contactID;";

            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@firstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@lastName", contact.LastName);
                cmd.Parameters.AddWithValue("@email", contact.Email);
                cmd.Parameters.AddWithValue("@phone", contact.Phone);
                cmd.Parameters.AddWithValue("@address", contact.Address);
                cmd.Parameters.AddWithValue("@dateOfBirth", contact.DateOfBirth);
                cmd.Parameters.AddWithValue("@countryID", contact.CountryId);
                cmd.Parameters.AddWithValue(
                    "@imagePath",
                    string.IsNullOrWhiteSpace(contact.ImagePath) ? DBNull.Value : (object)contact.ImagePath
                );
                cmd.Parameters.AddWithValue("@contactID", contact.ContactID);

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

        public static bool Delete(int contactId)
        {
            int rowsAffected = 0;

            SqlConnection conn = null;
            SqlCommand cmd = null;

            string query = @"DELETE Contacts
                             WHERE ContactID = @contactID;";

            try
            {
                conn = new SqlConnection(DataAccessSettings.connectionString);
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@contactID", contactId);

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

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();

            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            string query = @"SELECT * FROM Contacts";
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

        public static bool IsExist(int contactId)
        {
            bool IsFound = false;
            string query = @"SELECT 1 FROM Contacts
                             WHERE ContactID = @contactId;";

            SqlConnection conn = new SqlConnection(DataAccessSettings.connectionString);
            SqlCommand cmd = new SqlCommand(@query, conn);

            cmd.Parameters.AddWithValue("@contactId", contactId);

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
