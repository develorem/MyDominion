using System.Collections.Generic;
using System.Web;

namespace Dev.Dominion.Scraper.Parsing
{
    public class Settings
    {
        public string BaseUrl { get; set; }

        public Settings()
        {
            BaseUrl = @"http://dominion.diehrstraits.com";
        }

        public string SetUrl(string setName)
        {
            return $"{BaseUrl}/?set=" + HttpUtility.UrlEncode(setName);
        }

        // ./?card=!ruinedlibrary
        public string CardUrl(string relativeUrl)
        {
            return relativeUrl.Replace(".", BaseUrl);
        }

        public string ImageUrl(string relativeUrl)
        {
            return BaseUrl + relativeUrl.Substring(1);
        }

        public IEnumerable<string> SetNames()
        {
            yield return "Base";
            yield return "Intrigue";
            yield return "Seaside";
            yield return "Alchemy";
            yield return "Prosperity";
            yield return "Cornucopia";
            yield return "Hinterlands";
            yield return "Dark Ages";
            yield return "Guilds";
            yield return "Adventures";
            yield return "Promo";
            yield return "Base Cards";
            yield return "Common";
            yield return "2nd";

        }
    }
}