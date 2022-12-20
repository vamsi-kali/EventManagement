using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagement.DAL
{
    public partial class Customer
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string GovernmentNumber { get; set; }
        public long? Phone { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
