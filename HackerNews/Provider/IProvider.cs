using HackerNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Provider
{
    public interface IProvider
    {
        /// <summary>
        /// get the best stories id
        /// </summary>
        /// <param name="count">quantity of best stories to be returned</param>
        /// <returns>list with best stories' ids</returns>
        Task<IEnumerable<int>> GetBestStoriesIds(int count);

        /// <summary>
        /// get story detail
        /// </summary>
        /// <param name="id">id of the story</param>
        /// <returns><see cref="StoryDTO"/> </returns>
        Task<StoryDTO> GetStoryDetail(int id);

        /// <summary>
        /// get comments of stories
        /// </summary>
        /// <param name="id"> id of the comment</param>
        /// <returns><see cref="CommentDTO"/></returns>
        Task<CommentDTO> GetComment(int id);
    }
}
