using Domain.Enums.LabTesting;
using Domain.Models.Researches;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.LabTesting
{
    public class LabTest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public LabTestType LabTestType { get; set; } = LabTestType.Other;

        [Required]
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public Guid CreatorId{ get; set; }
        [ForeignKey("CreatorId")]
        public virtual User? Creator { get; set; }
        
        public Guid PatientId{ get; set; }
        [ForeignKey("PatientId")]
        public virtual User? Patient { get; set; }

        public Guid ResearchId { get; set; }
        [ForeignKey("ResearchId")]
        public virtual Research? Research { get; set; }
        public Guid? LabTestResultId { get; set; }
        public virtual LabTestResult? LabTestResult { get; set; }
    }
}
