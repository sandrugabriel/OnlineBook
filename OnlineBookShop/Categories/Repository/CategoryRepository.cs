using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Books.Models;
using OnlineBookShop.Categories.Dto;
using OnlineBookShop.Categories.Models;
using OnlineBookShop.Data;
using OnlineCategoryShop.Categories.Repository.interfaces;

namespace OnlineCategoryShop.Categories.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        AppDbContext _context;
        IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> CreateCategory(string name)
        {

            Category category = new Category();
            category.Name = name;
            category.Books = new List<Book>();
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(category);

        }

        public async Task<CategoryResponse> DeleteCategory(int id)
        {
            var category = await _context.Categories.Include(s => s.Books).FirstOrDefaultAsync(s => s.Id == id);

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            List<Category> categorys = await _context.Categories.Include(s => s.Books).ToListAsync();

            return _mapper.Map<List<CategoryResponse>>(categorys);
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {

            var category = await _context.Categories.Include(s => s.Books).FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse> GetByNameAsync(string name)
        {
            var category = await _context.Categories.Include(s => s.Books).FirstOrDefaultAsync(s => s.Name == name);

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<Category> GetById(int id)
        {

            var category = await _context.Categories.Include(s => s.Books).FirstOrDefaultAsync(s => s.Id == id);

            return category;
        }

        public async Task<Category> GetByName(string name)
        {
            var category = await _context.Categories.Include(s => s.Books).FirstOrDefaultAsync(s => s.Name == name);

            return category;
        }


        public async Task<CategoryResponse> UpdateCategory(int id, string name)
        {
            var category = await _context.Categories.Include(s=>s.Books).FirstOrDefaultAsync(s => s.Id == id);

            category.Name = name ?? category.Name;
          

            _context.Categories.Update(category);

            await _context.SaveChangesAsync();


            return _mapper.Map<CategoryResponse>(category);
        }

    }
}
