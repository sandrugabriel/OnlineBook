using AutoMapper;
using FluentMigrator.Builders.Create.Column;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Models;
using OnlineBookShop.Categories.Dto;
using OnlineBookShop.Categories.Models;
using OnlineBookShop.Customers.Dto;
using OnlineBookShop.Customers.Models;
using OnlineBookShop.Loans.Dto;
using OnlineBookShop.Loans.Models;

namespace OnlineBookShop.MappingProfiles
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateBookRequest, Book>();
            CreateMap<Book, BookResponse>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
            CreateMap<Loan, LoanResponse>();

        }
    }
}
