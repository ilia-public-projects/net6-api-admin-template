using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.EntityFramework.Identity;

namespace WebApplication1.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<IdentityGroup> IdentityGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // set globally decimal precision to 18,2
            IEnumerable<IMutableProperty> decimalProperties = modelBuilder.Model.GetEntityTypes()
                                                        .SelectMany(t => t.GetProperties())
                                                        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));
            foreach (IMutableProperty property in decimalProperties)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            // set globally cascade delete to false
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (IMutableForeignKey fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

    }
}
