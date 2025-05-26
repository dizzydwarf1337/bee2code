using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.LabTesting
{
    public class CreateLabTestResultDto
    {
        [Required]
        public string Result { get; set; }
        [Required]
        public string LabTestId { get; set; }
    }
}
