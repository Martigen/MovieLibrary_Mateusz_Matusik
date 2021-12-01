using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Core.Repositories.CategoryRepositories;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public IEnumerable<CategoryToDisplay> GetCategories([FromQuery] Paging paging)
        {


            var categories = _categoryRepository.GetAllCategories(paging);

            var metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasNext,
                categories.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            return categories;

            //return _categoryRepository.GetAllCategories(paging);


        }

        [HttpGet("{id}")]
        public CategoryToDisplay GetCategory(int id)
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

