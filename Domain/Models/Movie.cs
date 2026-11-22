using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Domain.Models
{
    public class Movie
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
        public string Country { get; set; }

        [ValidateNever]
        public string UserId { get; set; }

        [ValidateNever]
        public User User { get; set; }

    }
}
