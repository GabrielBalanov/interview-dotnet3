using GroceryStoreAPI.Interfaces.BusinessLogic;
using GroceryStoreAPI.Models.Customer;
using GroceryStoreAPI.SharedObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerLogic _customerLogic;
        public CustomerController(ICustomerLogic customerLogic)
        {
            _customerLogic = customerLogic;
        }

        [HttpGet]
        [Route("customers")]
        public ActionResult<List<Customer>> GetAllCustomers()
        {
            var allCustomers = new List<Customer>();

            try
            {
                allCustomers = _customerLogic.GetAllCustomers();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message?.ToString() ?? "Internal Error");
            }
            return allCustomers;
        }

        [HttpGet]
        [Route("customers/{customerId}")]
        public ActionResult<Customer> GetCustomerDetails(int customerId)
        {
            var response = new RepositoryResponse<Customer>();
            
            try
            {
                response = _customerLogic.GetCustomerDetails(customerId);

                if (!response.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message?.ToString() ?? "Internal Error");
            }

            return StatusCode(StatusCodes.Status200OK, response.Data);
        }

        [HttpPost]
        [Route("customers/{customerId}")]
        public IActionResult AddCustomer(Customer customer)
        {
            var response = new RepositoryResponse<int>();

            try
            {
                response = _customerLogic.AddCustomer(customer);

                if (!response.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message?.ToString() ?? "Internal Error");
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut]
        public ActionResult<RepositoryResponse<Customer>> UpdateCustomer(Customer customer)
        {
            var response = new RepositoryResponse<int>();

            try
            {
                response = _customerLogic.UpdateCustomer(customer); ;

                if (!response.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message?.ToString() ?? "Internal Error");
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        public ActionResult<RepositoryResponse<int>> DeleteCustomer(int customerId)
        {
            var response = new RepositoryResponse<int>();

            try
            {
                response = _customerLogic.DeleteCustomer(customerId);

                if (!response.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message?.ToString() ?? "Internal Error");
            }

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
