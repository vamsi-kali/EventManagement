using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagement.DAL
{
    public partial class Payment
    {
        public long PaymentId { get; set; }
        public long BookingId { get; set; }
        public int PaymentTypeId { get; set; }
        public long? Amount { get; set; }
        public long Discounts { get; set; }
        public bool? Paid { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? VendorId { get; set; }
    }
}
