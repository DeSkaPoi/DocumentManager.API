using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Domain;

namespace DocumentManager.Infrastructure
{
    public class DocManagerContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<FileLink> Files { get; set; }
        public DbSet<PictureLink> Pictures { get; set; }
        public DbSet<VideoLink> Videos { get; set; }
        
        public DocManagerContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DocManagerDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(document =>
            {
                document.HasKey(prop => prop.Id);

                document.HasMany(PropNavNav => PropNavNav.Files).WithOne(PropNav => PropNav.Document)
                    .OnDelete(DeleteBehavior.Cascade).IsRequired();

                document.HasMany(PropNavNav => PropNavNav.Pictures).WithOne(PropNav => PropNav.Document)
                    .OnDelete(DeleteBehavior.Cascade).IsRequired();

                document.HasMany(PropNavNav => PropNavNav.Videos).WithOne(PropNav => PropNav.Document)
                    .OnDelete(DeleteBehavior.Cascade).IsRequired();

                document.Property(prop => prop.Title).HasDefaultValue("not indicated");
                document.Property(prop => prop.Content).HasDefaultValue("not indicated");
                document.Property(prop => prop.Description).HasDefaultValue("not indicated");
            });

            modelBuilder.Entity<FileLink>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(PropNav => PropNav.Document).WithMany(PropNav => PropNav.Files).HasForeignKey("DocumentId");
            });

            modelBuilder.Entity<PictureLink>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(PropNav => PropNav.Document).WithMany(PropNav => PropNav.Pictures).HasForeignKey("DocumentId");
            });

            modelBuilder.Entity<VideoLink>(document =>
            {
                document.HasKey(prop => prop.Id);
                document.Property<Guid>("DocumentId");
                document.HasOne(PropNav => PropNav.Document).WithMany(PropNav => PropNav.Videos).HasForeignKey("DocumentId");
                
            });
        }
    }
}
