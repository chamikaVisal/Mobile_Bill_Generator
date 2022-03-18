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
            User user1 = new User("chamika", "perera", "madampe", "A", 0717291782, DateTime.Now);
            User user2 = new User("sriyani", "perera", "madampe", "A", 0779617149, DateTime.Now);

            CDR cdr1 = new CDR(0717291782, 0718331022, DateTime.Now, 100);
            CDR cdr2 = new CDR(0717291782, 0718331022, DateTime.Now, 250);
            CDR cdr3 = new CDR(0717291782, 0728331022, DateTime.Now, 5000);

            CDR cdr4 = new CDR(0779617149, 0728331022, DateTime.Now, 300);

            //user1 should inlucde the call records cdr1 | cdr2 | cdr3  as well 


            Bill b1 = new Bill();
            Bill b2 = new Bill();

            user1.AddtoCDRList(cdr1);
            user1.AddtoCDRList(cdr2);
            user1.AddtoCDRList(cdr3);

            user2.AddtoCDRList(cdr4);


            b1.PrintBill(user1);
            user1.PrintCallsList();

            b2.PrintBill(user2);
            user2.PrintCallsList();



        }
    }
}
