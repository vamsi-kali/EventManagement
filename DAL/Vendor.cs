using System;
using System.Collections.Generic;

#nullable disable

namespace EventManagement.DAL
{
    public partial class Vendor
    {
        public long VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Owner { get; set; }
        public string Department { get; set; }
        public long Mobile { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
