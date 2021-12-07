using GroceryStoreAPI.Models.Customer;
using GroceryStoreAPI.SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Interfaces.BusinessLogic
{
    public interface ICustomerLogic
    {
        List<Customer> GetAllCustomers();
        RepositoryResponse<Customer> GetCustomerDetails(int customerId);
        RepositoryResponse<int> AddCustomer(Customer customer);
        RepositoryResponse<int> UpdateCustomer(Customer customer);
        RepositoryResponse<int> DeleteCustomer(int customerId);
        int GetNewCustomerId();
    }
}
