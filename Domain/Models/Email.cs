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
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        public string FileType { get; set; }
        [MaxLength]
        public byte[] DataFiles { get; set; }
        public long fileLength { get; set; }

        [DataType(DataType.DateTime)]
        [ValidateDate]
        public DateTime FileExpiry { get; set; }

    }
}
