﻿using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class FileTransferRepository : IFileTransferRepository
    {
        private FileTransferContext context;
        public FileTransferRepository(FileTransferContext _context)
        {
            context = _context;
        }

        public void AddEmail(Email e)
        {
            context.Emails.Add(e);
            context.SaveChanges();
        }

        public IQueryable<Email> GetFiles(string user)
        {
            var list = from e in context.Emails
                       where e.ToEmail==user || e.FromEmail == user
                       select e;
            return list;
        }


    }
}