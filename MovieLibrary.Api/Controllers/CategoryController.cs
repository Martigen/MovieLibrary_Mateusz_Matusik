using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Core.Repositories.CategoryRepositories;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]Management")]
    public class CategoryController : Controller
    {


        private readonly ICategoryRepository _categoryRepository;

        public CategoryController( ICategoryRepository categoryRepository)
        {
         
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public IEnumerable<CategoryToDispaly> GetCategories()
        {

            return _categoryRepository.GetAllategories();


        }

        [HttpGet("{id}")]
        public CategoryToDispaly GetCategory(int id)
        {
            return _categoryRepository.GetCategoryById(id);
        }

        [HttpPost]
        public bool PostCategory(Category category)
        {
            return _categoryRepository.AddCategory(category);
        }

        [HttpDelete("{id}")]
        public bool DeleteCategory(int id)
        {
            return _categoryRepository.DeleteCategory(id);
        }

        [HttpPut("{id}")]
        public bool PutCategory(int id, Category category)
        {
            return _categoryRepository.UpdateCategory(category);
        }
    }
}

