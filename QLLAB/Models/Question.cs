using System.Collections.Generic;

namespace QLLAB.Models
{
    public class Question
    {
        public string WinningTag { get; set; }
        public List<Answer> Answers { get; set; }

        public Question(string winningTag)
        {
            WinningTag = winningTag;
            Answers = new List<Answer>();
        }
    }
}
