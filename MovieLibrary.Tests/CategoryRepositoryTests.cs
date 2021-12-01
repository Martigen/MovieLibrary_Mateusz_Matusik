using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace MovieLibrary.Tests
{


    // Before running tests copy "MovieLibrary.db" from "\src\MovieLibrary.Api" to "\src\MovieLibrary.Tests\bin\Debug\netcoreapp3.1"



    public class CategoryRepositoryTests
    {
        private MovieLibraryContext _movieLibraryContext;
        public CategoryRepositoryTests()
        {

            _movieLibraryContext = new MovieLibraryContext();
        }

        [Fact]
        public void GetAllCategories_Test()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_movieLibraryContext);
            Paging paging = new Paging();
            // Act
            var result = categoryRepository.GetAllCategories(paging);


            // Verify
            Assert.NotNull(result);
            Assert.True(result.GetType() == typeof(List<CategoryToDisplay>));
            Assert.True(result.Count() >= 1);
            foreach (var category in result)
            {
                Assert.True(_movieLibraryContext.Categories.Any(c => c.Name == category.Name && c.Id == category.Id));
            }


        }

        [Fact]
        public void GetCategoryByID_Test()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_movieLibraryContext);

            // Act
            var result = categoryRepository.GetCategoryById(1);


            // Verify
            Assert.NotNull(result);
            Assert.True(result.GetType() == typeof(CategoryToDisplay));
            Assert.True(_movieLibraryContext.Categories.Any(c => c.Name == result.Name && c.Id == result.Id));

        }

        [Fact]
        public void AddCategory_Test()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_movieLibraryContext);
            var category = new Category()
            {
                Name = "category",
                MovieCategories = new List<MovieCategory>()
            };
            // Act

            var result = categoryRepository.AddCategory(category);
            var result2 = categoryRepository.AddCategory(category);

            // Verify

            Assert.True(result);
            Assert.False(result2);
            Assert.True(_movieLibraryContext.Categories.Any(c => c.Name == category.Name));

        }

        [Fact]

        public void DeleteCategory_Test()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_movieLibraryContext);
            int id = _movieLibraryContext.Categories.Select(c => c.Id).Max();

            // Act

            var result = categoryRepository.DeleteCategory(id);
            var result2 = categoryRepository.DeleteCategory(id);

            // Verify

            Assert.True(result);
            Assert.False(result2);
            Assert.False(_movieLibraryContext.Categories.Any(c => c.Id == id));

        }

        [Fact]
        public void UpdateCategory_Test()
        {
            // Arrange
            var categoryRepository = new CategoryRepository(_movieLibraryContext);
            var category = _movieLibraryContext.Categories.Find(1);
            category.Name = "t1";
            // Act

            var result = categoryRepository.UpdateCategory(category);

            // Verify

            Assert.True(result);
            Assert.True(_movieLibraryContext.Categories.Any(c => c.Name == category.Name));

        }
    }
}
