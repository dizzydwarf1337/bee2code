using Domain.Models.LabTesting;
using Domain.Models.Links;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Researches
{
    public class Research
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        public virtual User? Owner { get; set; }

        public virtual ICollection<LabTest>? LabTests { get; set; } = new List<LabTest>();
        public virtual ICollection<UserResearch>? Patients { get; set; } = new List<UserResearch>();

    }
}
