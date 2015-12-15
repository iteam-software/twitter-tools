using Microsoft.Extensions.WebEncoders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ITeamSoftware.Tools.Twitter
{
    public class TwitterApplicationOnlyService : ITwitterApplicationOnlyService
    {
        private const string _applicationOnlyTokenEndpoint = "https://api.twitter.com/oauth2/token";
        private const string _userTimelineEndpoint = "https://api.twitter.com/1.1/statuses/user_timeline.json";
        private const string _contentType = "application/x-www-form-urlencoded;charset=UTF-8";
        private readonly TwitterToolsOptions _options;

        private HttpClient Client { get; set; } = new HttpClient();

        public TwitterApplicationOnlyService(TwitterToolsOptions options)
        {
            _options = options;
        }

        public async Task<TwitterTweet[]> GetTimeline(string token, string screenName)
        {
            var endpoint = $"{_userTimelineEndpoint}?screen_name={screenName}&exclude_replies=false&count=3";

            var message = new HttpRequestMessage(HttpMethod.Get, endpoint);
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UrlEncoder.Default.UrlEncode(token));

            var response = await Client.SendAsync(message);
            
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<TwitterTweet[]>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> GetTokenAsync()
        {
            var encoded = EncodeBase64($"{_options.ConsumerKey}:{_options.ConsumerSecret}");

            var message = new HttpRequestMessage(HttpMethod.Post, _applicationOnlyTokenEndpoint);
            message.Headers.Authorization = new AuthenticationHeaderValue("Basic", encoded);
            message.Content = new FormUrlEncodedContent(new[] { new KeyValuePair<string,string>( "grant_type", "client_credentials" )});
            message.Content.Headers.ContentType.CharSet = "UTF-8";

            var response = await Client.SendAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var strData = Uri.UnescapeDataString(await response.Content.ReadAsStringAsync());
            var data = JsonConvert.DeserializeObject<TwitterAuthenticationResponse>(strData);

            return data.access_token;
        }

        private string EncodeBase64(string data)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }
    }
}