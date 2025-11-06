using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppuppgift_booking.lokaler
{
    public class Booking
    {
        private static int IncrementalID;

        public Booking(string _customerName, DateTime start, DateTime end)
        {
            ID = IncrementalID++;
            CustomerName = _customerName;
            StartTime = start;
            EndTime = end;
        }

        public readonly int ID;
        public string CustomerName { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public string GetBookingData()
        {
            return $"[{ID}]: \"{CustomerName}\" {StartTime} to {EndTime}";
        }
    }
}
