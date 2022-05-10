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
                Name = model.Name,
                FileType = model.FileType,
                fileLength = model.fileLength,
                DataFiles = model.DataFiles,
                FileExpiry = model.FileExpiry
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
                           Name = e.Name,
                           FileType = e.FileType,
                           fileLength = e.fileLength,
                           DataFiles = e.DataFiles,
                           FileExpiry = e.FileExpiry
                       };
            return list;
        }

    }
}
