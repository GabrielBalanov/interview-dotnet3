using GroceryStoreAPI.Interfaces.Repositories;
using GroceryStoreAPI.Models.Customer;
using GroceryStoreAPI.SharedObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private const string filePath = "database.json";
        public List<Customer> GetAllCustomers()
        {
            var allCustomers = GetAllCustomersAsList();
            return allCustomers;          
        }

        public Customer GetCustomerDetails(int customerId)
        {
            var allCustomers = GetAllCustomers();
            var customerDetails = allCustomers.FirstOrDefault(c => c.Id == customerId);
            return customerDetails;
        }

        public void AddCustomer(Customer newCustomer)
        {
            var allCustomers = GetAllCustomers();
            allCustomers.Add(newCustomer);
            SaveCustomersAsJson(allCustomers);
        }

        public void UpdateCustomer(Customer customer)
        {
            var allCustomers = GetAllCustomersAsList();
            var customerToUpdate = allCustomers.First(c => c.Id == customer.Id);          
            customerToUpdate.Name = customer.Name;
            SaveCustomersAsJson(allCustomers);

        }

        public void DeleteCustomer(int customerId)
        {
            var allCustomers = GetAllCustomersAsList().Where(c => c.Id != customerId).ToList();
            SaveCustomersAsJson(allCustomers);
        }

        public int GetCustomerCount()
        {
            return GetAllCustomers().Count;
        }

        private List<Customer> GetAllCustomersAsList()
        {
            var json = File.ReadAllText(filePath);
            List<Customer> customers = new List<Customer>();
            var jObject = JObject.Parse(json);

            if (jObject != null)
            {
                JArray customersArrary = (JArray)jObject["customers"];
                if (customersArrary != null)
                {
                    foreach (var item in customersArrary)
                    {
                        Customer customer = new Customer()
                        {
                            Id = int.Parse(item["id"].ToString()),
                            Name = item["name"].ToString()
                        };
                        customers.Add(customer);
                    }
                }
            }

            return customers.OrderBy(c => c.Id).ToList();
        }

        private void SaveCustomersAsJson(List<Customer> customers)
        {
            var json = File.ReadAllText(filePath);
            var jObject = JObject.Parse(json);
            var customersArray = JArray.FromObject(customers);

            foreach (var item in customersArray.Children())
            {
                foreach (var property in item.Children<JProperty>().ToList())
                {
                    property.Replace(new JProperty(property.Name.ToLower(), property.Value));
                }
            }

            jObject["customers"] = customersArray;

            string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jObject,
                                   Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, newJsonResult);
        }
    }
}
