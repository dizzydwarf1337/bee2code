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
    public class LabTestDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string? Description { get; set; }
        public LabTestType LabTestType { get; set; } = LabTestType.Other;
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid ResearchId { get; set; }
        public Guid? LabTestResultId { get; set; }
    }
}
