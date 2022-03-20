using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    public class Package
    {

        public string PackageCode { get; set; }
        public double MonthlyRental { get; set; }
        public string BillingType { get; set; }
        public string CallType { get; set; }
        public int OffPeakStartHour { get; set; }
        public int OffPeakEndHour { get; set; }
        public int PeakStartHour { get; set; }
        public int PeakEndHour { get; set; }
        public double PeakandLocalCharge { get; set; }
        public double PeakandLongDistanceCharge { get; set; }
        public double OffPeakandLocalCharge { get; set; }
        public double OffPeakandLongDistanceCharge { get; set; }


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
    }
}
