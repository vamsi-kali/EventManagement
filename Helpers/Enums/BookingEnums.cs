using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Helpers.Enums
{
    public class BookingEnums
    {
        public enum BookingStatus
        {
            Initialize = 1,
            Pending = 2,
            Approved = 3,
            Rejected = 4,
            Cancelled = 5
        }
    }
}
