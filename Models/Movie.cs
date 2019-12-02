using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public String Name { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte StockNumber { get; set; }

    }
}