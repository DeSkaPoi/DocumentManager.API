using DocumentManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocumentManager.Infrastructure
{
    public class DocManagerContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        public DocManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(document =>
            {
                document.HasKey(prop => prop.Id);

                document.HasMany(propNav => propNav.Files).WithOne(propNavigate => propNavigate.Document)
                    .OnDelete(DeleteBehavior.Cascade).IsRequired();

                document.HasMany(propNav => propNav.Pictures).WithOne(propNavigate => propNavigate.Document)
                    .OnDelete(DeleteBehavior.Cascade).IsRequired();

                document.HasMany(propNav => propNav.Videos).WithOne(propNavigate => propNavigate.Document)
                    .OnDelete(DeleteBehavior.Cascade).IsRequired();

                document.Property(prop => prop.Title).HasDefaultValue("not indicated");
                document.Property(prop => prop.Content).HasDefaultValue("not indicated");
                document.Property(prop => prop.Description).HasDefaultValue("not indicated");
            });

            modelBuilder.Entity<FileLink>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(propNav => propNav.Document).WithMany(propNavigate => propNavigate.Files).HasForeignKey("DocumentId");
            });

            modelBuilder.Entity<PictureLink>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(propNav => propNav.Document).WithMany(propNavigate => propNavigate.Pictures).HasForeignKey("DocumentId");
            });

            modelBuilder.Entity<VideoLink>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(propNav => propNav.Document).WithMany(propNavigate => propNavigate.Videos).HasForeignKey("DocumentId");
            });
        }
    }
}
