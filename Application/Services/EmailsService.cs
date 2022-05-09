using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class EmailsService : IEmailsService
    {
        private IFileTransferRepository emailsRepo;
        
        public EmailsService(IFileTransferRepository _emailsRepo) 
        {
            emailsRepo = _emailsRepo;
        }

        public void AddEmail(AddEmailViewModel model)
        {
            emailsRepo.AddEmail(new Domain.Models.Email()
            {             
                ToEmail = model.ToEmail,
                FromEmail = model.FromEmail,
                Title = model.Title,
                Message = model.Message,
                Password = model.Password,
                FilePath = model.FilePath
            });
        }

        public IQueryable<EmailViewModel> GetFiles(string user)
        {
            var list = from e in emailsRepo.GetFiles(user)
                       orderby e.Id ascending
                       select new EmailViewModel()
                       {
                           Id = e.Id,
                           FromEmail = e.FromEmail,
                           ToEmail = e.ToEmail,
                           Title = e.Title,
                           Message = e.Message,
                           Password = e.Password,
                           FilePath = e.FilePath
                       };
            return list;
        }

    }
}
