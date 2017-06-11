using QLLAB.Models;
using QLLAB.Services.Interfaces;
using System;
using System.Linq;
using Microsoft.Extensions.Options;
using QLLAB.Repositories.Interfaces;

namespace QLLAB.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public QuestionService(IImageRepository imageRepository, IOptions<AppSettings> appSettings)
        {
            _imageRepository = imageRepository;
            _appSettings = appSettings;
        }

        public Question GetRandom()
        {
            var winningTag = _appSettings.Value.WinningTag;

            var possibleWinners = _imageRepository.GetImages(i => i.Tags.Contains(winningTag));
            var question = new Question(winningTag);

            if(possibleWinners.Count < 1)
                return question;

            var rnd = new Random();
            var randomWinner = rnd.Next(possibleWinners.Count);
            var winner = new Answer(possibleWinners[randomWinner].Url, true);
            question.Answers.Add(winner);

            var losers = _imageRepository.GetImages(i => !i.Tags.Contains(winningTag)).ToList();
            var loserA = new Answer(losers[rnd.Next(losers.Count)].Url, false);
            var loserB = new Answer(losers[rnd.Next(losers.Count)].Url, false);
            var loserC = new Answer(losers[rnd.Next(losers.Count)].Url, false);

            question.Answers.Add(loserA);
            question.Answers.Add(loserB);
            question.Answers.Add(loserC);

            return question;
        }
    }
}
