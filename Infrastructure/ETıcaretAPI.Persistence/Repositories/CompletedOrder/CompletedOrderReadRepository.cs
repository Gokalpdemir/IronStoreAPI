using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using ETıcaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Repositories
{
    public class CompletedOrderReadRepository : ReadRepository<CompletedOrder>, ICompletedOrderReadRepository
    {
        public CompletedOrderReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
