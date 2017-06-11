using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QLLAB.Models;

namespace QLLAB.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task SaveImageAsync(Image image);
        Image GetImage(Guid guid);
        List<Image> GetImages(Expression<Func<Image, bool>> predicate);
    }
}
