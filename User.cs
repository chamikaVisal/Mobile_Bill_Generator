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

        private string fName;
        public string FName { get { return fName; } set { fName = value; } }

        private string lName;
        public string LName { get { return lName; } set { lName = value; } }

        private string billingAddress;
        public string BillingAddress { get { return billingAddress; } set { billingAddress = value; } }

        private string packageCode;
        public string PackageCode { get { return packageCode; } set { packageCode = value; } }

        private int phoneNumber;
        public int PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }

        private DateTime registeredDate;
        public DateTime RegisteredDate { get { return registeredDate; } set { registeredDate = value; } }

        private List<CDR> calls = new List<CDR>();
        public List<CDR> Calls { get { return calls; }set { calls = value; }  }

        private List<User>users = new List<User>();
        public List<User> Users { get { return users; } set { users = value; } }

        private User userobj;
        public User user { get { return userobj; } set { userobj = value; } }

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

        public void AddtoCDRList(CDR cdr, string month)
        {
            if (cdr.CallerPhoneNumber == PhoneNumber && cdr.CallStartTime.ToString("MMMM").ToLower() == month)
            {
                Calls.Add(cdr);
            }

        }

        public List<CDR> GetCallsList()
        {
            return Calls;
        }

        public void AddtoUserList(User user)
        {
            Users.Add(user);
        }
        public List<User> GetUserList()
        {
            return Users;
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

        public void PrintCallsList(User user, string month)
        {
            if (user.PhoneNumber == PhoneNumber)
            {
                {
                    Console.WriteLine("Call Records for " + user.PhoneNumber + " for the month of " + month + " 2022\n");
                    foreach (var obj in Calls)
                    {
                        Console.WriteLine("Callee Phone Number         : " + obj.CalleePhoneNumber + "\n"
                                         +"Call Duration               : " + obj.CallDuration + " seconds" + "\n"
                                         +"Call Start Time             : " + obj.CallStartTime + "\n"
                                         +"Call Charge                 : " + "Rs." + obj.Charge + "\n");
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------- \n");
                }
            }
        }

        

    }
}
