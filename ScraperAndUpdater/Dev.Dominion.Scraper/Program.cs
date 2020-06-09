using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dev.Dominion.Scraper.Framework;
using Dev.Dominion.Scraper.Models;
using Dev.Dominion.Scraper.Parsing;
using Newtonsoft.Json;

namespace Dev.Dominion.Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootPath = @"C:\DEV\Private\Dominion\Cards";
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Downloading Dominion cards");
            Console.WriteLine("Target path to dump files: " + rootPath);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();
            Console.Write("Performing setup and getting root card lists: ");

            var settings = new Settings();
            var parser = new SetParser(settings, new HttpGetter(), new CardTypeMapper());

            var sets = new List<Set>();
            foreach (var name in settings.SetNames())
            {
                var set = parser.Parse(name);
                sets.Add(set);
                Console.Write("#");
            }
            Console.WriteLine(" complete.");
            Console.WriteLine();
            
            foreach (var set in sets)
            {
                Console.WriteLine("Downloading " + set.Name + ": ");

                var setNameSafe = set.Name.ToLower().Replace(" ", "").Trim();
                
                foreach (var card in set.Cards)
                {
                    Console.Write("#");

                    var cardNameSafe = card.Name.ToLower().Replace(" ", "").Trim();
                    var url = settings.ImageUrl(card.ImageUrl);
                    card.LocalImageFileName = $@"\{setNameSafe}\{cardNameSafe}.jpg";
                    var cardFileName = rootPath + card.LocalImageFileName;
                    ImageGetter.Download(url, cardFileName);
                }

                var json = JsonConvert.SerializeObject(set);

                var path = rootPath + @"\" + setNameSafe;
                if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                var filePath = path + "\\cards.dom";
                if (File.Exists(filePath)) File.Delete(filePath);
                File.AppendAllText(filePath, json);

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Completed everything.");
            Console.ReadKey();

        }
    }
}
