using OnlineBookShop.Books.Models;
using OnlineBookShop.Customers.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineBookShop.Loans.Models
{
    public class Loan
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [ForeignKey("BookId")]
        public int BookId { get; set; }

        [JsonIgnore]
        public virtual Book Book { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

    }
}
