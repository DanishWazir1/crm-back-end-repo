using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using CRM.CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.CORE
{
    public class CRMDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=DESKTOP-1KGQUT9;Initial Catalog=CRM;Persist Security Info=True;User ID=sa;Password=abc123;MultipleActiveResultSets=true;TrustServerCertificate=True;");
                
                base.OnConfiguring(optionsBuilder);
               
            }
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            return 0;
        }

    }
}
