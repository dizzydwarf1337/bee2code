using Domain.Models.LabTesting;
using Domain.Models.Links;
using Domain.Models.Researches;
using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database
{
    public class BeeCodeDbContext : IdentityDbContext<User, IdentityRole<Guid>,Guid>
    {
        public BeeCodeDbContext(DbContextOptions<BeeCodeDbContext> options) : base(options)
        {

        }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserResearch> UserResearches { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<LabTestResult> LabTestResults { get; set; }
        public DbSet<Research> Researches { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LabTest>()
                .HasOne(l => l.Creator)
                .WithMany(x=>x.CreatedLabTests)
                .HasForeignKey(l => l.CreatorId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<LabTest>()
                .HasOne(l => l.Patient)
                .WithMany(x=>x.LabTests) 
                .HasForeignKey(l => l.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
