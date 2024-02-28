using Microsoft.EntityFrameworkCore;
using TestTask.Data.Mapping;

namespace TestTask.Data
{
    public class TestTaskApplcationContext : DbContext
    {
        public TestTaskApplcationContext(DbContextOptions<TestTaskApplcationContext> options) : base(options) { }

        public TestTaskApplcationContext(string connectionString)
        {
            Connection = connectionString;
            Database.EnsureCreated();
        }

        private string Connection { get; set; }

        public DbSet<Entity.File> Files { get; set; }
        public DbSet<Entity.FileShare> FileShares { get; set; }
        public DbSet<Entity.User> Users { get; set; }
        public DbSet<Entity.UserSession> UserSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(Connection);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddMappers();
            base.OnModelCreating(modelBuilder);
        }
    }
}
