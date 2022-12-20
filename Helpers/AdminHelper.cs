using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.DAL;
using Microsoft.Data;
using EventManagement.Helpers.Enums;
using EventManagement.Helpers;

namespace EventManagement.Helpers
{
    public class AdminHelper
    {
        EventManagementContext db;
        public AdminHelper()
        {
            this.db = new EventManagementContext();
        }

        public List<Booking> FetchBookingQueue()
        {
            try
            {
               return db.Bookings.Where(x => x.BookingStatusId == (int)BookingEnums.BookingStatus.Pending).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool EventDecision(Booking booking, bool approval)
        {
            try
            {
                var booking_details = db.Bookings.Where(x => x.BookingId == booking.BookingId).FirstOrDefault();
                if (approval)
                {
                    booking_details.Approvedby = booking.Approvedby;
                    booking_details.BookingStatusId = (int)BookingEnums.BookingStatus.Approved;
                    booking_details.ModifiedDate = DateTime.Now;
                }

                else
                {
                    booking_details.Approvedby = string.Empty;
                    booking_details.BookingStatusId = (int)BookingEnums.BookingStatus.Rejected;
                    booking_details.ModifiedDate = DateTime.Now;
                }
                return new BookingHelper().UpdateBookingDetails(booking_details);

            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public long InsertVendor(Vendor vendor)
        {
            try
            {
                Vendor ven = vendor;
                db.Vendors.Add(ven);
                if (db.SaveChanges() > 0)
                    return ven.VendorId;
                throw new Exception("Unable to insert Vendor details");
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateVendor(Vendor vendor)
        {
            try
            {
                db.Entry(vendor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                if (db.SaveChanges() > 0)
                    return true;
                throw new Exception("Unable to update Vendor details");
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteVendor(long vendorid)
        {
            try
            {
                var vendor = db.Vendors.Where(x => x.VendorId == vendorid).FirstOrDefault();
                if (vendor != null)
                    db.Vendors.Remove(vendor);
                else
                    throw new Exception("Vendor details not found or might be deleted");

                return db.SaveChanges() > 0 ? true : false;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
