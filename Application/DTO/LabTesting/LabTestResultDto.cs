using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.LabTesting
{
    public class LabTestResultDto
    {
        public string Id { get; set; }
        public string Result { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string LabTestId { get; set; }
    }
}
