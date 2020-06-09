namespace Dev.Dominion.Scraper.Models
{
    public class Card
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string SetName { get; set; }
        public string ImageUrl { get; set; }
        public string LocalImageFileName { get; set; }

        public CardType CardType { get; set; }

        public int Cost { get; set; }

        public string SpecialText { get; set; }
        public string FullText { get; set; }

        public int PlusBuy { get; set; }
        public int PlusAction { get; set; }
        public int PlusCard { get; set; }
        public int PlusCoin { get; set; }
        public int PlusVictory { get; set; }

        public bool HasSpecialEffect { get; set; }
    }
}