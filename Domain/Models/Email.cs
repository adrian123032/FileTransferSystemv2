using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ToEmail { get; set; }
        [Required]
        public string FromEmail { get; set; }
        [Required]
        public string Title { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
        [Required]
        public string FilePath { get; set; }
        public DateTime FileExpiry { get; set; }
    }
}
