﻿using System;
using System.Threading.Tasks;
using QLLAB.Models;

namespace QLLAB.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task SaveImageAsync(Image image);
        Image GetImage(Guid guid);
    }
}
