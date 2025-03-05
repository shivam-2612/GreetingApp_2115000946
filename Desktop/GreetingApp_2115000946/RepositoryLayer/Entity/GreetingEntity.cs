using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Model
{
    [Table("Greetings")]
    public class GreetingEntity
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Message { get; set; }  // Greeting message

        public DateTime CreatedAt { get; set; } = DateTime.Now;  // Timestamp
    }
}


