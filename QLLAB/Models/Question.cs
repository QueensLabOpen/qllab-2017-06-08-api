using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLLAB.Models
{
    public class Question
    {
        public string winningTag { get; set; }
        public List<Answer> answers { get; set; }

        public Question(string winningTag)
        {
            this.winningTag = winningTag;
            this.answers = new List<Answer>();
        }
    }
}
