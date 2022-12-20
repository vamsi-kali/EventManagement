using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.DAL;
using Microsoft.Data;
namespace EventManagement.Helpers
{
    public class CustomerHelper
    {
        EventManagementContext db;
        public CustomerHelper()
        {
            this.db = new EventManagementContext();
        }
        public long InsertCustomer(Customer customer)
        {
            try
            {
                Customer cus = customer;
                var res = db.Customers.Add(cus);
                if (db.SaveChanges() <= 0)
                    throw new Exception("Unable to insert customer");
                return cus.CustomerId;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long UpdateCustomer(Customer customer)
        {
            try
            {
                db.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                if (db.SaveChanges() <= 0)
                    throw new Exception("Unable to update customer");
                return customer.CustomerId;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCustomer(Customer customer)
        {
            try
            {
                var existing_customer = db.Customers.Remove(customer);
                if (db.SaveChanges() > 0)
                    return true;
                throw new Exception("Unable to delete customer");
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Customer> SearchCustomer(Customer customer) => db.Customers.Where(x => x.Email == customer.Email || (x.FirstName == customer.FirstName && x.LastName == customer.LastName)).ToList();
    }
}
