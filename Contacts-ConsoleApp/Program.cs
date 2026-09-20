using System;
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
            TestFindContactById(1);
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
    }
}
