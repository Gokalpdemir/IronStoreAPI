﻿using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Repositories
{
    public class FileWriteRepository:WriteRepository<Domain.Entities.File>,IFileWriteRepository
    {
        
        public FileWriteRepository(ETicaretAPIDbContext context):base(context)
        {
            
        }
    }
}
