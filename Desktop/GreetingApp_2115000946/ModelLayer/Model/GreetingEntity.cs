using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Model
{
    public class GreetingEntity
    {
        [Key]
        public int GreetingId { get; set; }  // Primary Key

        [ForeignKey("User")]
        public int UserId { get; set; }  // Foreign Key

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual UserEntity User { get; set; }
    }
}
