using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
   public class EmailViewModel
    {
        public int Id { get; set; }
        public string ToEmail { get; set; }
        public string FromEmail { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        [MaxLength(100)]
        public string FileType { get; set; }
        [MaxLength]
        public byte[] DataFiles { get; set; }
        public long fileLength { get; set; }
        public DateTime FileExpiry { get; set; }
    }
}
