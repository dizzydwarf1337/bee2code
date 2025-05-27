using Application.DTO.Researches;
using Application.Services.Interfaces.General;
using Domain.Exceptions.BusinessExceptions;
using Domain.Exceptions.DataExceptions;
using Domain.Models.Links;
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

            string uploadsFolder = Path.Combine(_baseFilePath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_baseFilePath, "uploads", fileName);


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

        public async Task<AcceptanceDownloadDto> DownloadFile(string FilePath)
        {
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FilePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
            var content = await File.ReadAllBytesAsync(absolutePath);
            var extension = Path.GetExtension(absolutePath).ToLowerInvariant();
            return new AcceptanceDownloadDto
            {
                Content = content,
                FileName = $"Acceptance{extension}",
                ContentType = extension == ".pdf" ? "application/pdf" : "image/jpg"
            };
        }
    }
}

