using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using XamarinBlogEducation.DataAccess.Entities;

namespace XamarinBlogEducation.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
         
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
           .HasOne(b => b.ApplicationUser)
           .WithMany()
           .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Post>()
            .HasOne(b => b.Category)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
          //  DbSeeder.SeedDb(this);
            base.OnModelCreating(modelBuilder);
        }
        
    }
}