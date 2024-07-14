using OnlineBookShop.Categories.Dto;

namespace OnlineBookShop.Categories.Service.interfaces
{
    public interface ICategoryCommandService
    {
        Task<CategoryResponse> CreateCategory(string name);

        Task<CategoryResponse> UpdateCategory(int id, string name);

        Task<CategoryResponse> DeleteCategory(int id);
    }
}
