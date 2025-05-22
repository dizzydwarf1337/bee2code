using Domain.Models.LabTesting;
using Domain.Models.Links;
using Domain.Models.Researches;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? HouseNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<UserResearch>? PatientResearches { get; set; } = new List<UserResearch>();
        public virtual ICollection<LabTest>? LabTests { get; set; } = new List<LabTest>();
        public virtual ICollection<UserNotification>? Notifications { get; set; } = new List<UserNotification>();
        public virtual ICollection<Research>? MyResearch { get; set; } = new List<Research>();// Created researches by uesr

    }
}
