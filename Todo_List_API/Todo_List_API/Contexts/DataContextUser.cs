using Microsoft.EntityFrameworkCore;
using Todo_List_API.Entities;

namespace Todo_List_API.Contexts
{
    public class DataContextUser(DbContextOptions<DataContextUser> options) : DbContext(options)
    {
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
            .Property(u => u.CreatedDate)
            .HasColumnType("timestamp without time zone");
            modelBuilder.Entity<User>()
               .HasMany(u => u.Todos)
               .WithOne(t => t.User)
               .HasForeignKey(t => t.UserId);
        }
    }
}
