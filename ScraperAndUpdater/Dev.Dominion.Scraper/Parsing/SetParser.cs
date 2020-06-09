using System;
using Dev.Dominion.Scraper.Framework;
using Dev.Dominion.Scraper.Models;

namespace Dev.Dominion.Scraper.Parsing
{
    public class SetParser
    {
        private readonly Settings _settings;
        private readonly HttpGetter _httpGetter;
        private readonly CardTypeMapper _cardTypeMapper;

        public SetParser(Settings settings, HttpGetter httpGetter, CardTypeMapper cardTypeMapper)
        {
            _settings = settings;
            _httpGetter = httpGetter;
            _cardTypeMapper = cardTypeMapper;
        }

        // http://dominion.diehrstraits.com/?set=2nd
        // <div class="card-container"> $0 P <b>Transmute</b><br><a class="card-link" href="./?card=!transmute"><img class="card-img card-img-small" src="./scans/alchemy/transmute.jpg" title="Transmute" alt="Transmute"></a></div>
        // <div class="card-container"> $2 P <b>Scrying Pool</b><br>
        //  <a class="card-link" href="./?card=!scryingpool">
        //  <img class="card-img card-img-small" src="./scans/alchemy/scryingpool.jpg" title="Scrying Pool" alt="Scrying Pool">
        // </a></div>

        public Set Parse(string setname)
        {
            var set = new Set { Name = setname};
            set.Url = _settings.SetUrl(setname);

            var html = _httpGetter.Get(set.Url).Result;

            ParseSetHtml(set, html);

            foreach (var card in set.Cards)
            {
                var cardUrl = _settings.CardUrl(card.Url);
                var cardHtml = _httpGetter.Get(cardUrl).Result;
                ParseCardHtml(card, cardHtml);
                card.SetName = set.Name;
            }

            return set;
        }


        // http://dominion.diehrstraits.com/?card=!herbalist
        // <tr class="card-row action">
        //  <td><a href="./?card=!herbalist">
        //     <img class="card-img" src="./scans/alchemy/herbalist.jpg" title="Herbalist" alt="Herbalist"></a></td>
        //  <td class="card-name"><b><a href="./?card=!herbalist">Herbalist</a></b></td>
        // <td class="card-expansion"><a href="./?set=Alchemy">Alchemy</a></td>
        // <td class="card-type">Action</td>
        // <td class="card-cost">$2</td>
        // <td class="card-rules">+1 Buy<br>+1 Coin<br>When you discard this from play, you may put one of your Treasures from play on top of your deck.</td>
        // </tr>
        public void ParseCardHtml(Card card, string html)
        {
            var cardRow = GetCardPageCardRow(html);
            card.ImageUrl = GetImageUrl(cardRow);
            card.CardType = GetCardType(cardRow);
            card.Cost = GetCardCost(cardRow);
            GetCardRules(card, cardRow);
        }

        public void GetCardRules(Card card, string cardRow)
        {
            var cv = GetCellValue(cardRow, "card-rules");

            var rules = cv.Split(new[] { "<br>", "<br />", "<br/>", "<BR>" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var rule in rules)
            {
                ProcessRule(card, rule);
            }
        }

        public void ProcessRule(Card card, string rule)
        {
            if (rule.ToLower().Contains("buy") && rule.StartsWith("+"))
            {
                card.PlusBuy = GetNumberForRule(rule);
            }
            else if (rule.ToLower().Contains("coin") && rule.StartsWith("+"))
            {
                card.PlusCoin = GetNumberForRule(rule);
            }
            else if (rule.ToLower().Contains("action") && rule.StartsWith("+"))
            {
                card.PlusAction = GetNumberForRule(rule);
            }
            else if (rule.ToLower().Contains("card") && rule.StartsWith("+"))
            {
                card.PlusCard = GetNumberForRule(rule);
            }
            else
            {
                card.SpecialText = rule;
            }
            card.FullText = rule.Replace("<br>", ", ").Replace("<br/>", ", ").Replace("<br />", ", ").Replace("<BR>", ", ").Replace("<BR/>", ", ");
        }

        public int GetNumberForRule(string rule)
        {
            var num = rule.Substring(1, 1);
            int val;
            if (int.TryParse(num, out val)) return val;
            return 0;
        }

        public int GetCardCost(string cardRow)
        {
            var cv = GetCellValue(cardRow, "card-cost");
            cv = cv.Replace("$", "").Trim();
            int val;
            if (int.TryParse(cv, out val)) return val;
            return -1;
        }

        public CardType GetCardType(string cardRow)
        {
            var cv = GetCellValue(cardRow, "card-type");
            return _cardTypeMapper.Find(cv);
        }

        public static string GetImageUrl(string cardRow)
        {
            var text = FindTextBetween("<img class=\"card-img\" src=\"", "\" title=\"", cardRow, 0);
            if (text == null) text = FindTextBetween("<img class='card-img' src='", "' title='", cardRow, 0);
            return text;
        }

        public static string GetCellValue(string html, string className)
        {
            var startMarker = $@"<td class=""{className}"">";
            var text = FindTextBetween(startMarker, "</td>", html, 0);
            if (text == null)
            {
                startMarker = $@"<td class='{className}'>";
                text = FindTextBetween(startMarker, "</td>", html, 0);
            }
            return text;
        }

        public static string GetCardPageCardRow(string html)
        {
            var text = FindTextBetween("<tr class=\"card-row", "</tr>", html, 0);
            if (text == null) text = FindTextBetween("<tr class='card-row", "</tr>", html, 0);
            return text;
        }

        public static void ParseSetHtml(Set set, string html)
        {
            var cardIndexSearchFrom = 0;

            var cardRow = GetCardRow(cardIndexSearchFrom, html, out cardIndexSearchFrom);
            while (cardRow != null)
            {
                var card = new Card();
                card.Name = GetCardName(cardRow);
                card.Url = GetCardUrl(cardRow);
                set.Cards.Add(card);

                cardRow = GetCardRow(cardIndexSearchFrom, html, out cardIndexSearchFrom);
            }
        }

        public static string GetCardRow(int start, string html, out int nextIndex)
        {
            const string StartCardRow = "<div class='card-container'>";
            const string EndCardRow = "</div>";

            var text = FindTextBetween(StartCardRow, EndCardRow, html, start, out nextIndex);
            return text;
        }

        public static string GetCardName(string cardRow)
        {
            return FindTextBetween("<b>", "</b>", cardRow, 0);
        }

        public static string GetCardUrl(string cardRow)
        {
            var match =  FindTextBetween("<a class=\"card-link\" href=\"", "\">", cardRow, 0);
            if (match == null) match = FindTextBetween("<a class='card-link' href='", "'>", cardRow, 0);
            return match;
        }

        private static string FindTextBetween(string startMarker, string endMarker, string text, int startFrom, out int nextStart)
        {
            nextStart = startFrom;
            var from = text.IndexOf(startMarker, startFrom);
            if (from < 0) return null;
            var until = text.IndexOf(endMarker, from + startMarker.Length);
            if (until < 0) return null;

            var begin = from + startMarker.Length;
            var end = until - begin;
            nextStart = until + endMarker.Length;
            return text.Substring(begin, end);
        }

        private static string FindTextBetween(string startMarker, string endMarker, string text, int startFrom)
        {
            var from = text.IndexOf(startMarker, startFrom);
            if (from < 0) return null;
            var until = text.IndexOf(endMarker, from + startMarker.Length);
            if (until < 0) return null;

            var begin = from + startMarker.Length;
            var end = until - begin;

            return text.Substring(begin, end);
        }
    }
}