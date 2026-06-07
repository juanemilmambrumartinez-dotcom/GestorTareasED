using Microsoft.EntityFrameworkCore;
using GestorTareasED.Api.Models.Entities;

namespace GestorTareasED.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(p => p.Description)
                      .HasMaxLength(500);
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(t => t.Description)
                      .HasMaxLength(1000);
                entity.HasOne(t => t.Project)
                      .WithMany()
                      .HasForeignKey(t => t.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}