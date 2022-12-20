using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.DAL;
using Microsoft.Data;
using EventManagement.Helpers.Enums;
using System.Transactions;

namespace EventManagement.Helpers
{
    public class BookingHelper
    {
        EventManagementContext db;
        public BookingHelper()
        {
            this.db = new EventManagementContext();
        }

        public List<Booking> GetAllBookings(long customerid) => db.Bookings.Where(x => x.CustomerId == customerid).OrderByDescending(x => x.BookingStatusId).ToList();


        public long InsertBooking(Booking book)
        {
            Booking booking = book;
            try
            {
                booking.BookingStatusId = (int)BookingEnums.BookingStatus.Pending;
                db.Bookings.Add(booking);
                if (db.SaveChanges() > 0)
                    return booking.BookingId;
                throw new Exception("Unable to process your booking");
            }

            catch (Exception ex) { throw ex; }
        }

        public List<Booking> GetAllBookingsfordate(DateTime dateTime) => db.Bookings.Where(x => x.BookingStatusId == (int)BookingEnums.BookingStatus.Approved && x.CreatedDate == dateTime).ToList();

        public bool CancelBooking(Booking booking)
        {
            try
            {
                var current_booking = db.Bookings.Where(x => x.BookingId == booking.BookingId).FirstOrDefault();
                current_booking.BookingStatusId = (int)BookingEnums.BookingStatus.Cancelled;

                var booking_payment = db.Payments.Where(x => x.BookingId == booking.BookingId).FirstOrDefault();
                if (booking_payment != null)
                {
                    booking_payment.Paid = null;
                    var booking_tran = db.BookingTransactions.Where(x => x.PaymentId == booking_payment.PaymentId).FirstOrDefault();
                    if (booking_tran != null)
                        booking_tran.Amount = -booking_tran.Amount;
                }

                if (db.SaveChanges() <= 0)
                    throw new Exception("Event cancellation failed");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MakePayment(long bookingid, int paymenttype)
        {
            using (TransactionScope sc = new TransactionScope())
            {
                try
                {
                    var booking_details = db.Payments.Where(x => x.BookingId == bookingid).FirstOrDefault();
                    if (booking_details == null)
                        throw new Exception("No event booking is found");
                    booking_details.PaymentTypeId = paymenttype;
                    booking_details.Paid = true;

                    this.InsertBookingTransaction(booking_details);
                    if (db.SaveChanges() > 0)
                    {
                        sc.Complete();
                        return true;
                    }
                    throw new Exception("Payment Failed...try again sometime later");
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public bool InsertBookingTransaction(Payment p)
        {
            using (TransactionScope sc = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    var final_amount = (p.Amount - (p.Amount * p.Discounts));
                    BookingTransaction bt = new BookingTransaction() { Amount = final_amount, CreatedDate = DateTime.Now, PaymentId = p.PaymentId };
                    db.BookingTransactions.Add(bt);
                    if (db.SaveChanges() > 0)
                    {
                        sc.Complete();
                        return true;
                    }
                    throw new Exception("Transaction is aborted.....");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool UpdateBookingDetails(Booking booking)
        {
            try
            {
                //var booking_details = db.Bookings.Where(x => x.BookingId == booking.BookingId).FirstOrDefault();
                //booking_details.Approvedby = booking.Approvedby;
                //booking_details.BookingStatusId = (int)BookingEnums.BookingStatus.Approved;
                //booking_details.ModifiedDate = DateTime.Now;

                db.Entry(booking).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                if (db.SaveChanges() > 0)
                    return true;
                throw new Exception("Booking details updation failed");
                
            }

            catch(Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
