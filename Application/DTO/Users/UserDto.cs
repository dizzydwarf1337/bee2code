using Application.DTO.LabTesting;
using Application.DTO.Researches;
using Domain.Models.LabTesting;
using Domain.Models.Researches;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? HouseNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public ICollection<ResearchPreviewDto> PatientResearches { get; set; } = new List<ResearchPreviewDto>();
        public ICollection<LabTestDto> LabTests { get; set; } = new List<LabTestDto>();
        public ICollection<UserNotificationDto> Notifications { get; set; } = new List<UserNotificationDto>();
        public ICollection<ResearchPreviewDto> MyResearch { get; set; } = new List<ResearchPreviewDto>();
    }
}
