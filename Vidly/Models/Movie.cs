using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        [Required]
        public int NumberInStock { get; set; }
    }

    public class Genre
    {
        public byte Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}