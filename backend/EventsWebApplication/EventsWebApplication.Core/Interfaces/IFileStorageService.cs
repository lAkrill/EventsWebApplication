﻿using Microsoft.AspNetCore.Http;

namespace EventsWebApplication.Core.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string containerName);
        Task DeleteFile(string filePath, string containerName);
    }
}
