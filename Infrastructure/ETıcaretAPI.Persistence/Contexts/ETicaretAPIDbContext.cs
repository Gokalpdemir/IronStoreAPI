using ETıcaretAPI.Domain.Entities;
using ETıcaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext : DbContext
    {
        public ETicaretAPIDbContext(DbContextOptions<ETicaretAPIDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
          var datas=  ChangeTracker
                .Entries<BaseEntity>();
            foreach (var data in datas)
            {
                if (data.State==EntityState.Added)
                {
                    data.Entity.CreatedDate = DateTime.Now;
                }
                if (data.State==EntityState.Modified)
                    data.Entity.UpdatedDate = DateTime.Now;

            }      

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
 