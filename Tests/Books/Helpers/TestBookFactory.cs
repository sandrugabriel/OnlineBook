using OnlineBookShop.Books.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Books.Helpers
{
    public class TestBookFactory
    {
        public static BookResponse CreateBook(int id)
        {
            return new BookResponse
            {
                Title = "test" + id,
                AvailableCopies = id * 10,
                Author = "Asdasd"
            };
        }

        public static List<BookResponse> CreateBooks(int count)
        {
            var books = new List<BookResponse>();

            for (int i = 0; i < count; i++)
            {
                books.Add(CreateBook(i));
            }

            return books;
        }
    }
}
