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
        private double totalCallCharges;
        public double TotalCallCharges { get { return totalCallCharges; } set { totalCallCharges = value; } }

        private double totalDiscount;
        public double TotalDiscount { get { return totalDiscount; } set { totalDiscount = value; } }

        private double totalTax;
        public double TotalTax { get { return totalTax; } set { totalTax = value; } }

        private double rental;
        public double Rental { get { return rental; } set { rental = value; } }

        private double billamount;
        public double Billamount { get { return billamount; } set { billamount = value; } }

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

            Package packageA = new Package("A", 100, "PER_MINUTE", 18, 10, 10, 18, 3, 5, 2, 4);
            Package packageB = new Package("B", 100, "PER_SECOND", 20, 8, 8, 20, 4, 6, 3, 5);
            Package packageC = new Package("C", 300, "PER_MINUTE", 21, 9, 9, 21, 2, 3, 1, 2);
            Package packageD = new Package("D", 399, "PER_SECOND", 20, 8, 8, 20, 3, 5, 2, 4);

            switch (user.PackageCode)
            {
                case "A":
                    bill.Rental = packageA.MonthlyRental;
                    CalculateCost(user, bill, packageA);
                    break;
                case "B":
                    bill.Rental = packageB.MonthlyRental;
                    CalculateCost(user, bill, packageB);
                    break;
                case "C":
                    bill.Rental = packageC.MonthlyRental;
                    CalculateCost(user, bill, packageC);
                    break;
                case "D":
                    bill.Rental = packageD.MonthlyRental;
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


                if (obj.IsPeakHour(obj, pkg.PeakStartHour, pkg.PeakEndHour) && obj.IsLocalCall(obj))    // peak hour and local call
                {
   
                    double CallFare = (double)((int)Math.Round(NoOfMins) * pkg.PeakandLocalCharge);

                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (obj.IsPeakHour(obj, pkg.PeakStartHour, pkg.PeakEndHour) && !obj.IsLocalCall(obj))  // peak hour and long distance call
                {
                    double CallFare = (double)((int)Math.Round(NoOfMins) * pkg.PeakandLongDistanceCharge);

                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (!obj.IsPeakHour(obj, pkg.PeakStartHour, pkg.PeakEndHour) && obj.IsLocalCall(obj))  // off -peak hour and local call
                {
                    double CallFare = (double)((int)Math.Round(NoOfMins) * pkg.OffPeakandLocalCharge);

                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (!obj.IsPeakHour(obj, pkg.PeakStartHour, pkg.PeakEndHour) && !obj.IsLocalCall(obj)) // off -peak hour and long distance call
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

                if (bill.TotalCallCharges > 1000 && (pkg.PackageCode=="A" || pkg.PackageCode == "C"))
                {
                    bill.TotalDiscount = bill.TotalCallCharges * (40.0 / 100);
                }
                bill.Billamount = (bill.TotalCallCharges + bill.TotalTax + bill.Rental) - bill.TotalDiscount;
            }
        }

        public void PrintBill(User user, string month)
        {
            Console.WriteLine("--------------------------------------------------------------------------------- \n");

            Console.WriteLine("Bill for " + user.GetFullName() + " for the month of " + month + " 2022\n");

            Console.WriteLine("Customer Name                : " + user.GetFullName() + "\n"
                             + "Billed to                    : " + user.BillingAddress + "\n"
                             + "Total call charges           : " + "Rs." + CalculateBill(user).TotalCallCharges + "\n"
                             + "Total discount               : " + "Rs." + CalculateBill(user).TotalDiscount + "\n"
                             + "Total Tax                    : " + "Rs." + CalculateBill(user).TotalTax + "\n"
                             + "Rental                       : " + "Rs." + CalculateBill(user).Rental + "\n"
                             + "Bill amount                  : " + "Rs." + CalculateBill(user).Billamount + "\n");

            Console.WriteLine("--------------------------------------------------------------------------------- \n");

            user.PrintCallsList(user, month);
        }

    }


}
