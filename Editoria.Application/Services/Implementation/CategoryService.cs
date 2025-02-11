using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task CreateCategoryAsync(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);

            await _categoryRepository.AddAsync(category);
        }
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetAsync(a => a.CategoryId == categoryId);

                if (category != null)
                {
                    await _categoryRepository.DeleteAsync(category);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Category with ID {categoryId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByPriorityAsync(int minPriority, int maxPriority)
        {
            return await _categoryRepository.GetCategoriesByPriorityAsync(minPriority, maxPriority);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetAsync(u => u.CategoryId == id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);

            await _categoryRepository.UpdateAsync(category);
        }
    }
}
