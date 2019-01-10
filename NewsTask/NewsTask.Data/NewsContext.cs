using Microsoft.EntityFrameworkCore;
using NewsTask.Data.Models;

namespace NewsTask.Data
{
    public class NewsContext : DbContext
    {
        internal DbSet<Collection> Collections { get; set; }

        internal DbSet<Source> Sources { get; set; }

        internal DbSet<SourceCollection> SourceCollections { get; set; }

        internal DbSet<User> Users { get; set; }


        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Source>()
                .HasIndex(e => e.Url)
                .IsUnique();

            modelBuilder.Entity<Collection>()
                .HasIndex(e => e.Title)
                .IsUnique();


            modelBuilder.Entity<SourceCollection>()
                .HasKey(e => new { e.SourceId, e.CollectionId });

            modelBuilder.Entity<SourceCollection>()
                .HasOne(e => e.Source)
                .WithMany(e => e.SourceCollections)
                .HasForeignKey(e => e.SourceId);

            modelBuilder.Entity<SourceCollection>()
                .HasOne(e => e.Collection)
                .WithMany(e => e.SourceCollections)
                .HasForeignKey(e => e.CollectionId);


            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
