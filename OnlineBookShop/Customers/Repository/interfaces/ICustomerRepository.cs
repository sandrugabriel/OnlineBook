using OnlineBookShop.Books.Models;
using OnlineBookShop.Customers.Dto;
using OnlineBookShop.Customers.Models;
using OnlineBookShop.Loans.Models;

namespace OnlineBookShop.Customers.Repository.interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<Customer> GetById(int id);

        Task<CustomerResponse> GetByNameAsync(string name);

        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

        Task<CustomerResponse> AddLoan(int id, Book book);

        Task<CustomerResponse> DeleteLoan(int id,Loan loan);
    }
}
