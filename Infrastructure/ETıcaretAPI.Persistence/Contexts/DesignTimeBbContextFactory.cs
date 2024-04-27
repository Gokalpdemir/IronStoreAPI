using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Contexts
{
    public class DesignTimeBbContextFactory : IDesignTimeDbContextFactory<ETicaretAPIDbContext>
    {
        private readonly IConfiguration _configuration;

        public DesignTimeBbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public ETicaretAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ETicaretAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ETicaretAPIDb;Trusted_Connection=true");
            return new ETicaretAPIDbContext(dbContextOptionsBuilder.Options,_configuration);
        }
    }
}
