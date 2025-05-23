using Application.Services.Interfaces.General;
using Domain.Exceptions.BusinessExceptions;
using Domain.Exceptions.DataExceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.General
{
    public class FileService : IFileService
    {
        private readonly string _baseFilePath = "wwwroot/";

        public async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new InvalidDataProvidedException("File", "", "FileService.SaveFile");
            }

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_baseFilePath, "uploads", fileName);

            Directory.CreateDirectory(_baseFilePath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("uploads", fileName).Replace("\\", "/");
        }

        public Task DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new EntityNotFoundException("File path");
            }

            string fullPath = Path.Combine(_baseFilePath, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            else
            {
                throw new EntityNotFoundException("File");
            }

            return Task.CompletedTask;
        }
    }
}

