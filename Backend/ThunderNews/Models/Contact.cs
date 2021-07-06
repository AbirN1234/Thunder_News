using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThunderNews.Models
{
    public class Contact
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string comment { get; set; }

        public string sentAt { get; set; }
    }
}
