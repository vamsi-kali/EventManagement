using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.DAL;
using EventManagement.Helpers;
using System.Text.RegularExpressions;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        BookingHelper bh;
        public BookingController()
        {
            this.bh = new BookingHelper();
        }
        [HttpGet]
        [Route("Getallbookings/{customerid}")]
        public List<Booking> FetchAllbookings(long customerid)
        {
           return bh.GetAllBookings(customerid);
        }

        [HttpGet]
        [Route("Getbookingsforthedate")]
        public List<Booking> Fetchbookings(Object date)
        {
            var d = date.ToString().Split(':')[1];
            Match match = Regex.Match(d, @"\d{4}\-\d{2}\-\d{2}");
            string regex_date = match.Value;
            DateTime dateTime = default;
            if (!string.IsNullOrEmpty(regex_date))
                dateTime = Convert.ToDateTime(regex_date);

            return bh.GetAllBookingsfordate(dateTime);

        }

        [HttpPost]
        [Route("NewBooking")]
        public long CreateBooking(Booking booking)
        {
            return bh.InsertBooking(booking);
        }

        [HttpPut]
        [Route("cancelbooking")]
        public bool CancelBooking(Booking booking)
        {
            return bh.CancelBooking(booking);
        }

        [HttpPost]
        [Route("Makepayment/{bookingid}/{paymenttype}")]
        public bool Makepayment(long bookingid, int paymenttype)
        {
            return bh.MakePayment(bookingid, paymenttype);
        }


    }
}
