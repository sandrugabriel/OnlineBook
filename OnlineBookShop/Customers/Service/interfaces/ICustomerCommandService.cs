using OnlineBookShop.Books.Models;
using OnlineBookShop.Customers.Dto;

namespace OnlineBookShop.Customers.Service.interfaces
{
    public interface ICustomerCommandService
    {
        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

        Task<CustomerResponse> AddLoan(int id, string nameBook);

        Task<CustomerResponse> DeleteLoan(int id, int loanId);
    }
}
