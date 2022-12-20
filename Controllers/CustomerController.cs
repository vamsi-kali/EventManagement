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
    public class CustomerController : ControllerBase
    {
        CustomerHelper ch;
        public CustomerController()
        {
            this.ch = new CustomerHelper();
        }
        [HttpPost]
        [Route("SignUp")]
        public long CustomerSignUp(Customer customer)
        {
            return ch.InsertCustomer(customer);
        }

        [HttpPut]
        [Route("UpdateCustInfo")]
        public long UpdateCustomerDetails(Customer customer)
        {
            return ch.UpdateCustomer(customer);
        }

        [HttpDelete]
        [Route("DeleteCust")]
        public bool DeleteCustomer(Customer customer)
        {
            return ch.DeleteCustomer(customer);
        }

        [HttpGet]
        [Route("FetchCust")]
        public List<Customer> SearchCustomer(Customer customer)
        {
            return ch.SearchCustomer(customer);
        }
    }
}
