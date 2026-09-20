using System;
using System.Data;
using Contacts_BLL;
using Contacts_Contracts.DTOs;


namespace Contacts_ConsoleApp
{
    internal class Program
    {
        static public void PrintContact(ContactDto dto)
        {
            Console.WriteLine();
            Console.WriteLine("----------");
            Console.WriteLine($"Id: {dto.ContactID}");

            Console.WriteLine();
            Console.WriteLine($"Name: {dto.FirstName} {dto.LastName}");
            Console.WriteLine($"Email: {dto.Email}");
            Console.WriteLine($"Phone: {dto.Phone}");
            Console.WriteLine($"Address: {dto.Address}");
            Console.WriteLine($"Country Id: {dto.CountryId}");
            Console.WriteLine($"Date Of Birth: {dto.DateOfBirth}");
            Console.WriteLine($"Image Path: {dto.ImagePath??"Not Found!"}");
            Console.WriteLine("----------");
        }


        static void Main(string[] args)
        {
            // TestFindCountryById(5);
            // TestAddCountry();
            // TestUpdateCountry();
            // TestDeleteCountry();
            // TestGetAllCountries();
            // TestCountryIsExist();
            // TestFindCountryByName("Germany");
            // TestCountryIsExistByName("Germany");
        }


        static void TestFindContactById(int Id)
        {
            ContactBL contact = ContactBL.FindById(Id);

            if(contact == null)
            {
                Console.WriteLine("NOT Found!");
                
            }
            else
            {
                PrintContact(contact.contactDto);
            }
        }
        static void TestAddContact()
        {
            ContactDto dto = new ContactDto();
            dto.FirstName = "name";
            dto.LastName = "last name";
            dto.Email = "email";
            dto.Phone = "1234567890";
            dto.Address = "address";
            dto.CountryId = 1;
            dto.DateOfBirth = DateTime.Now;
            dto.ImagePath = null;

            ContactBL contact = new ContactBL(dto);

            if (contact.Save())
                Console.WriteLine($"Added Successfully, ID = {contact.contactDto.ContactID}");
            else
                Console.WriteLine("SomeThing Went Wrong!");
        }
        static void TestUpdateContact()
        {
            ContactBL contact = ContactBL.FindById(14);

            if (contact == null)
            {
                Console.WriteLine("Not Found!");
                return;
            }

            ContactDto dto = contact.contactDto;
            dto.FirstName = "Updated name";
            dto.LastName = "Updated last name";
            dto.Email = "email";
            dto.Phone = "1234567890";
            dto.Address = "address";
            dto.CountryId = 1;
            dto.DateOfBirth = DateTime.Now;
            dto.ImagePath = "Updated image path";


            if (contact.Save())
                Console.WriteLine($"Updated Successfully, ID = {contact.contactDto.ContactID}");
            else
                Console.WriteLine("SomeThing Went Wrong!");
        }
        static void TestDeleteContact()
        {
            if (ContactBL.Delete(14))
                Console.WriteLine("Deleted!");
            else
                Console.WriteLine("Something Went Wrong");
        }
        static void TestGetAllContact()
        {
            DataTable dt = ContactBL.GetAllContacts();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Not Content");
                return;
            }

            Console.WriteLine("Contacts Info: ");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["ContactId"]}: {row["FirstName"]} {row["LastName"]}");
            }
            
        }
        static void TestIsExist()
        {
            if (ContactBL.IsExist(9))
                Console.WriteLine("Found!");
            else
                Console.WriteLine("Not Found!");
        }


        static public void PrintCountry(CountryDto dto)
        {
            Console.WriteLine();
            Console.WriteLine("----------");
            Console.WriteLine($"Id: {dto.CountryID}");
            Console.WriteLine($"Name: {dto.CountryName}");
            Console.WriteLine("----------");
        }
        static void TestFindCountryById(int Id)
        {
            CountryBL country = CountryBL.FindById(Id);

            if (country == null)
            {
                Console.WriteLine("NOT Found!");
            }
            else
            {
                PrintCountry(country.countryDto);
            }
        }
        static void TestAddCountry()
        {
            CountryDto dto = new CountryDto();
            dto.CountryName = "Country name";

            CountryBL country = new CountryBL(dto);

            if (country.Save())
                Console.WriteLine($"Added Successfully, ID = {country.countryDto.CountryID}");
            else
                Console.WriteLine("SomeThing Went Wrong!");
        }
        static void TestUpdateCountry()
        {
            CountryBL country = CountryBL.FindById(6);

            if (country == null)
            {
                Console.WriteLine("Not Found!");
                return;
            }

            CountryDto dto = country.countryDto;
            dto.CountryName = "Updated country name";

            if (country.Save())
                Console.WriteLine($"Updated Successfully, ID = {country.countryDto.CountryID}");
            else
                Console.WriteLine("SomeThing Went Wrong!");
        }
        static void TestDeleteCountry()
        {
            if (CountryBL.Delete(6))
                Console.WriteLine("Deleted!");
            else
                Console.WriteLine("Something Went Wrong");
        }
        static void TestGetAllCountries()
        {
            DataTable dt = CountryBL.GetAllCountries();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Not Content");
                return;
            }

            Console.WriteLine("Countries Info: ");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["CountryID"]}: {row["CountryName"]}");
            }
        }
        static void TestCountryIsExist()
        {
            if (CountryBL.IsExist(6))
                Console.WriteLine("Found!");
            else
                Console.WriteLine("Not Found!");
        }
        static void TestFindCountryByName(string name)
        {
            CountryBL country = CountryBL.FindByName(name);

            if (country == null)
            {
                Console.WriteLine("NOT Found!");
            }
            else
            {
                PrintCountry(country.countryDto);
            }
        }
        static void TestCountryIsExistByName(string name)
        {
            if (CountryBL.IsExistByName(name))
                Console.WriteLine("Found!");
            else
                Console.WriteLine("Not Found!");
        }
    }
}
