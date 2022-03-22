using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    public class Package
    {

        private string packageCode;
        public string PackageCode { get { return packageCode; } set { packageCode = value; } }

        private double monthlyRental;
        public double MonthlyRental { get { return monthlyRental; } set { monthlyRental = value; } }

        private string billingType;
        public string BillingType { get { return billingType; } set { billingType = value; } }

        private string callType;
        public string CallType { get { return callType; } set { callType = value; } }

        private int offPeakStartHour;
        public int OffPeakStartHour { get { return offPeakStartHour; } set { offPeakStartHour = value; } }

        private int offPeakEndHour;
        public int OffPeakEndHour { get { return offPeakEndHour; } set { offPeakEndHour = value; } }

        private int peakStartHour;
        public int PeakStartHour { get { return peakStartHour; } set { peakStartHour = value; } }

        private int peakEndHour;
        public int PeakEndHour { get { return peakEndHour; } set { peakEndHour = value; } }

        private double peakandLocalCharge;
        public double PeakandLocalCharge { get { return peakandLocalCharge; } set { peakandLocalCharge = value; } }

        private double peakandLongDistanceCharge;
        public double PeakandLongDistanceCharge { get { return peakandLongDistanceCharge; } set { peakandLongDistanceCharge = value; } }

        private double offPeakandLocalCharge;
        public double OffPeakandLocalCharge { get { return offPeakandLocalCharge; } set { offPeakandLocalCharge = value; } }

        private double offPeakandLongDistanceCharge;
        public double OffPeakandLongDistanceCharge { get { return offPeakandLongDistanceCharge; } set { offPeakandLongDistanceCharge = value; } }


        public Package(string packageCode, double monthlyRental, string billingType, int offPeakStartHour, int offPeakEndHour, int peakStartHour, int peakEndHour, double peakandLocalCharge, double peakandLongDistanceCharge, double offPeakandLocalCharge, double offPeakandLongDistanceCharge)
        {
            PackageCode = packageCode;
            MonthlyRental = monthlyRental;
            BillingType = billingType;
            OffPeakStartHour = offPeakStartHour;
            OffPeakEndHour = offPeakEndHour;
            PeakStartHour = peakStartHour;
            PeakEndHour = peakEndHour;
            PeakandLocalCharge = peakandLocalCharge;
            PeakandLongDistanceCharge = peakandLongDistanceCharge;
            OffPeakandLocalCharge = offPeakandLocalCharge;
            OffPeakandLongDistanceCharge = offPeakandLongDistanceCharge;
        }
        public double MakeFirstMinuteFree(Package pkg,double noOfMins, double callCharge, double chargePerMin)
        {
            if(pkg.packageCode == "B" || pkg.packageCode == "C")
            {
                if (noOfMins > 1) { callCharge = callCharge - chargePerMin * (noOfMins - 1); }
                return callCharge;
            }
            else
            {
                return 0.00;
            }

        }
    }
}
