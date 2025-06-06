﻿using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Users;
using Application.DTO.LabTesting;

namespace Application.DTO.Researches
{
    public class ResearchDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid OwnerId { get; set; }
        public List<UserPreviewDto> Patients { get; set; } = new List<UserPreviewDto>();
        public List<LabTestDto> LabTest { get; set; } = new List<LabTestDto>();
    }
}
