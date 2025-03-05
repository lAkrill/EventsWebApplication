using AutoMapper;
using EventsWebApplication.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EventsWebApplication.Infrastructure
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;
        public LocalFileStorageService(string basePath)
        {
            _basePath = basePath;
        }
        public Task DeleteFile(string filePath, string containerName)
        {
            var absolutePath = Path.Combine(_basePath, filePath);
            
            if (File.Exists(absolutePath))
            {
                File.Delete(absolutePath);
            }

            return Task.CompletedTask;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string containerName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File can't be empty", nameof(file));

            var containerPath = Path.Combine(_basePath, containerName);
            if (!Directory.Exists(containerPath))
            {
                Directory.CreateDirectory(containerPath);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(containerPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Можно возвращать относительный путь, который потом соберется в URL
            var relativePath = Path.Combine(containerName, fileName).Replace("\\", "/");
            return relativePath;
        }
    }
}
