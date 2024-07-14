using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OnlineBookShop.Categories.Models;
using OnlineBookShop.Categories.Dto;

namespace OnlineBookShop.Books.Dto
{
    public class BookResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public CategoryResponse Category { get; set; }

        public int PublicationYear { get; set; }

        public int AvailableCopies { get; set; }

    }
}
