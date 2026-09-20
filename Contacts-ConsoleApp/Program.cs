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
            TestGetAllContact();
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
    }
}
