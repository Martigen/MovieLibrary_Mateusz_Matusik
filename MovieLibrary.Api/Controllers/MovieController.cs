using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Core.Repositories.CategoryRepositories;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IEnumerable<MovieToDisplay> GetMovies()
        {

            return _movieRepository.GetAllMovies();
               
           
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
