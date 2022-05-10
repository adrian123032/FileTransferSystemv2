using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class AddEmailViewModel
    {
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
        [DataType(DataType.DateTime)]
        [ValidateDate]
        public DateTime FileExpiry { get; set; }


    }
}
