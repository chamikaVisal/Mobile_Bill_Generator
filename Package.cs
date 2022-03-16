using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    internal class Package
    {
        public string PackageCode { get; set; }
        public double MonthlyRental { get; set; }
        public string BillingType { get; set; }
        public string CallType { get; set; }
        public int PeakHours { get; set; }
        public int OffPeakHours { get; set; }

        public Package(string packageCode, double monthlyRental, string billingType, string callType, int peakHours, int offPeakHours)
        {
            PackageCode = packageCode;
            MonthlyRental = monthlyRental;
            BillingType = billingType;
            CallType = callType;
            PeakHours = peakHours;
            OffPeakHours = offPeakHours;
        }
        public Package()
        {

        }
    }
}
