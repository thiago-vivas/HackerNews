using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Models
{
    public class CommentDTO
    {
        public string by { get; set; }
        public int id { get; set; }
        public List<int> kids { get; set; }
        public int parent { get; set; }
        public string text { get; set; }
        public int time { get; set; }
        public string type { get; set; }
    }
}
