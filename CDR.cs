using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    internal class CDR
    {
        public int CallerPhoneNumber { get; set; }
        public int CalleePhoneNumber { get; set; }
        public DateTime CallStartTime { get; set; }
        public double CallDuration { get; set; }
        public double Charge { get; set; }

        public CDR(int callerPhoneNumber, int calleePhoneNumber, DateTime callStartTime, double callDuration)
        {
            CallerPhoneNumber = callerPhoneNumber;
            CalleePhoneNumber = calleePhoneNumber;
            CallStartTime = callStartTime;
            CallDuration = callDuration;
        }
        public CDR()
        {

        }

        public bool IsLocalCall(CDR cdr)
        {
            int callerExtenion = CallerPhoneNumber.ToString()[0] + CallerPhoneNumber.ToString()[1];
            int calleeExtenion = CalleePhoneNumber.ToString()[0] + CalleePhoneNumber.ToString()[1];

            if (callerExtenion == calleeExtenion)
            {
                return true;
            }
            return false; // long distance call
        }

        public bool IsPeakHour(CDR cdr)
        {
            string hour = cdr.CallStartTime.ToString("HH");
            int convertedhour = int.Parse(hour);

            if ((convertedhour > 8) && (convertedhour < 20))
            {
                return true;
            }
            return false;
        }

    }
}
