using HackerNews.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNews.Provider
{
    public class HackerNewsProvider : IProvider
    {
        public async Task<CommentDTO> GetComment(int id)
        {
            CommentDTO result = new CommentDTO();
            using (var client = new HttpClient())
            {
                var json_data = string.Empty;
                try
                {
                    var request = await client.GetAsync($@"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                    request.EnsureSuccessStatusCode();

                    json_data = await request.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<CommentDTO>(json_data);
                }
                catch (Exception ex)
                {
                    throw ex;
                    //log it instead of throwing
                }
            }
            return result;
        }

        public async Task<StoryDTO> GetStoryDetail(int id)
        {
            StoryDTO result = new StoryDTO();
            using (var client = new HttpClient())
            {
                var json_data = string.Empty;
                try
                {
                    var request = await client.GetAsync($@"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                    request.EnsureSuccessStatusCode();

                    json_data = await request.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<StoryDTO>(json_data);

                }
                catch (Exception ex)
                {
                    throw ex;
                    //log it instead of throwing
                }
            }
            return result;
        }

        public async Task<IEnumerable<int>> GetBestStoriesIds(int count)
        {
            List<int> result = new List<int>();
            using (var client = new HttpClient())
            {
                var json_data = string.Empty;
                try
                {
                    var request = await client.GetAsync(@"https://hacker-news.firebaseio.com/v0/beststories.json");
                    request.EnsureSuccessStatusCode();

                    json_data = await request.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<int>>(json_data);
                }
                catch (Exception ex)
                {
                    throw ex;
                    //log it instead of throwing
                }
            }
            return result.Take(count).ToList();
        }
    }
}
