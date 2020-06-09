namespace Dev.Dominion.Scraper.Tests
{
    public static class CardTestData
    {
        public static string ExamplePage(string set, string card)
        {
            var html = ExampleOneCard(set, card);
            return
                $@"<html><head></head><body><table><tr><td></td></tr></table><div></div><table>{html}</table><div></div><table><tr><td></td></tr></table></body></html>";
        }

        public static string ExampleOneCard(string set, string card)
        {
            var imageCard = card.ToLower().Replace(" ", "");
            var setLower = set.ToLower().Replace(" ", "");

            return $@"<tr class=""card-row action"">
          <td><a href=""./?card=!{imageCard}"">
             <img class=""card-img"" src=""./scans/{set}/{imageCard}.jpg"" title=""{card}"" alt=""{card}""></a></td>
          <td class=""card-name""><b><a href=""./?card=!{imageCard}"">{card}</a></b></td>
         <td class=""card-expansion""><a href=""./?set={setLower}"">{set}</a></td>
         <td class=""card-type"">Action</td>
         <td class=""card-cost"">$2</td>
         <td class=""card-rules"">+1 Buy<br>+1 Coin<br>When you discard this from play, you may put one of your Treasures from play on top of your deck.</td>
         </tr>";
        }
    }
}