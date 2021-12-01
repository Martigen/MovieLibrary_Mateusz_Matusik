using MovieLibrary.Api.Models;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieLibrary.Core.Repositories.CategoryRepositories
{
    public interface ICategoryRepository
    {

        PagedList<CategoryToDisplay> GetAllCategories(Paging paging);
        bool AddCategory(Category category);
        bool DeleteCategory(int id);
        bool UpdateCategory(Category category);
        CategoryToDisplay GetCategoryById(int id);
       
    }
}
