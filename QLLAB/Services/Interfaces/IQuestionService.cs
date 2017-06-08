using QLLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLLAB.Services.Interfaces
{
    public interface IQuestionService
    {
        Question GetRandom();
    }
}
