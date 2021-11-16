using MovieLibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Api.Models
{
    public class CategoryToDispaly
    {

        public CategoryToDispaly()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryToDispaly(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
        }

        public CategoryToDispaly(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
