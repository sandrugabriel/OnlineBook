using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Books.Models;
using OnlineBookShop.Customers.Dto;
using OnlineBookShop.Customers.Models;
using OnlineBookShop.Customers.Repository.interfaces;
using OnlineBookShop.Data;
using OnlineBookShop.Loans.Dto;
using OnlineBookShop.Loans.Models;

namespace OnlineBookShop.Customers.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        AppDbContext _context;
        IMapper _mapper;

        public CustomerRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customers = await _context.Customers.Include(s => s.Loans).ToListAsync();
            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.Include(s => s.Loans).FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = await _context.Customers.Include(s => s.Loans).FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _context.Customers.Include(s => s.Loans).FirstOrDefaultAsync(c => c.Name.Equals(name));
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest)
        {

            var customer = _mapper.Map<Customer>(createRequest);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            CustomerResponse customerView = _mapper.Map<CustomerResponse>(customer);

            return customerView;
        }
        public async Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest)
        {
            var customer = await _context.Customers.Include(s => s.Loans).FirstOrDefaultAsync(s => s.Id == id);
            customer.PhoneNumber = updateRequest.PhoneNumber ?? customer.PhoneNumber;
            customer.Name = updateRequest.Name ?? customer.Name;
            customer.Email = updateRequest.Email ?? customer.Email;

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            CustomerResponse customerView = _mapper.Map<CustomerResponse>(customer);

            return customerView;
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.Include(s => s.Loans).FirstOrDefaultAsync(s => s.Id == id);

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> AddLoan(int id, Book book)
        {
            var customer = await _context.Customers.Include(s => s.Loans).FirstOrDefaultAsync(s => s.Id == id);

            var loan = new Loan();
            loan.BorrowDate = DateTime.Now;
            loan.ReturnDate = DateTime.Now.AddDays(7);
            loan.Customer = customer;
            loan.CustomerId = id;
            loan.Book = book;
            loan.BookId = book.Id;


            var loanResponse = _mapper.Map<LoanResponse>(loan);

            customer.Loans.Add(loan);

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            //customerResponse.Loans.Add(loanResponse);

            return customerResponse;
        }

        public async Task<CustomerResponse> DeleteLoan(int id, Loan loan)
        {
            var customer = await _context.Customers.Include(s => s.Loans).FirstOrDefaultAsync(s => s.Id == id);

            customer.Loans.Remove(loan);

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }



    }
}
