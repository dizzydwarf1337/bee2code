using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class UserNotification
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        public string Message { get; set; }
        
        [Required]
        public string Title { get; set; }

        public bool IsRead { get; set; } = false;
    }
}
