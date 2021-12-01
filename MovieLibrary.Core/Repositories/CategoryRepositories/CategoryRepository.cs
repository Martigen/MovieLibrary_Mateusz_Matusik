using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories.CategoryRepositories;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Models;
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

        public PagedList<CategoryToDisplay> GetAllCategories(Paging paging)
        {

            List<CategoryToDisplay> categories = new List<CategoryToDisplay>();
            foreach (Category item in _movieLibraryContext.Categories.ToList())
            {
                categories.Add(new CategoryToDisplay(item));
            }
            //var categoriesList = categories.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList();

            return PagedList<CategoryToDisplay>.ToPagedList(categories,
                paging.PageNumber,
                paging.PageSize);


        }

        public CategoryToDisplay GetCategoryById(int id)
        {
            Category category = _movieLibraryContext.Categories.Find(id);
            if (category != null)
            {
                return new CategoryToDisplay(category);
            }

            return null;
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
