using Domain.Enums.LabTesting;
using Domain.Models.LabTesting;
using Domain.Models.Researches;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.LabTesting
{
    public class EditLabTestDto
    {
        [Required]
        public string Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public LabTestType LabTestType { get; set; } 

        public DateTime? Date { get; set; }
    }
}
