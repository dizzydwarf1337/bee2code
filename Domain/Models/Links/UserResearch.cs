using Domain.Models.Researches;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Links
{
    [PrimaryKey("UserId","ResearchId")]
    public class UserResearch
    {
        [Key, Column(Order = 0)]    
        public Guid UserId {  get; set; }
        [Key,Column(Order = 1)]
        public Guid ResearchId { get; set; }
        public string? AccteptationFilePath { get; set; }
        public virtual User? User { get; set; }
        public virtual Research? Research { get; set; }
    }
}
