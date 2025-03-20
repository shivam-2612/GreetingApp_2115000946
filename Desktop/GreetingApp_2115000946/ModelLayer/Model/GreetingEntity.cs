using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Model
{
    public class GreetingEntity
    {
        [Key]
        public int GreetingId { get; set; } // Primary Key

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key Reference to User
        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign Key

        public virtual UserEntity User { get; set; } // Navigation Property
    }
}
