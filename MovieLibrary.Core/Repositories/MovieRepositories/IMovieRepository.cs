using MovieLibrary.Api.Models;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieLibrary.Core.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<MovieToDisplay> GetAllMovies();
        bool AddMovie(Movie movie);
        bool DeleteMovie(int id);
        bool UpdateMovie(Movie movie);
        List<MovieToDisplay> FilterByTitle(string title);
        List<MovieToDisplay> FilterByCategories(List<int> categoryIds);
        List<MovieToDisplay> FilterByRating(decimal min, decimal max);

        MovieToDisplay GetMovieById(int id);


    }
}
