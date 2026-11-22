using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Queries
{
    public class MovieFilter
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public int? ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string? Keyword { get; set; }
        public string? Country { get; set; }
    }
}
