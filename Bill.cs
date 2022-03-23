using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    public delegate bool CheckCallDelegate(CDR obj);
    public delegate bool CheckHourDelegate(CDR obj, int a, int b);

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
        public double BillAmount { get { return billamount; } set { billamount = value; } }

        public Bill()
        {

        }

        public Bill(double totalCallCharges, double totalDiscount, double totalTax, double rental, double billamount)
        {
            TotalCallCharges = totalCallCharges;
            TotalDiscount = totalDiscount;
            TotalTax = totalTax;
            Rental = rental;
            BillAmount = billamount;
        }

        public Bill CalculateBill(User user)
        {
            Bill bill = new Bill();

            Package packageA = new Package("A", 100.0, "PER_MINUTE", 18, 10, 10, 18, 3, 5, 2, 4);
            Package packageB = new Package("B", 100.0, "PER_SECOND", 20, 8, 8, 20, 4, 6, 3, 5);
            Package packageC = new Package("C", 300.0, "PER_MINUTE", 21, 9, 9, 21, 2, 3, 1, 2);
            Package packageD = new Package("D", 399.0, "PER_SECOND", 20, 8, 8, 20, 3, 5, 2, 4);

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
                double CallFare = 0.00;
                CheckCallDelegate CheckLocalCallDelegate = new CheckCallDelegate(obj.IsLocalCall);
                CheckHourDelegate CheckPeakHourDelegate = new CheckHourDelegate(obj.IsPeakHour);

                if (pkg.BillingType == "PER_MINUTE")
                {
                    NoOfMins = obj.CallDuration / 60;
                }
                else
                {
                    NoOfMins = obj.CallDuration / 3600;
                }


                if (CheckPeakHourDelegate(obj, pkg.PeakStartHour, pkg.PeakEndHour) && CheckLocalCallDelegate(obj))          // peak hour and local call
                {

                    CallFare = (double)((int)Math.Round(NoOfMins) * pkg.PeakandLocalCharge);

                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (CheckPeakHourDelegate(obj, pkg.PeakStartHour, pkg.PeakEndHour) && !CheckLocalCallDelegate(obj))    // peak hour and long distance call
                {
                    CallFare = (double)((int)Math.Round(NoOfMins) * pkg.PeakandLongDistanceCharge);

                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (!CheckPeakHourDelegate(obj, pkg.PeakStartHour, pkg.PeakEndHour) && CheckLocalCallDelegate(obj))    // off -peak hour and local call
                {
                    CallFare = (double)((int)Math.Round(NoOfMins) * pkg.OffPeakandLocalCharge);

                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else if (!CheckPeakHourDelegate(obj, pkg.PeakStartHour, pkg.PeakEndHour) && !CheckLocalCallDelegate(obj))   // off -peak hour and long distance call
                {
                    CallFare = (double)((int)Math.Round(NoOfMins) * pkg.OffPeakandLongDistanceCharge);

                    obj.Charge = CallFare;
                    bill.TotalCallCharges += CallFare;
                }
                else
                {
                    throw new Exception();
                }

                bill.TotalTax = (bill.TotalCallCharges + bill.Rental) * (20.0 / 100);

                if (bill.TotalCallCharges > 1000 && (pkg.PackageCode == "A" || pkg.PackageCode == "C"))
                {
                    bill.TotalDiscount = bill.TotalCallCharges * (40.0 / 100);
                }

                bill.BillAmount = (bill.TotalCallCharges + bill.TotalTax + bill.Rental) - bill.TotalDiscount;

            }

            if (user.GetCallsList().Count < 1)
            {
                bill.BillAmount = bill.Rental;
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
                             + "Bill amount                  : " + "Rs." + CalculateBill(user).BillAmount + "\n");

            Console.WriteLine("--------------------------------------------------------------------------------- \n");

            user.PrintCallsList(user, month);
        }

    }


}
