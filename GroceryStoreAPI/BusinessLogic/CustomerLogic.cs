using GroceryStoreAPI.Constants;
using GroceryStoreAPI.Interfaces.BusinessLogic;
using GroceryStoreAPI.Interfaces.Repositories;
using GroceryStoreAPI.Models.Customer;
using GroceryStoreAPI.SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.BusinessLogic
{
    public class CustomerLogic : ICustomerLogic
    {
        private ICustomerRepository _repo;

        public CustomerLogic(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public List<Customer> GetAllCustomers()
        {
            var result = _repo.GetAllCustomers();
            return result;
        }

        public RepositoryResponse<Customer> GetCustomerDetails(int customerId)
        {
            var response = new RepositoryResponse<Customer>();

            var allCustomers = GetAllCustomers();
            var customerDetails = allCustomers.FirstOrDefault(c => c.Id == customerId);

            if (customerDetails == null)
            {
                return new RepositoryResponse<Customer>()
                {
                    Data = null,
                    IsSuccess = false,
                    ErrorMessage = ErrorMessages.FailedToRetrieveCustomerData
                };
            }

            response.Data = customerDetails;
            return response;
        }

        public RepositoryResponse<int> AddCustomer(Customer newCustomer)
        {
            var response = new RepositoryResponse<int>();          
            newCustomer.Id = GetNewCustomerId();
            _repo.AddCustomer(newCustomer);

            response.Data = newCustomer.Id;
            return response;
        }

        public RepositoryResponse<int> UpdateCustomer(Customer customer)
        {
            var response = new RepositoryResponse<int>();
            var customerToUpdate = _repo.GetCustomerDetails(customer.Id);

            if (customerToUpdate == null)
            {
                return new RepositoryResponse<int>()
                {
                    Data = -1,
                    ErrorMessage = ErrorMessages.FailedToRetrieveCustomerData,
                    IsSuccess = false
                };
            }

            _repo.UpdateCustomer(customer);
            response.Data = customer.Id;
            return response;
        }

        public RepositoryResponse<int> DeleteCustomer(int customerId)
        {
            var response = new RepositoryResponse<int>();
            var customerToDelete = _repo.GetCustomerDetails(customerId);

            if (customerToDelete == null)
            {
                return new RepositoryResponse<int>()
                {
                    Data = -1,
                    ErrorMessage = ErrorMessages.FailedToRetrieveCustomerData,
                    IsSuccess = false
                };
            }

            _repo.DeleteCustomer(customerId);
            response.Data = customerId;
            return response;
        }

        public int GetNewCustomerId()
        {
            var allCustomers = GetAllCustomers();
            long customerCount = _repo.GetCustomerCount();

            if (customerCount == 0)
            {
                return 1;
            }

            if (customerCount + 1 > int.MaxValue)
            {
                throw new OverflowException("No more space in DB");
            }

            for (int i = 1; i < customerCount + 1; i++)
            {
                if (i != allCustomers[i - 1].Id)
                {
                    return i;
                }
            }

            return (int)(customerCount + 1);
        }
    }
}
