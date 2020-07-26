using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MicroMessager.SDKs.Models;

namespace MicroMessager.SDKs.Services
{
    public class HTTPService
    {
        private readonly HttpClient _client;
        private readonly Regex _regex;

        public HTTPService(
            IHttpClientFactory clientFactory)
        {
            _regex = new Regex("^https://", RegexOptions.Compiled);
            _client = clientFactory.CreateClient();
        }

        public async Task<string> Get(CoreUri url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url.ToString())
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>())
            };

            request.Headers.Add("accept", "application/json");

            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new WebException($"The remote server returned unexpcted status code: {response.StatusCode} - {response.ReasonPhrase}.");
            }
        }

        public async Task<string> Post(CoreUri url, CoreUri postDataStr)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url.Address)
            {
                Content = new FormUrlEncodedContent(postDataStr.Params)
            };

            request.Headers.Add("accept", "application/json");

            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new WebException($"The remote server returned unexpcted status code: {response.StatusCode} - {response.ReasonPhrase}.");
            }
        }
    }
}
