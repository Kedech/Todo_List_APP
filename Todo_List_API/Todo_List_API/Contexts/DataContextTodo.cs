using Microsoft.EntityFrameworkCore;
using Todo_List_API.Entities;

namespace Todo_List_API.Contexts
{
    public class DataContextTodo(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Todo>? Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);
        }
    }
}
