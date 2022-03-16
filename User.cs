using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    internal class User
    {

        public string FName { get; set; }
        public string LName { get; set; }
        public string BillingAddress { get; set; }
        public string PackageCode { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime RegisteredDate { get; set; }

        List<CDR> Calls = new List<CDR>();

        public User()
        {

        }

        public User(string fName, string lName, string billingAddress, string packageCode, int phoneNumber, DateTime registeredDate)
        {
            FName = fName;
            LName = lName;
            BillingAddress = billingAddress;
            PackageCode = packageCode;
            PhoneNumber = phoneNumber;
            RegisteredDate = registeredDate;
        }

        public string GetFullName(string fname, string lname)
        {
            return fname + " " + lname;
        }

        public void AddtoCDRList(CDR cdr)
        {

            if (cdr.CallerPhoneNumber == PhoneNumber)
            {
                Calls.Add(cdr);
              //  Console.WriteLine("this is the callee > " + cdr.CalleePhoneNumber); // To ensure the record is added
            }
            else
            {
                throw new Exception();
            }
        }

        public List<CDR> GetCallsList()
        {
            return Calls;
        }

        public void PrintCallsList()
        {
            Console.WriteLine("Call Records for " + PhoneNumber + " for the month of March 2022\n");
            foreach (var obj in Calls)
            {
                Console.WriteLine("Callee Phone Number         : " + obj.CalleePhoneNumber);
                Console.WriteLine("Call Duration               : " + obj.CallDuration + " seconds");
                Console.WriteLine("Call Start Time             : " + obj.CallStartTime);
                Console.WriteLine("Call Charge                 : " + "Rs." + obj.Charge + "\n");
            }
            Console.WriteLine("--------------------------------------------------------------------------------- \n");
        }


    }
}
