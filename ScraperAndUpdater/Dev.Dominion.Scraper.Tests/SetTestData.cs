namespace Dev.Dominion.Scraper.Tests
{
    public static class SetTestData
    {
        public static string ExampleSetOneCard(string set, string card)
        {
            var oneCard = ExampleOneCard(set, card);
            return $"<div class=\"set\"><div class=\"Title\">{set}</div>{oneCard}</div>";
        }

        public static string ExampleSetMultipleCards(string set)
        {
            var text = "<table>";
            text += ExampleOneCard(set, "Herbalist");
            text += ExampleOneCard(set, "Alchemist");
            text += ExampleOneCard(set, "Drug Dealer");
            text += ExampleOneCard(set, "Botanist");
            text += "</table>";
            return text;
        }

        public static string ExampleOneCard(string set, string card)
        {
            var inner = ExampleOneCardInner(set, card);
            return $@"<div class=""card-container"">{inner}</div>";
        }

        public static string ExampleOneCardInner(string set, string card)
        {
            var imageCard = card.ToLower().Replace(" ", "");
            var setLower = set.ToLower().Replace(" ", "");

            return $@" $2 P <b>{card}</b><br><a class=""card-link"" href=""./?card=!{imageCard}""><img class=""card-img card-img-small"" src=""./scans/{setLower}/{imageCard}.jpg"" title=""{card}"" alt=""{card}""></a>";

        }
    }
}