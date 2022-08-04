using Backend.EF.Sqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.EF.Sqlite.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(e => e.Name).HasMaxLength(200);

            modelBuilder.Entity<Company>().Property(c => c.Name).HasMaxLength(200);
            modelBuilder.Entity<Company>().Property(c => c.ZipCode).HasMaxLength(8);
            modelBuilder.Entity<Company>().Property(c => c.Phone).HasMaxLength(11);
        }

    }
}
