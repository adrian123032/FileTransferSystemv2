using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    public interface IEmailsService
    {
        public void AddEmail(AddEmailViewModel model);
        public IQueryable<EmailViewModel> GetFiles(string user);
        public IQueryable<EmailViewModel> GetFileByName(string fileName);


    }
}
