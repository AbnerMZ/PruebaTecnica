using Microsoft.EntityFrameworkCore;
using Capa_Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Data
{
    public class PruebaTecnicaDbContext: DbContext
    {
        public PruebaTecnicaDbContext(DbContextOptions options) : base(options) 
        { 
        
        }

        public virtual DbSet<Vehiculo> Vehiculo { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



    }
}
