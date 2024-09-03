using CRM.CORE;
using System.Collections.Generic;
using System.Linq;

using CRM.CORE.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.CORE.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = _customerRepository.GetCustomers();
            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
            _customerRepository.Save();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _customerRepository.UpdateCustomer(customer);

            try
            {
                _customerRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_customerRepository.CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.DeleteCustomer(customer);
            _customerRepository.Save();

            return Ok(customer);
        }
    }
}
