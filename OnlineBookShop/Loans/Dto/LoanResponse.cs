using OnlineBookShop.Books.Models;
using OnlineBookShop.Customers.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineBookShop.Loans.Dto
{
    public class LoanResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int BookId { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
