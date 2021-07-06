using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThunderNews.Models
{
    public class Comment
    {
        public int id { get; set; }
        public int postId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string comment { get; set; }
        public int isActive { get; set; }
        public string postDate { get; set; }
    }
}
