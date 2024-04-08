﻿using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Repositories
{
    public class FileReadRepository:ReadRepository<Domain.Entities.File>,IFileReadRepository
    {
        public FileReadRepository(ETicaretAPIDbContext context):base(context) 
        {
            
        }
    }
}
