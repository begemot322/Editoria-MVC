using Editoria.Domain.Entities;

namespace Editoria.Application.Services.Services
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetCategoriesByPriorityAsync(int minPriority, int maxPriority);

    }
}