using OnlineBookShop.Customers.Dto;
using OnlineBookShop.Customers.Models;

namespace OnlineBookShop.Customers.Service.interfaces
{
    public interface ICustomerQueryService
    {
        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<CustomerResponse> GetByNameAsync(string name);

    }
}
