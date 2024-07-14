
using OnlineBookShop.Categories.Dto;
using OnlineBookShop.Categories.Service.interfaces;
using OnlineBookShop.System.Constants;
using OnlineCarWash.System.Exceptions;
using OnlineCategoryShop.Categories.Repository.interfaces;

namespace OnlineCategoryShop.Categories.Service
{
    public class CategoryCommandService : ICategoryCommandService
    {

        ICategoryRepository _repo;

        public CategoryCommandService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<CategoryResponse> CreateCategory(string name)
        {
            if (name.Length <= 2) throw new InvalidName(Constants.InvalidName);

            return await _repo.CreateCategory(name);
        }
        public async Task<CategoryResponse> UpdateCategory(int id, string name)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            if (name.Length <= 2) throw new InvalidName(Constants.InvalidName);


            category = await _repo.UpdateCategory(id, name);

            return category;
        }

        public async Task<CategoryResponse> DeleteCategory(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            await _repo.DeleteCategory(id);

            return category;
        }


    }
}
