using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieLibrary.Data.Entities
{
    public class Category
    {
        public Category()
        {
            this.MovieCategories = new List<MovieCategory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<MovieCategory> MovieCategories { get; set; }
    }
}
