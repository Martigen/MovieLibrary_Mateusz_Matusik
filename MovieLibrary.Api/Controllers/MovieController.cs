using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Core.Repositories.CategoryRepositories;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MovieLibrary.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]Management")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;


        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;

        }


        [HttpGet]
        public IEnumerable<MovieToDisplay> GetMovies([FromQuery] Paging paging)
        {

            var movies = _movieRepository.GetAllMovies(paging);

            var metadata = new
            {
                movies.TotalCount,
                movies.PageSize,
                movies.CurrentPage,
                movies.TotalPages,
                movies.HasNext,
                movies.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            return movies;

        }

        [HttpGet("{id}")]
        public MovieToDisplay GetMovie(int id)
        {
            return _movieRepository.GetMovieById(id);
        }

        [HttpPost]
        public bool PostMovie(Movie movie)
        {
            return _movieRepository.AddMovie(movie);
        }

        [HttpDelete("{id}")]
        public bool DeleteMovie(int id)
        {
            return _movieRepository.DeleteMovie(id);
        }

        [HttpPut("{id}")]
        public bool PutMovie(int id, Movie movie)
        {
            return _movieRepository.UpdateMovie(movie);
        }



    }
}
