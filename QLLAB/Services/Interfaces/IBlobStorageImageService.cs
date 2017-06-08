using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLLAB.Models;

namespace QLLAB.Services.Interfaces
{
    public interface IBlobStorageImageService
    {
        void Save(string base64, string originalFilename);
        Task<Content> SaveAsync(Content uploadImage);
        Task<Content> GetAsync(Guid id);
    }
}
