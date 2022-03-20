using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    public class Bill
    {
        private User user { get; set; }
        private double TotalCallCharges { get; set; }
        private double TotalDiscount { get; set; }
        private double TotalTax { get; set; }
        private double Rental { get; set; }
        private double Billamount { get; set; }

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

            Package packageA = new Package("A", 100, "PER_MINUTE", 20, 8, 8, 20, 3, 5, 2, 4);
            Package packageB = new Package("B", 100, "PER_SECOND", 20, 8, 8, 20, 4, 6, 3, 5);
            Package packageC = new Package("C", 300, "PER_MINUTE", 20, 8, 8, 20, 2, 3, 1, 2);
            Package packageD = new Package("D", 399, "PER_SECOND", 20, 8, 8, 20, 3, 5, 2, 4);

            switch (user.PackageCode)
            {
                case "A":
                    bill.Rental = packageA.MonthlyRental;
                    CalculateCost(user, bill, packageA);
                    break;
                case "B":
                    bill.Rental = packageA.MonthlyRental;
                    CalculateCost(user, bill, packageB);
                    break;
                case "C":
                    bill.Rental = packageA.MonthlyRental;
                    CalculateCost(user, bill, packageC);
                    break;
                case "D":
                    bill.Rental = packageA.MonthlyRental;
                    CalculateCost(user, bill, packageD);
                    break;
                default:
                    throw new Exception();
            }


            return bill;
        }

        private static void CalculateCost(User user, Bill bill, Package pkg)
        {
            foreach (CDR obj in user.GetCallsList())
            {
                double NoOfMins = 0.00;
                if (pkg.BillingType == "PER_MINUTE")
                {
                    NoOfMins = obj.CallDuration / 60;
                }
                else
                {
                    NoOfMins = obj.CallDuration / 3600;
                }


                if (obj.IsPeakHour(obj) && obj.IsLocalCall(obj))    // peak hour and local call
                {
                    double CallFare = (double)((int)Math.Round(NoOfMins) * pkg.PeakandLocalCharge);
                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (obj.IsPeakHour(obj) && !obj.IsLocalCall(obj))  // peak hour and long distance call
                {
                    double CallFare = (double)((int)Math.Round(NoOfMins) * pkg.PeakandLongDistanceCharge);
                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (!obj.IsPeakHour(obj) && obj.IsLocalCall(obj))  // off -peak hour and local call
                {
                    double CallFare = (double)((int)Math.Round(NoOfMins) * pkg.OffPeakandLocalCharge);
                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (!obj.IsPeakHour(obj) && !obj.IsLocalCall(obj)) // off -peak hour and long distance call
                {
                    double CallFare = (double)((int)Math.Round(NoOfMins) * pkg.OffPeakandLongDistanceCharge);
                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
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
        }


        public void PrintBill(User user)
        {
            Console.WriteLine("--------------------------------------------------------------------------------- \n");

            Console.WriteLine("Bill for " + user.GetFullName() + " for the month of March 2022\n");

            Console.WriteLine("Customer Name                : " + user.GetFullName());
            Console.WriteLine("Billed to                    : " + user.BillingAddress);
            Console.WriteLine("Total call charges           : " + "Rs." + CalculateBill(user).TotalCallCharges);
            Console.WriteLine("Total discount               : " + "Rs." + CalculateBill(user).TotalDiscount);
            Console.WriteLine("Total Tax                    : " + "Rs." + CalculateBill(user).TotalTax);
            Console.WriteLine("Rental                       : " + "Rs." + CalculateBill(user).Rental);
            Console.WriteLine("Bill amount                  : " + "Rs." + CalculateBill(user).Billamount + "\n");

            Console.WriteLine("--------------------------------------------------------------------------------- \n");

            user.PrintCallsList(user);
        }

    }


}
