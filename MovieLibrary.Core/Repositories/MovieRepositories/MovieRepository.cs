using MovieLibrary.Api.Models;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieLibrary.Core.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly MovieLibraryContext _movieLibraryContext;

        public MovieRepository(MovieLibraryContext movieLibraryContext)
        {
            _movieLibraryContext = movieLibraryContext;
        }


        public IEnumerable<MovieToDisplay> GetAllMovies()
        {

            List<MovieToDisplay> movieList = new List<MovieToDisplay>();
            foreach (Movie item in _movieLibraryContext.Movies.ToList())
            {

                List<Category> categories = this.GetMovieCategories(item).ToList();
                movieList.Add(new MovieToDisplay(item, categories));
            }
            return movieList.OrderByDescending(m => m.ImdbRating);

          
        }
        public MovieToDisplay GetMovieById(int id)
        {
            Movie movie = _movieLibraryContext.Movies.Find(id);
            if(movie != null)
            {
                return new MovieToDisplay(movie, this.GetMovieCategories(movie).ToList());
            }
            return null;
        }

        public bool AddMovie(Movie movie)
        {
            bool movieExists = _movieLibraryContext.Movies.Any(m => m.Title == movie.Title && m.Year == movie.Year);

            if (movieExists)
            {
                return false;
            }
            else
            {
                _movieLibraryContext.Movies.Add(movie);
                _movieLibraryContext.SaveChanges();
                return true;
            }
        }

        public bool DeleteMovie(int id)
        {
            Movie movie_to_delete = _movieLibraryContext.Movies.Find(id);
            if (movie_to_delete == null)
            {
                return false;
            }
            else
            {
                _movieLibraryContext.Movies.Remove(movie_to_delete);
                _movieLibraryContext.SaveChanges();
                return true;
            }
        }

        public bool UpdateMovie(Movie movie)
        {
            bool movie_ok = _movieLibraryContext.Movies.Any(m => m.Id == movie.Id);
            if (!movie_ok)
            {
                return false;
            }
            else
            {
                _movieLibraryContext.Movies.Update(movie);
                _movieLibraryContext.SaveChanges();
                return true;
            }
        }



        public IEnumerable<Category> GetMovieCategories(Movie movie)
        {
            return _movieLibraryContext.MovieCategories.Where(mc => mc.Movie == movie).Select(c => c.Category).ToList();
        }

        public List<MovieToDisplay> FilterByTitle(string title)
        {
            if (title == null)
                return new List<MovieToDisplay>();
            return this.GetAllMovies().Where(m => m.Title.ToLower().Contains(title.ToLower())).OrderByDescending(m => m.ImdbRating).ToList();
        }
        public List<MovieToDisplay> FilterByCategories(List<int> categoryIds)
        {
            
            return this.GetAllMovies().Where(m => m.Categories.Select(c => c.Id).ToList().Intersect(categoryIds).Count() == categoryIds.Count).OrderByDescending(m => m.ImdbRating).ToList();
        }

        public List<MovieToDisplay> FilterByRating(decimal min, decimal max)
        {

            return this.GetAllMovies().Where(m => min <= m.ImdbRating && m.ImdbRating <= max).OrderByDescending(m => m.ImdbRating).ToList();
        }
    }
}
