using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GenWords
{
	public static class WordHelp
	{
		private static HttpClient httpClient = new HttpClient();
        const string Host = "http://poiskslova.com";
        public static string GetRandomWord(string path)
        {
            Random r = new Random();
            string file = path;
            int count = 0;
            int offset = 0;
            using (StreamReader streamReader = new StreamReader(file))
            {
                while (!streamReader.EndOfStream)
                {
                    streamReader.ReadLine();
                    count++;
                };
            };
            int linePos = r.Next(0, count);
            using (StreamReader streamReader = new StreamReader(file))
            {
                while (!streamReader.EndOfStream)
                {
                    if (linePos > offset)
                    {
                        streamReader.ReadLine();
                        offset++;
                    }
                    else return streamReader.ReadLine();
                };
            };

            return "";
        }

        private static string Geturl(string word) =>
           $"{Host}/search-by-params/word-generator?query={word}";

        public static List<string> Combine(string word)=>
				ParseData(httpClient.GetStringAsync(Geturl(word)).GetAwaiter().GetResult());

		/* public static async Task<List<string>> CombineAsync(string word)=>
				ParseData(await httpClient.GetStringAsync(Geturl(word)));
		*/

		private static List<string> ParseData(string data)
		{
			HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
			htmlDocument.LoadHtml(data);
            var res = htmlDocument.DocumentNode.SelectNodes("/html/body/div[5]/div/div[2]/div[3]/div/div[2]/ul/li");
            return res.Select(x => x.InnerText).ToList();
		}
	}
}
