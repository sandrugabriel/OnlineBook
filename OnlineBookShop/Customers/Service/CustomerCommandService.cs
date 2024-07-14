using OnlineBookShop.Books.Repository.interfaces;
using OnlineBookShop.Customers.Dto;
using OnlineBookShop.Customers.Repository.interfaces;
using OnlineBookShop.Customers.Service.interfaces;
using OnlineBookShop.System.Constants;
using OnlineBookShop.System.Exceptions;
using OnlineCarWash.System.Exceptions;

namespace OnlineBookShop.Customers.Service
{
    public class CustomerCommandService : ICustomerCommandService
    {

        ICustomerRepository _repo;
        IBookRepository _repobook;

        public CustomerCommandService(ICustomerRepository repo,IBookRepository book)
        {
            _repo = repo;
            _repobook = book;
        }

        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest)
        {
            if (createRequest.Name.Equals("") || createRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            var customer = await _repo.CreateCustomer(createRequest);

            return customer;
        }

        public async Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest)
        {

            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (updateRequest.Name.Equals("") || updateRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            customer = await _repo.UpdateCustomer(id, updateRequest);
            return customer;
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            await _repo.DeleteCustomer(id);

            return customer;
        }

        public async Task<CustomerResponse> AddLoan(int id, string nameBook)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            var book = await _repobook.GetByTitle(nameBook);
            if (book == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            if(customer.Loans.Count >= 6) throw new TooMuch(Constants.TooMuch);

            customer = await _repo.AddLoan(id, book);

            return customer;
        }

        public async Task<CustomerResponse> DeleteLoan(int id, int loanId)
        {
            var customer = await _repo.GetById(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            var loan = customer.Loans.FirstOrDefault(s => s.Id == loanId);

            if (loan == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            var custresponse = await _repo.GetByIdAsync(id);

            custresponse = await _repo.DeleteLoan(id, loan);

            return custresponse;
        }

    }
}
