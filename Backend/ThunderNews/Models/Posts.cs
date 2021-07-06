using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThunderNews.Models
{
    public class Posts
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int category { get; set; }
        public int isActive { get; set; }
        public string postedAt { get; set; }
    }
}
