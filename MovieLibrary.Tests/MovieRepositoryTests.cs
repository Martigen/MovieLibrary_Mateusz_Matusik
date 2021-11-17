using Microsoft.EntityFrameworkCore;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace MovieLibrary.Tests
{
    public class MovieRepositoryTests
    {

      
        private MovieLibraryContext _movieLibraryContext;


        public MovieRepositoryTests()
        {
          
            _movieLibraryContext = new MovieLibraryContext();
        }

        [Fact]
        public void GetAllMovies_Test()
        {
            // Arrange
            var movieRepository = new MovieRepository(_movieLibraryContext);
            Paging paging = new Paging();
            // Act
            var result = movieRepository.GetAllMovies(paging);


            // Verify
            Assert.NotNull(result);
            Assert.True(result.GetType() == typeof(List<MovieToDisplay>));
            Assert.True(result.Count() >= 1);
            foreach (var movie in result)
            {
                Assert.True(_movieLibraryContext.Movies.Any(m => m.Title == movie.Title && m.Year == movie.Year));
            }


        }

        [Fact]
        public void GetMovieByID_Test()
        {
            // Arrange
            var movieRepository = new MovieRepository(_movieLibraryContext);

            // Act
            var result = movieRepository.GetMovieById(1);


            // Verify
            Assert.NotNull(result);
            Assert.True(result.GetType() == typeof(MovieToDisplay));
            Assert.True(_movieLibraryContext.Movies.Any(m => m.Title == result.Title && m.Year == result.Year && m.Id == result.Id));
            
        }

        [Fact]
        public void AddMovie_Test()
        {
            // Arrange
            var movieRepository = new MovieRepository(_movieLibraryContext);
            var movie = new Movie()
            {
                Title = "movie",
                Description = "description",
                Year = 2000,
                ImdbRating = 1,
                MovieCategories = new List<MovieCategory>()
            };
            // Act

            var result = movieRepository.AddMovie(movie);
            var result2 = movieRepository.AddMovie(movie);

            // Verify

            Assert.True(result);
            Assert.False(result2);
            Assert.True(_movieLibraryContext.Movies.Any(m => m.Title == movie.Title && m.Year == movie.Year));

        }

        [Fact]

        public void DeleteMovie_Test()
        {
            // Arrange
            var movieRepository = new MovieRepository(_movieLibraryContext);
            int id = _movieLibraryContext.Movies.Select(m => m.Id).Max();

            // Act
            
            var result = movieRepository.DeleteMovie(id);
            var result2 = movieRepository.DeleteMovie(id);

            // Verify

            Assert.True(result);
            Assert.False(result2);
            Assert.False(_movieLibraryContext.Movies.Any(m => m.Id == id));

        }

        [Fact]
        public void UpdateMovie_Test()
        {
            // Arrange
            var movieRepository = new MovieRepository(_movieLibraryContext);
            var movie = _movieLibraryContext.Movies.Find(3);
            movie.Title = "t1";
            // Act

            var result = movieRepository.UpdateMovie(movie);

            // Verify

            Assert.True(result);
            Assert.True(_movieLibraryContext.Movies.Any(m => m.Title == movie.Title && m.Year == movie.Year && m.Id == movie.Id));

        }


        [Fact]
        public void FilterByTitle_Test()
        {
            // Arrange
            var movieRepository = new MovieRepository(_movieLibraryContext);
            Paging paging = new Paging();
            string title = "harry";
            // Act

            var result = movieRepository.FilterByTitle(paging,title);

            // Verify

            Assert.True(result.GetType() == typeof(List<MovieToDisplay>));
            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
            foreach (var movie in result)
            {
                Assert.True(_movieLibraryContext.Movies.Any(m => m.Title == movie.Title && m.Year == movie.Year));
            }

        }

        [Fact]
        public void FilterByCategories_Test()
        {
            // Arrange
            var movieRepository = new MovieRepository(_movieLibraryContext);
            Paging paging = new Paging();
            List<int> categoriesId = new List<int>();
            categoriesId.Add(2);
            categoriesId.Add(6);
            // Act

            var result = movieRepository.FilterByCategories(paging, categoriesId);

            // Verify

            Assert.True(result.GetType() == typeof(List<MovieToDisplay>));
            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
            foreach (var movie in result)
            {
                Assert.True(_movieLibraryContext.Movies.Any(m => m.Title == movie.Title && m.Year == movie.Year));
            }

        }

        [Fact]
        public void FilterByRating_Test()
        {
            // Arrange
            var movieRepository = new MovieRepository(_movieLibraryContext);
            Paging paging = new Paging();
            decimal min = 8;
            decimal max = 9;
            // Act

            var result = movieRepository.FilterByRating(paging, min,max);

            // Verify

            Assert.True(result.GetType() == typeof(List<MovieToDisplay>));
            Assert.NotNull(result);
            Assert.True(result.Count() >= 1);
            foreach (var movie in result)
            {
                Assert.True(_movieLibraryContext.Movies.Any(m => m.Title == movie.Title && m.Year == movie.Year));
            }

        }

        
    }
}
