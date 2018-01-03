using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core;

namespace Store.Data
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext() : base("DbConnection")
        {
            Database.SetInitializer<StoreDbContext>(null);
        }

        #region Entity Sets
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<CPUSocket> CPUSockets { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<PowerSupply> Powersuppliers { get; set; }
        public DbSet<PC> PCs { get; set; }
        public DbSet<PCMemory> PCMemories { get; set; }

        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        
    }
}
