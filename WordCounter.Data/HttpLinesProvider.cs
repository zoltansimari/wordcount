using System;
using System.Collections.Generic;
using System.Net.Http;

namespace WordCounter.Data
{
    public class HttpLinesProvider : ILinesProvider
    {
        public IEnumerable<string> GetLines(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(url).Result;
                return response.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
