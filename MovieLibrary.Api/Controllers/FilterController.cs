using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieLibrary.Api.Controllers
{
    [ApiController]
    [Route("v1/Movie/[controller]")]
    public class FilterController : Controller
    {


        private readonly IMovieRepository _movieRepository;

        public FilterController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;

        }

        [HttpPost("title/{title}")]
        public List<MovieToDisplay> FilterMovieByTitle([FromQuery] Paging paging, string title)
        {


            var movies = _movieRepository.FilterByTitle(paging, title);

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


        [HttpPost("categories")]
        public List<MovieToDisplay> FilterMovieByTitle([FromQuery] Paging paging, [FromQuery] int[] categoryIds)
        {
            var movies = _movieRepository.FilterByCategories(paging, new List<int>(categoryIds));

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

        [HttpPost("min/{min}/max/{max}")]
        public List<MovieToDisplay> FilterMovieByTitle([FromQuery] Paging paging, decimal min, decimal max)
        {
            var movies = _movieRepository.FilterByRating(paging, min, max);

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
    }
}
