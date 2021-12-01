using MovieLibrary.Api.Models;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Models;
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


        private IEnumerable<MovieToDisplay> GetAllWithoutPaging()
        {
            List<MovieToDisplay> movieList = new List<MovieToDisplay>();
            foreach (Movie item in _movieLibraryContext.Movies.ToList())
            {

                List<Category> categories = this.GetMovieCategories(item).ToList();
                movieList.Add(new MovieToDisplay(item, categories));
            }
            return movieList.OrderByDescending(m => m.ImdbRating).ToList();

        }

        public PagedList<MovieToDisplay> GetAllMovies(Paging paging)
        {
            return PagedList<MovieToDisplay>.ToPagedList(GetAllWithoutPaging(),
                 paging.PageNumber,
                 paging.PageSize);

            //return GetAllWithoutPaging().Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToPagedList();
        }



        public MovieToDisplay GetMovieById(int id)
        {
            Movie movie = _movieLibraryContext.Movies.Find(id);
            if (movie != null)
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

        public PagedList<MovieToDisplay> FilterByTitle(Paging paging, string title)
        {
            if (title == null)
                return new PagedList<MovieToDisplay>(null, 0, 0, 0);

            return PagedList<MovieToDisplay>.ToPagedList(GetAllWithoutPaging().Where(m => m.Title.ToLower().Contains(title.ToLower())).OrderByDescending(m => m.ImdbRating),
                paging.PageNumber,
                paging.PageSize);

        }
        public PagedList<MovieToDisplay> FilterByCategories(Paging paging, List<int> categoryIds)
        {

            return PagedList<MovieToDisplay>.ToPagedList(GetAllWithoutPaging().Where(m => m.Categories.Select(c => c.Id).ToList().Intersect(categoryIds).Count() == categoryIds.Count).OrderByDescending(m => m.ImdbRating),
            paging.PageNumber,
            paging.PageSize);
        }

        public PagedList<MovieToDisplay> FilterByRating(Paging paging, decimal min, decimal max)
        {

            return PagedList<MovieToDisplay>.ToPagedList(GetAllWithoutPaging().Where(m => min <= m.ImdbRating && m.ImdbRating <= max).OrderByDescending(m => m.ImdbRating),
            paging.PageNumber,
            paging.PageSize);
        }
    }
}
