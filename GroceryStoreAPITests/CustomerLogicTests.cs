using GroceryStoreAPI.BusinessLogic;
using GroceryStoreAPI.Interfaces.Repositories;
using GroceryStoreAPI.Models.Customer;
using GroceryStoreAPI.SharedObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GroceryStoreAPI.Constants;

namespace GroceryStoreAPITests
{
    public class CustomerLogicTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepo;
        private readonly CustomerLogic _customerLogic;
        private readonly List<Customer> _initialTestData;
        private List<Customer> _data;

        public CustomerLogicTests()
        {
            _mockCustomerRepo = new Mock<ICustomerRepository>();
            _customerLogic = new CustomerLogic(_mockCustomerRepo.Object);
            _initialTestData = new List<Customer>()
            {
                new Customer { Id = 1, Name = "Steve" },
                new Customer { Id = 2, Name = "John" },
                new Customer { Id = 3, Name = "Mark" } ,
                new Customer { Id = 4, Name = "Matt" } ,
                new Customer { Id = 5, Name = "Sarah" }
            };
        }
        
        [Fact]
        public void GetCustomerDetailsForExistingCustomerSuccess()
        {
            // Arrange
            InitializeCustomers();
            _mockCustomerRepo.Setup(m => m.GetAllCustomers()).Returns(GetCustomers());

            // Act
            var response = _customerLogic.GetCustomerDetails(1);

            // Assert
            Xunit.Assert.NotNull(response.Data);
            Xunit.Assert.Equal(1, response.Data.Id);
            Xunit.Assert.Empty(response.ErrorMessage);
            Xunit.Assert.True(response.IsSuccess);
        }

        [Fact]
        public void GetCustomerDetailsReturnsNullDataAndErrorOnFailure()
        {
            // Arrange
            InitializeCustomers();
            _mockCustomerRepo.Setup(m => m.GetAllCustomers()).Returns(GetCustomers());

            // Act
            var response = _customerLogic.GetCustomerDetails(0);

            // Assert
            Xunit.Assert.Null(response.Data);
            Xunit.Assert.Equal(ErrorMessages.FailedToRetrieveCustomerData, response.ErrorMessage);
            Xunit.Assert.False(response.IsSuccess);
        }

        [Fact]
        public void GetNewCustomerIdReturnIdOneWhenNoCustomersExist()
        {
            // Arrange
            _mockCustomerRepo.Setup(m => m.GetAllCustomers()).Returns(new List<Customer>());
            
            // Act
            int id = _customerLogic.GetNewCustomerId();

            // Assert
            Xunit.Assert.Equal(1, id);
        }

        [Fact]
        public void GetNewCustomerIdReturnsLowestMissingPositiveIntergerAsId()
        {
            // Arrange
            InitializeCustomers();
            _mockCustomerRepo.Setup(m => m.GetAllCustomers()).Returns(GetCustomers().Where(c => c.Id != 3).ToList());

            // Act
            int id = _customerLogic.GetNewCustomerId();

            // Assert
            Xunit.Assert.Equal(3, id);
        }

        [Fact]
        public void GetNewCustomerIdReturnsIdThatEqualsTheSizeOfCustomersListPlusOneWhenAllOtherIdsAreConsecutive()
        {
            // Arrange
            InitializeCustomers();
            _mockCustomerRepo.Setup(m => m.GetAllCustomers()).Returns(GetCustomers());

            // Act
            int id = _customerLogic.GetNewCustomerId();

            // Assert
            Xunit.Assert.Equal(6, id);
        }

        [Fact]
        public void GetNewCustomerIdThrowsOverflowExceptionWhenIdTooLarge()
        {
            // Arrange
            _mockCustomerRepo.Setup(m => m.GetCustomerCount()).Returns(int.MaxValue);
            _mockCustomerRepo.Setup(m => m.GetAllCustomers()).Returns(GetCustomers());

            // Act
            Assert.Throws<OverflowException>(() => _customerLogic.AddCustomer(new Customer() { Id=0, Name = "test" }));
        }

        [Fact]
        public void UpdateCustomerReturnsNegativeIdAndErrorOnFailure()
        {
            // Arrange
            InitializeCustomers();
            _mockCustomerRepo.Setup(m => m.GetAllCustomers()).Returns(GetCustomers());

            // Act
            var response = _customerLogic.UpdateCustomer(new Customer() { Id=10, Name = "test"});

            // Assert
            Xunit.Assert.Equal(-1, response.Data);
            Xunit.Assert.Equal(ErrorMessages.FailedToRetrieveCustomerData, response.ErrorMessage);
            Xunit.Assert.False(response.IsSuccess);
        }

        [Fact]
        public void DeleteCustomerReturnsNegativeIdAndErrorOnFailure()
        {
            // Arrange
            InitializeCustomers();
            _mockCustomerRepo.Setup(m => m.GetAllCustomers()).Returns(GetCustomers());

            // Act
            var response = _customerLogic.DeleteCustomer(10);

            // Assert
            Xunit.Assert.Equal(-1, response.Data);
            Xunit.Assert.Equal(ErrorMessages.FailedToRetrieveCustomerData, response.ErrorMessage);
            Xunit.Assert.False(response.IsSuccess);
        }

        private void InitializeCustomers()
        {
            _data = new List<Customer>();

            foreach(var c in _initialTestData)
            {
                _data.Add(new Customer()
                {
                    Id = c.Id,
                    Name = c.Name
                });
            }
        }

        private List<Customer> GetCustomers()
        {
            return _data;
        }
    }
}
