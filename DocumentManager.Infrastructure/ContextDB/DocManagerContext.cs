using DocumentManager.Infrastructure.ModelDB;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocumentManager.Infrastructure.ContextDB
{
    public class DocManagerContext : DbContext
    {
        public DbSet<DocumentDataBase> Documents { get; set; }

        public DocManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentDataBase>(document =>
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

            modelBuilder.Entity<FileLinkDataBase>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(propNav => propNav.Document).WithMany(propNavigate => propNavigate.Files).HasForeignKey("DocumentId");
            });

            modelBuilder.Entity<PictureLinkDataBase>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(propNav => propNav.Document).WithMany(propNavigate => propNavigate.Pictures).HasForeignKey("DocumentId");
            });

            modelBuilder.Entity<VideoLinkDataBase>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(propNav => propNav.Document).WithMany(propNavigate => propNavigate.Videos).HasForeignKey("DocumentId");
            });
        }
    }
}
