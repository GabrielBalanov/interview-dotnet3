using GroceryStoreAPI.Models.Customer;
using GroceryStoreAPI.SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerDetails(int customerId);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        int GetCustomerCount();
    }
}
