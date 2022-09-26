using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aranda.Productos.Persistence.Entities;

namespace Aranda.Productos.Persistence.Data{
    public class CatalagoArandaDBContext : DbContext
    {
        public CatalagoArandaDBContext(DbContextOptions<CatalagoArandaDBContext> options) : base(options)
        {
        }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
