using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QLLAB.Data;
using QLLAB.Models;
using QLLAB.Repositories.Interfaces;

namespace QLLAB.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly QlLabContext _labContext;
        private readonly IOptions<AppSettings> _appSettings;

        public ImageRepository(QlLabContext labContext, IOptions<AppSettings> appSettings)
        {
            _labContext = labContext;
            _appSettings = appSettings;
        }

        public async Task SaveImageAsync(Image image)
        {
            _labContext.Images.Add(image);
            await _labContext.SaveChangesAsync();
        }

        public Image GetImage(Guid id)
        {
            var first = _labContext.Images.FirstOrDefault(i => i.Url.Equals(string.Format(_appSettings.Value.ImageBasePath, id)));
            return first;
        }

        public List<Image> GetImages(Expression<Func<Image, bool>> predicate)
        {
            return _labContext.Images.Where(predicate).ToList();
        }
    }
}
