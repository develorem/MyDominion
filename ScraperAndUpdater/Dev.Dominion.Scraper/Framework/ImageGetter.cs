using System.Net;

namespace Dev.Dominion.Scraper.Framework
{
    public class ImageGetter
    {
        public static void Download(string url, string filePath)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, filePath);
            }
        }
    }
}