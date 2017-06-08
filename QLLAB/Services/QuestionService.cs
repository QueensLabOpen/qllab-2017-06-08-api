using QLLAB.Data;
using QLLAB.Models;
using QLLAB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLLAB.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly QlLabContext _context;

        public QuestionService(QlLabContext context)
        {
            _context = context;

        }

        public Question GetRandom()
        {
            var tags = _context.Images.Select(i => i.Tags).ToList();
            var distinct = ExtractDistinct(tags);
            Random rnd = new Random();
            int max = rnd.Next(distinct.Count);
            string winningTag = distinct[max];

            List<Image> possibleWinners = _context.Images.Where(i => i.Tags.Contains(winningTag)).ToList();
            Question question = new Question(winningTag);
            Answer winner = new Answer(possibleWinners[max].Url, true);
            question.Answers.Add(winner);

            List<Image> losers = _context.Images.Where(i => i.Tags.Contains(winningTag)).ToList();
            Answer loserA = new Answer(losers[rnd.Next(losers.Count)].Url, false);
            Answer loserB = new Answer(losers[rnd.Next(losers.Count)].Url, false);
            Answer loserC = new Answer(losers[rnd.Next(losers.Count)].Url, false);

            question.Answers.Add(loserA);
            question.Answers.Add(loserB);
            question.Answers.Add(loserC);


            return question;

        }

        private List<string> ExtractDistinct(List<string> tags)
        {
            List<string> splitTags = new List<string>();
            foreach (string tag in tags)
            {
                var temp = tag.Split(',').ToList();
                splitTags.AddRange(temp);
            }
            List<string> distinct = new List<string>();
            for (int i=0; i < splitTags.Count; i++)
            {
                if (splitTags[i] != splitTags[i - 1])
                {
                    distinct.Add(splitTags[i].Trim());
                }
            }
            return distinct;
        }
    }
}
