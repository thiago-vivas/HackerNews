using HackerNews.Models;
using HackerNews.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Manager
{
    public class StoryManager : IStoryManager
    {
        private readonly IProvider provider;
        public StoryManager(IProvider provider)
        {
            this.provider = provider;
        }
        public async Task<IEnumerable<Story>> GetBestStories(int count, bool countCommentsInsideComments = false)
        {
            List<Story> result = new List<Story>();
            try
            {
                var storyIds = await provider.GetBestStoriesIds(count);

                await Task.Run(() =>
                {
                    Parallel.ForEach(storyIds, (storyId) =>
                    {
                        result.Add(this.GetStoryDetail(storyId, countCommentsInsideComments).Result);
                    });
                });
            }
            catch (Exception ex)
            {
                //log instead of throwing
                throw ex;
            }
            return result.OrderByDescending(x => x.Score);
        }

        private async Task<Story> GetStoryDetail(int id, bool countCommentsInsideComments)
        {
            Story story = new Story();
            try
            {
                var storyDto = await provider.GetStoryDetail(id);
                story.CommentCount = storyDto.kids != null ? storyDto.kids.Count() : 0;
                story.PostedBy = storyDto.by;
                story.Score = storyDto.score;
                story.Time = UnixTimeToDateTime(storyDto.time);
                story.Title = storyDto.title;
                story.URI = storyDto.url;

                if (storyDto.kids != null && countCommentsInsideComments)
                {
                    await Task.Run(() =>
                    {
                        Parallel.ForEach(storyDto.kids, (commentId) =>
                        {
                            story.CommentCount += GetCommentCount(commentId).Result;
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return story;
        }
        private DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        private async Task<int> GetCommentCount(int id)
        {
            int count = 0;
            try
            {
                var comment = await provider.GetComment(id);
                if (comment == null || comment.kids == null)
                    return 0;

                count = comment.kids.Count;

                if (comment.kids != null)
                {
                    await Task.Run(() =>
                    {
                        Parallel.ForEach(comment.kids, (commentId) =>
                        {
                            count += GetCommentCount(commentId).Result;
                        });

                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }
    }
}
