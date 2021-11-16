using MovieLibrary.Api.Models;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieLibrary.Core.Repositories.CategoryRepositories
{
    public interface ICategoryRepository
    {

        IEnumerable<CategoryToDispaly> GetAllategories();
        bool AddCategory(Category category);
        bool DeleteCategory(int id);
        bool UpdateCategory(Category category);
        CategoryToDispaly GetCategoryById(int id);
       
    }
}
