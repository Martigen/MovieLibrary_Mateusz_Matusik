using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories.CategoryRepositories;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieLibrary.Core.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly MovieLibraryContext _movieLibraryContext;

        public CategoryRepository(MovieLibraryContext movieLibraryContext)
        {
            _movieLibraryContext = movieLibraryContext;
        }
        public bool AddCategory(Category category)
        {
            bool categoryExists = _movieLibraryContext.Categories.Any(c => c.Name == category.Name);

            if (categoryExists)
            {
                return false;
            }
            else
            {
                _movieLibraryContext.Categories.Add(category);
                _movieLibraryContext.SaveChanges();
                return true;
            }
        }

        public bool DeleteCategory(int id)
        {
            Category category_to_delete = _movieLibraryContext.Categories.Find(id);
            if (category_to_delete == null)
            {
                return false;
            }
            else
            {
                _movieLibraryContext.Categories.Remove(category_to_delete);
                _movieLibraryContext.SaveChanges();
                return true;
            }
        }

        public IEnumerable<CategoryToDispaly> GetAllategories()
        {

            List<CategoryToDispaly> categories = new List<CategoryToDispaly>();
            foreach (Category item in _movieLibraryContext.Categories.ToList())
            {
                categories.Add(new CategoryToDispaly(item));
            }
            return categories;
        }

        public CategoryToDispaly GetCategoryById(int id)
        {
            Category category = _movieLibraryContext.Categories.Find(id);
            if (category != null)
            {
                return new CategoryToDispaly(category);
            }
            
            return null;
        }

     

        public bool UpdateCategory(Category category)
        {
            bool category_ok = _movieLibraryContext.Categories.Any(c => c.Id == category.Id);
            if (!category_ok)
            {
                return false;
            }
            else
            {
                _movieLibraryContext.Categories.Update(category);
                _movieLibraryContext.SaveChanges();
                return true;
            }
        }

        
    }
}
