using System;


namespace Contacts_Contracts.DTOs
{
    public class ContactDto
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryId { get; set; }
        public string ImagePath { get; set; }
    }
}
