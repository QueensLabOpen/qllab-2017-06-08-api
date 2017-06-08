using System;
using System.Linq;
using System.Threading.Tasks;
using QLLAB.Data;
using QLLAB.Models;
using QLLAB.Repositories.Interfaces;

namespace QLLAB.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly QlLabContext _labContext;
        private const string BasePath = "http://qllab1-api.azurewebsites.net/api/content/{0}";

        public ImageRepository(QlLabContext labContext)
        {
            _labContext = labContext;
        }

        public async Task SaveImageAsync(Image image)
        {
            _labContext.Images.Add(image);
            await _labContext.SaveChangesAsync();
        }

        public Image GetImage(Guid id)
        {
            var first = _labContext.Images.FirstOrDefault(i => i.Url.Equals(string.Format(BasePath, id)));
            return first;
        }
    }
}
