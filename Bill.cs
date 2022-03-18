using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    internal class Bill
    {
        public User user { get; set; }
        public double TotalCallCharges { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalTax { get; set; }
        public double Rental { get; set; }
        public double Billamount { get; set; }

        public Bill()
        {

        }

        public Bill(double totalCallCharges, double totalDiscount, double totalTax, double rental, double billamount)
        {
            TotalCallCharges = totalCallCharges;
            TotalDiscount = totalDiscount;
            TotalTax = totalTax;
            Rental = rental;
            Billamount = billamount;
        }

        public Bill CalculateBill(User user)
        {
            Bill bill = new Bill();
            Package p1 = new Package(); // A B C D 

            switch (user.PackageCode)
            {
                case "A":
                    p1.MonthlyRental = 100;
                    bill.Rental = 100;

                    foreach (CDR obj in user.GetCallsList())
                    {
                        double NoOfMins = obj.CallDuration / 60;

                        if (obj.IsPeakHour(obj) && obj.IsLocalCall(obj))    // peak hour and local call
                        {
                            obj.Charge = (double)((int)Math.Round(NoOfMins) * 3);
                            bill.TotalCallCharges += (double)((int)Math.Round(NoOfMins) * 3);

                        }
                        else if (obj.IsPeakHour(obj) && !obj.IsLocalCall(obj))  // peak hour and long distance call
                        {
                            obj.Charge = (double)((int)Math.Round(NoOfMins) * 5);
                            bill.TotalCallCharges += (double)((int)Math.Round(NoOfMins) * 5);
                        }
                        else if (!obj.IsPeakHour(obj) && obj.IsLocalCall(obj))  // off -peak hour and local call
                        {
                            obj.Charge = (double)((int)Math.Round(NoOfMins) * 2);
                            bill.TotalCallCharges += (double)((int)Math.Round(NoOfMins) * 2);
                        }
                        else if (!obj.IsPeakHour(obj) && !obj.IsLocalCall(obj)) // off -peak hour and long distance call
                        {
                            obj.Charge = (double)((int)Math.Round(NoOfMins) * 4);
                            bill.TotalCallCharges += (double)((int)Math.Round(NoOfMins) * 4);
                        }
                        else
                        {
                            throw new Exception();
                        }
                        bill.TotalTax = bill.TotalCallCharges * (20.0 / 100);

                        if (bill.TotalCallCharges > 1000)
                        {
                            bill.TotalDiscount = bill.TotalCallCharges * (40.0 / 100);
                        }
                        bill.Billamount = (bill.TotalCallCharges + bill.TotalTax + bill.Rental) - bill.TotalDiscount;
                    }

                    break;
                case "B":
                 //   CalculateCharge(bill.Rental, obj);
                    break;
                case "C":
                    // code block
                    break;
                case "D":
                    // code block
                    break;
                default:
                    // code block
                    break;
            }


            return bill;
        }
        public static Bill CalculateCharge(double rental,CDR cdr,User user)
        {
            Bill bill = new Bill();
            double NoOfMins = cdr.CallDuration / 60;

            foreach (CDR obj in user.GetCallsList())
            {
                if (obj.IsPeakHour(obj) && obj.IsLocalCall(obj))    // peak hour and local call
                {
                    obj.Charge = (double)((int)Math.Round(NoOfMins) * 3);
                    bill.TotalCallCharges += (double)((int)Math.Round(NoOfMins) * 3);

                }
                else if (obj.IsPeakHour(obj) && !obj.IsLocalCall(obj))  // peak hour and long distance call
                {
                    obj.Charge = (double)((int)Math.Round(NoOfMins) * 5);
                    bill.TotalCallCharges += (double)((int)Math.Round(NoOfMins) * 5);
                }
                else if (!obj.IsPeakHour(obj) && obj.IsLocalCall(obj))  // off -peak hour and local call
                {
                    obj.Charge = (double)((int)Math.Round(NoOfMins) * 2);
                    bill.TotalCallCharges += (double)((int)Math.Round(NoOfMins) * 2);
                }
                else if (!obj.IsPeakHour(obj) && !obj.IsLocalCall(obj)) // off -peak hour and long distance call
                {
                    obj.Charge = (double)((int)Math.Round(NoOfMins) * 4);
                    bill.TotalCallCharges += (double)((int)Math.Round(NoOfMins) * 4);
                }
                else
                {
                    throw new Exception();
                }
                bill.TotalTax = bill.TotalCallCharges * (20.0 / 100);

                if (bill.TotalCallCharges > 1000)
                {
                    bill.TotalDiscount = bill.TotalCallCharges * (40.0 / 100);
                }
            }
            bill.Billamount = (bill.TotalCallCharges + bill.TotalTax + bill.Rental) - bill.TotalDiscount;
            return bill;
        }

        public void PrintBill(User user)
        {
            Console.WriteLine("--------------------------------------------------------------------------------- \n");

            Console.WriteLine("Bill for " + user.PhoneNumber + " for the month of March 2022\n");

            Console.WriteLine("Customer Name                : " + user.GetFullName());
            Console.WriteLine("Billed to                    : " + user.BillingAddress);
            Console.WriteLine("Total call charges           : " + "Rs." + CalculateBill(user).TotalCallCharges);
            Console.WriteLine("Total discount               : " + "Rs." + CalculateBill(user).TotalDiscount);
            Console.WriteLine("Total Tax                    : " + "Rs." + CalculateBill(user).TotalTax);
            Console.WriteLine("Rental                       : " + "Rs." + CalculateBill(user).Rental);
            Console.WriteLine("Bill amount                  : " + "Rs." + CalculateBill(user).Billamount + "\n");

            Console.WriteLine("--------------------------------------------------------------------------------- \n");
        }

    }


}
