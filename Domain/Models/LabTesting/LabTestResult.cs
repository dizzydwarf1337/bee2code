using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.LabTesting
{
    public  class LabTestResult
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Result { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
      
        [ForeignKey("LabTest")]
        public Guid LabTestId { get; set; }
        public virtual LabTest? LabTest { get; set; }
    }
}
