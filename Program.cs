using System;
using System.Collections.Generic;
using static Biller.User;
using static Biller.CDR;

namespace Biller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int PhoneNo;
            string month;

            Console.WriteLine("Enter the Phone Number to Generate the Bill : ");
            PhoneNo = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Month : ");
            month = Console.ReadLine();

            User user = new User(); 
            User user1 = new User("chamika", "perera", ".chamikamadampe", "A", 0717291782, DateTime.Now);
            User user2 = new User("sriyani", "perera", "madampe", "A", 0779617149, DateTime.Now);

            user.AddtoUserList(user1);
            user.AddtoUserList(user2);

            CDR cdr1 = new CDR(0717291782, 0718331022, DateTime.Now, 100);
            CDR cdr2 = new CDR(0717291782, 0718331022, DateTime.Now, 250);
            CDR cdr3 = new CDR(0717291782, 0728331022, new DateTime(2022, 10, 3, 8, 40, 08), 5000);

            CDR cdr4 = new CDR(0779617149, 0728331022, DateTime.Now, 300);

            Bill b1 = new Bill();

            user1.AddtoCDRList(cdr1, month);
            user1.AddtoCDRList(cdr2, month);
            user1.AddtoCDRList(cdr3, month);

            user2.AddtoCDRList(cdr4, month);


            b1.PrintBill(user.getUser(PhoneNo));



        }
    }
}
