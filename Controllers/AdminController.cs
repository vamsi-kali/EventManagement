using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.DAL;
using EventManagement.Helpers;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        AdminHelper ah;

        public AdminController()
        {
            this.ah = new AdminHelper();
        }

        [HttpGet]
        [Route("Fetchqueue")]
        public List<Booking> FetchQueue()
        {
            return ah.FetchBookingQueue();
        }

        [HttpPost]
        [Route("decision/{adminid}/{approval}")]
        public bool EventDecision(Booking booking, [FromQuery] long adminid, [FromQuery] bool approval )
        {
            booking.Approvedby = adminid.ToString();
            return ah.EventDecision(booking, approval);
        }

        [HttpPost]
        [Route("insertvendor")]
        public long InsertVendor(Vendor vendor)
        {
            return ah.InsertVendor(vendor);
        }

        [HttpPut]
        [Route("updatevendor")]
        public bool UpdateVendor(Vendor vendor)
        {
            return ah.UpdateVendor(vendor);
        }

        [HttpDelete]
        [Route("DeleteVendor/{vendorid}")]

        public bool DeleteVendor(long vendorid)
        {
            return ah.DeleteVendor(vendorid);
        }

    }
}
