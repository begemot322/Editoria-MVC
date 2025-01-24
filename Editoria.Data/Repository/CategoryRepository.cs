using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _db.Categories.ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public void UpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }
    }
}
