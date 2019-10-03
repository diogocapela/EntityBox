using System.Collections.Generic;

namespace EntityBox.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public decimal Rating { get; set; }
        public string ImdbId { get; set; }
        public string YoutubeId { get; set; }
        public Country Country { get; set; }
        public Person Director { get; set; }
        public List<Person> Actors { get; set; }
    }
}