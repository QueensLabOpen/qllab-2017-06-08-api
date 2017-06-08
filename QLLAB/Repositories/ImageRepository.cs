using System;
using System.Threading.Tasks;
using QLLAB.Data;
using QLLAB.Models;
using QLLAB.Repositories.Interfaces;

namespace QLLAB.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly QlLabContext _labContext;
        public ImageRepository(QlLabContext labContext)
        {
            _labContext = labContext;
        }

        public async Task SaveImage(Image image)
        {
            _labContext.Images.Add(image);
            await _labContext.SaveChangesAsync();
        }
    }
}
