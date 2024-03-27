using Microsoft.EntityFrameworkCore;
using Suptickit.Application;
using Suptickit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SuptickitContext _db;
        public CustomerRepository(SuptickitContext db)
        {
            _db = db;
        }
        public async Task<ServiceResponse<Customer>> AddAsync(Customer customer)
        {
            try
            {
                customer.DateCreated = DateTime.UtcNow;
                _db.Customers.Add(customer);
                await _db.SaveChangesAsync();
                return new ServiceResponse<Customer> { Data = customer, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Customer> { Success = false, Message = "Failed to create Customer, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<IEnumerable<Customer>>> GetAllAsync()
        {
            try
            {
                var customers = await _db.Customers.ToListAsync();
                return new ServiceResponse<IEnumerable<Customer>> { Data = customers, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<Customer>> { Success = true, Message = "Failed to retrieve Customers, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Customer>> GetByIdAsync(int id)
        {
            try
            {
                var customer = await _db.Customers.FindAsync(id);
                if (customer == null) return new ServiceResponse<Customer> { Success = false, Message = "No matching customer for id " + id };
                return new ServiceResponse<Customer> { Data = customer, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Customer> { Success = false, Message = "Failed to retrieve customer, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Customer>> RemoveAsync(int id)
        {
            try
            {
                var customer = await _db.Customers.FindAsync(id);
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync();
                return new ServiceResponse<Customer> { Data = customer, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Customer> { Success = false, Message = "Failed to retrieve customer, " + ex.Message };
            }
        }

        public async Task<ServiceResponse<Customer>> UpdateAsync(Customer customer)
        {
            try
            {
                var oldCustomer = await _db.Customers.FindAsync(customer.Id);
                oldCustomer.Firstname = customer.Firstname;
                oldCustomer.Adress = customer.Adress;
                oldCustomer.Lastname= customer.Lastname;
                oldCustomer.Email= customer.Email;
                oldCustomer.Phone= customer.Phone;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Customer> { Data = oldCustomer, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Customer> { Success = false, Message = "Failed to update customer, " + ex.Message };
            }
        }
    }
}
