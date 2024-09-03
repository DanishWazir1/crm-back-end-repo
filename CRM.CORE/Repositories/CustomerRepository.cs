using CRM.CORE.Entities;
using CRM.CORE.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CORE.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbContext _context;

        public CustomerRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Set<Customer>().ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Set<Customer>().Find(id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Set<Customer>().Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Set<Customer>().Remove(customer);
        }

        public bool CustomerExists(int id)
        {
            return _context.Set<Customer>().Any(c => c.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
