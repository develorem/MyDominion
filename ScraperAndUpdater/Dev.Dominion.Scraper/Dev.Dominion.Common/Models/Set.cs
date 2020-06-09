using System.Collections.Generic;

namespace Dev.Dominion.Scraper.Models
{
    public class Set
    {
        public Set()
        {
            Cards = new List<Card>();
        }
        public string Name { get; set; }
        public string Url { get; set; }
        
        public List<Card> Cards { get; set; }
    }
}