using QLLAB.Data;
using QLLAB.Models;
using QLLAB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var rnd = new Random();
            int max = rnd.Next(distinct.Count);
            string winningTag = distinct[max];

            var possibleWinners = _context.Images.Where(i => !i.Tags.Contains(winningTag)).ToList();
            var question = new Question(winningTag);

            if(possibleWinners.Count < 1)
                return question;

            var winner = new Answer(possibleWinners[max].Url, true);
            question.Answers.Add(winner);

            var losers = _context.Images.Where(i => i.Tags.Contains(winningTag)).ToList();
            var loserA = new Answer(losers[rnd.Next(losers.Count)].Url, false);
            var loserB = new Answer(losers[rnd.Next(losers.Count)].Url, false);
            var loserC = new Answer(losers[rnd.Next(losers.Count)].Url, false);

            question.Answers.Add(loserA);
            question.Answers.Add(loserB);
            question.Answers.Add(loserC);

            return question;
        }

        private static List<string> ExtractDistinct(IEnumerable<string> tags)
        {
            var splitTags = new List<string>();
            foreach (var tag in tags)
            {
                var temp = tag.Split(',').ToList();
                splitTags.AddRange(temp);
            }
            var distinct = new List<string>();
            for (var i=0; i < splitTags.Count; i++)
            {
                if (i == 0)
                {
                    distinct.Add(splitTags[0]);
                    continue;
                }

                if (i != 0 && splitTags[i] != splitTags[i - 1])
                {
                    distinct.Add(splitTags[i].Trim());
                }
            }
            return distinct;
        }
    }
}
