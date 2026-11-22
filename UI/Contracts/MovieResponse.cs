namespace UI.Contracts
{
    public class MovieResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public int ReleaseYear { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string PosterUrl { get; set; }
    }
}