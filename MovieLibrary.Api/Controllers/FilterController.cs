using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Models;
using MovieLibrary.Core.Repositories;
using MovieLibrary.Data.Models;
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
        public List<MovieToDisplay> FilterMovieByTitle([FromQuery] Paging paging, string title)
        {
            return _movieRepository.FilterByTitle(paging, title);
        }


        [HttpPost("categories")]
        public List<MovieToDisplay> FilterMovieByTitle([FromQuery] Paging paging, [FromQuery] int[] categoryIds)
        {

            return _movieRepository.FilterByCategories(paging, new List<int>(categoryIds));
        }

        [HttpPost("min/{min}/max/{max}")]
        public List<MovieToDisplay> FilterMovieByTitle([FromQuery] Paging paging, decimal min,decimal max)
        {
            return _movieRepository.FilterByRating(paging, min, max);
        }
    }
}
