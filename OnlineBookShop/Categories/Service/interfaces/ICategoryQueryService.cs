using OnlineBookShop.Categories.Dto;

namespace OnlineBookShop.Categories.Service.interfaces
{
    public interface ICategoryQueryService
    {
        Task<List<CategoryResponse>> GetAllAsync();

        Task<CategoryResponse> GetByIdAsync(int id);

        Task<CategoryResponse> GetByNameAsync(string name);
    }
}
