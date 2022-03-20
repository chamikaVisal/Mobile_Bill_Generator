using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biller
{
    public class CDR
    {
        private int callerPhoneNumber;
        public int CallerPhoneNumber { get { return callerPhoneNumber; } set { callerPhoneNumber = value; } }
        private int calleePhoneNumber;
        public int CalleePhoneNumber { get { return calleePhoneNumber; } set { calleePhoneNumber = value; } }
        private DateTime callStartTime;
        public DateTime CallStartTime { get { return callStartTime; } set { callStartTime = value; } }
        private double callDuration;
        public double CallDuration { get { return callDuration; } set { callDuration = value; } }
        private double charge;
        public double Charge { get { return charge; } set { charge = value; } }

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
