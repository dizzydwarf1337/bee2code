using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Researches
{
    public class CreateUserResearchDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ResearchId { get; set; }
        [Required]
        public IFormFile AcceptanceFile { get; set; }
    }
}
