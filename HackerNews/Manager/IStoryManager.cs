using HackerNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Manager
{
    public interface IStoryManager
    {
        /// <summary>
        /// Get the best stories
        /// </summary>
        /// <param name="count">quantity of stories to be returned</param>
        /// <param name="countCommentsInsideComments">if should count comments inside comments as well</param>
        /// <returns>the best stories list ordered by score</returns>
        Task<IEnumerable<Story>> GetBestStories(int count, bool countCommentsInsideComments = false);
    }
}
