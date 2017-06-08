using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLLAB.Models
{
    public class Answer
    {
        public string url { get; set; }
        public string[] tags { get; set; }

        public Answer(string url, string[] tags) 
        {
            this.url = url;
            this.tags = tags;
        }
    }
}
