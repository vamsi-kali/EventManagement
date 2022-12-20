using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagement.DAL
{
    public partial class BookingAdmin
    {
        public long AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
