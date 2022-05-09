using Domain.Models;
using System;
using System.Collections.Generic;
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
        public string Password { get; set; }
        public string FilePath { get; set; }
    }
}
