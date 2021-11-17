using Microsoft.EntityFrameworkCore;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace MovieLibrary.Tests
{

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

            // Act
            var result = categoryRepository.GetAllCategories();


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

        /*
                 foreach (int _id in id)
                        movies.Add(new Movie
                        {
                            Id = _id,
                            Title = "movie" + _id,
                            Description = "description" + _id,
                            Year = 2000 + _id,
                            ImdbRating = 1 + _id,
                            MovieCategories = new List<MovieCategory>()
            });

                    categories = new List<Category>();

                    foreach (int _id in Enumerable.Range(1,6))
                        categories.Add(new Category
                        {
                            Name = "category" + _id,               
                            MovieCategories = new List<MovieCategory>()
        });


        movieCategories = new List<MovieCategory>();
        foreach (int _id in id)
            movieCategories.Add(new MovieCategory
            {
                MovieId = _id,
                CategoryId = _id,
                Movie = movies[_id - 1],
                Category = categories[_id - 1],
            });
        */
    }
}
