using OnlineBookShop.Categories.Dto;
using OnlineBookShop.Categories.Models;

namespace OnlineCategoryShop.Categories.Repository.interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryResponse>> GetAllAsync();

        Task<CategoryResponse> GetByIdAsync(int id);

        Task<CategoryResponse> GetByNameAsync(string name);

        Task<Category> GetById(int id);

        Task<Category> GetByName(string name);

        Task<CategoryResponse> CreateCategory(string name);

        Task<CategoryResponse> UpdateCategory(int id, string name);

        Task<CategoryResponse> DeleteCategory(int id);

    }
}
