using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    public class User
    {

        public string FName { get; set; }
        public string LName { get; set; }
        public string BillingAddress { get; set; }
        public string PackageCode { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime RegisteredDate { get; set; }

        private  List<CDR> Calls = new List<CDR>();
        private  List<User> Users = new List<User>();
        public User user { get; set; }
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

        public string GetFullName()
        {
            return FName + " " + LName;
        }

        public void AddtoCDRList(CDR cdr)
        {
            
            if (cdr.CallerPhoneNumber == PhoneNumber)
            {
                Console.WriteLine(PhoneNumber + "thi sis calleer");
                Calls.Add(cdr);
                  
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

        public  void AddtoUserList(User user)
        {
            Users.Add(user);
        }
        public List<User> GetUserList()
        {
            return Users;
        }

        public void PrintCallsList(User user)
        {
            if (user.PhoneNumber == PhoneNumber)
            {
                {
                    Console.WriteLine("Call Records for " + user.PhoneNumber + " for the month of March 2022\n");
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

        public User getUser(int PhoneNo)
        {

            foreach (var obj in Users)
            {
                if (obj.PhoneNumber == PhoneNo)
                {
                    user = obj;
                }
            }
            return user;

        }


    }
}
