using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<MovieToDisplay> FilterMovieByTitle(string title)
        {
            return _movieRepository.FilterByTitle(title);
        }


        [HttpPost("categories")]
        public List<MovieToDisplay> FilterMovieByTitle([FromQuery] int[] categoryIds)
        {

            return _movieRepository.FilterByCategories(new List<int>(categoryIds));
        }

        [HttpPost("min/{min}/max/{max}")]
        public List<MovieToDisplay> FilterMovieByTitle(decimal min,decimal max)
        {
            return _movieRepository.FilterByRating(min, max);
        }
    }
}
