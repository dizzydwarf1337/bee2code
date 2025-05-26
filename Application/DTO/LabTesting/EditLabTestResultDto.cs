using Domain.Models.LabTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.LabTesting
{
    public class EditLabTestResultDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Result { get; set; }
    }
}
