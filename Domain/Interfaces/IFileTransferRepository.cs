using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{

    public interface IFileTransferRepository
    {
        public void AddEmail(Email b);
        public IQueryable<Email> GetFiles(string user);


    }
}
