using MovieLibrary.Api.Models;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieLibrary.Core.Repositories
{
    public interface IMovieRepository
    {
        PagedList<MovieToDisplay> GetAllMovies(Paging paging);
        bool AddMovie(Movie movie);
        bool DeleteMovie(int id);
        bool UpdateMovie(Movie movie);
        MovieToDisplay GetMovieById(int id);
        PagedList<MovieToDisplay> FilterByTitle(Paging paging, string title);
        PagedList<MovieToDisplay> FilterByCategories(Paging paging, List<int> categoryIds);
        PagedList<MovieToDisplay> FilterByRating(Paging paging, decimal min, decimal max);

       


    }
}
