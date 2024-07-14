
using OnlineBookShop.Categories.Dto;
using OnlineBookShop.Categories.Service.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;
using OnlineCategoryShop.Categories.Repository.interfaces;

namespace OnlineCategoryShop.Categories.Service
{
    public class CategoryQueryService : ICategoryQueryService
    {
        ICategoryRepository _repo;

        public CategoryQueryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            var category = await _repo.GetAllAsync();
            if (category.Count == 0) return new List<CategoryResponse>();
            return category.ToList();
        }

        public async Task<CategoryResponse> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            return category;
        }

        public async Task<CategoryResponse> GetByNameAsync(string name)
        {
            var category = await _repo.GetByNameAsync(name);
            if (category == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            return category;
        }

    }
}
