using Contacts_Contracts.DTOs;
using System;
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
    }
}
