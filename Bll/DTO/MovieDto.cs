using System;
using System.Collections.Generic;
using System.Text;

namespace Bll.DTO
{
    public enum MovieSearchField
    {
        Name,
        Genre,
        Director,
        Year,
        Country,
        Keyword
    }

    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string PosterUrl { get; set; }
        public string MovieUrl { get; set; }
        public string Keyword { get; set; }
        public int ReleaseYear { get; set; }
        public string Country { get; set; }
    }
}
