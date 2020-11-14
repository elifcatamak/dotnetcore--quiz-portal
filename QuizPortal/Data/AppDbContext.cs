using Microsoft.EntityFrameworkCore;
using QuizPortal.Models;

namespace QuizPortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasOne(e => e.Quiz)
                                            .WithMany()
                                            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}