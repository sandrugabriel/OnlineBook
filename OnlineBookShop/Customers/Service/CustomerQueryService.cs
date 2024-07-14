using OnlineBookShop.Customers.Dto;
using OnlineBookShop.Customers.Repository.interfaces;
using OnlineBookShop.Customers.Service.interfaces;
using OnlineBookShop.Loans.Service.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;

namespace OnlineBookShop.Customers.Service
{
    public class CustomerQueryService : ICustomerQueryService
    {

        ICustomerRepository _repo;

        public CustomerQueryService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            if (customers.Count == 0) return new List<CustomerResponse>();

            return customers;
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _repo.GetByNameAsync(name);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }
    }
}
