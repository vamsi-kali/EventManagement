using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagement.DAL
{
    public partial class Booking
    {
        public long BookingId { get; set; }
        public long CustomerId { get; set; }
        public string Vendors { get; set; }
        public int? BookingStatusId { get; set; }
        public int? BookingTypeId { get; set; }
        public string Approvedby { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
