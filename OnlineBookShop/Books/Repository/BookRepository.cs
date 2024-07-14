using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Books.Dto;
using OnlineBookShop.Books.Models;
using OnlineBookShop.Books.Repository.interfaces;
using OnlineBookShop.Categories.Models;
using OnlineBookShop.Data;

namespace OnlineBookShop.Books.Repository
{
    public class BookRepository : IBookRepository
    {

        AppDbContext _context;
        IMapper _mapper;

        public BookRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookResponse> CreateBook(CreateBookRequest createRequest, Category category)
        {

            var book = _mapper.Map<Book>(createRequest);
            book.Category = category;
            book.CategoryId = category.Id;

            _context.Books.Add(book);

            await _context.SaveChangesAsync();

            return _mapper.Map<BookResponse>(book);

        }

        public async Task<BookResponse> DeleteBook(int id)
        {
            var book = await _context.Books.Include(s => s.Loans).Include(s=>s.Category).FirstOrDefaultAsync(s => s.Id == id);

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return _mapper.Map<BookResponse>(book);
        }

        public async Task<List<BookResponse>> GetAllAsync()
        {
            List<Book> books = await _context.Books.Include(s => s.Loans).Include(s => s.Category).ToListAsync();

            return _mapper.Map<List<BookResponse>>(books);
        }

        public async Task<BookResponse> GetByIdAsync(int id)
        {

            var book = await _context.Books.Include(s => s.Loans).Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<BookResponse>(book);
        }

        public async Task<BookResponse> GetByTitleAsync(string name)
        {
            var book = await _context.Books.Include(s => s.Loans).Include(s => s.Category).FirstOrDefaultAsync(s => s.Title == name);

            return _mapper.Map<BookResponse>(book);
        }

        public async Task<Book> GetById(int id)
        {

            var book = await _context.Books.Include(s => s.Loans).Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);

            return book;
        }

        public async Task<Book> GetByTitle(string name)
        {
            var book = await _context.Books.Include(s => s.Loans).Include(s => s.Category).FirstOrDefaultAsync(s => s.Title == name);

            return book;
        }


        public async Task<BookResponse> UpdateBook(int id, UpdateBookRequest updateRequest, Category category)
        {
            var book = await _context.Books.Include(s => s.Loans).Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);

            book.Title = updateRequest.Title ?? book.Title;
            book.Author = updateRequest.Author ?? book.Author;

            if (updateRequest.AvailableCopies != null)
            {
                book.AvailableCopies = updateRequest.AvailableCopies ?? book.AvailableCopies;
            }

            if (updateRequest.PublicationYear != null)
            {
                book.PublicationYear = updateRequest.PublicationYear ?? book.PublicationYear;
            }

            if (category != null)
            {
                book.Category = category;
                book.CategoryId = category.Id;
            }
          

            _context.Books.Update(book);

            await _context.SaveChangesAsync();


            return _mapper.Map<BookResponse>(book);
        }


    }
}
