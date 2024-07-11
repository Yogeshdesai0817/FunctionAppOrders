using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class HttpClientFetcher: IDataFetcher
    {
        private readonly HttpClient _httpClient;

        public HttpClientFetcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Method gets details from hosted client
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> FetchDataAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
    }
}
