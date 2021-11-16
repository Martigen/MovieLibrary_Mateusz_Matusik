using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Api.Models
{
    public class MovieToDisplay
    {
        public MovieToDisplay()
        {
            this.Categories = new List<CategoryToDispaly>();
        }

        public MovieToDisplay(Movie movie, List<Category> categories)
        {
            this.Id = movie.Id;
            this.Title = movie.Title;
            this.Description = movie.Description;
            this.Year = movie.Year;
            this.ImdbRating = movie.ImdbRating;
            this.Categories = categories.Select(c => new CategoryToDispaly()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal ImdbRating { get; set; }

        public virtual ICollection<CategoryToDispaly> Categories { get; set; }
    }
}
