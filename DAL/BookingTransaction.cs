using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagement.DAL
{
    public partial class BookingTransaction
    {
        public long TransactionId { get; set; }
        public long PaymentId { get; set; }
        public long? Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
