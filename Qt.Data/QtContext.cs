using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Qt.Domain.Entity;

namespace Qt.Data
{
    public class QtContext : DbContext
    {
        public DbSet<Query> Queries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<QueryGroup> Groups { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Query>().HasRequired(g => g.Owner);
            modelBuilder.Entity<Query>().Property(g => g.Name).IsRequired();
            modelBuilder.Entity<Query>().Property(g => g.Text).IsRequired();

            modelBuilder.Entity<Query>().Property(o => o.CreatedDateTime).HasColumnType("datetime2");
            modelBuilder.Entity<User>().Property(o => o.CreatedDateTime).HasColumnType("datetime2");
            modelBuilder.Entity<QueryGroup>().Property(o => o.CreatedDateTime).HasColumnType("datetime2");

            base.OnModelCreating(modelBuilder);
        }
    }
}
