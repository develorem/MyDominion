using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dev.Dominion.Scraper.Framework
{
    public class HttpGetter
    {
        public async Task<string> Get(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Received status code {response.StatusCode} requesting {url}");
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

    }
    


}