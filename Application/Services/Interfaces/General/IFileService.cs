﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.General
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file);
        Task DeleteFile(string FilePath);
    }
}
