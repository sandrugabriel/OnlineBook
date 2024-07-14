using Moq;
using OnlineBookShop.Customers.Dto;
using OnlineBookShop.Customers.Repository.interfaces;
using OnlineBookShop.Customers.Service;
using OnlineBookShop.Customers.Service.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Customers.Helpers;

namespace Tests.Customers.UnitTests
{
    public class TestCustomerQueryService
    {

        private readonly Mock<ICustomerRepository> _mock;
        private readonly ICustomerQueryService _queryServiceCustomer;

        public TestCustomerQueryService()
        {
            _mock = new Mock<ICustomerRepository>();
            _queryServiceCustomer = new CustomerQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAllCustomer_ReturnCustomer()
        {
            var customers = TestCustomerFactory.CreateCustomers(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            var result = await _queryServiceCustomer.GetAllAsync();

            Assert.Equal(5,result.Count);

        }

        [Fact]
        public async Task GetByIdCustomer_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((CustomerResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryServiceCustomer.GetByIdAsync(1));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByIdCustomer_ReturnCustomer()
        {
            var customer = TestCustomerFactory.CreateCustomer(1);
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            var result = await _queryServiceCustomer.GetByIdAsync(1);

            Assert.Equal("test1",result.Name);

        }


        [Fact]
        public async Task GetByNameCustomer_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync((CustomerResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryServiceCustomer.GetByNameAsync("test"));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByNameCustomer_ReturnCustomer()
        {
            var customer = TestCustomerFactory.CreateCustomer(1);
            _mock.Setup(repo => repo.GetByNameAsync("test1")).ReturnsAsync(customer);

            var result = await _queryServiceCustomer.GetByNameAsync("test1");

            Assert.Equal("test1", result.Name);

        }
    }
}
