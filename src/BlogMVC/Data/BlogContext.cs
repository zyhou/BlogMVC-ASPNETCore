using BlogMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC.Data
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Slug).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Created).ValueGeneratedOnAdd();

                entity.Property(e => e.IdUser).HasColumnName("user_id");
                entity.Property(e => e.IdCategorie).HasColumnName("category_id");

                entity.HasOne(e => e.User)
                      .WithMany(p => p.Posts)
                      .HasForeignKey(e => e.IdUser)
                      .IsRequired();

                entity.HasOne(e => e.Category)
                      .WithMany(p => p.Posts)
                      .HasForeignKey(e => e.IdCategorie)
                      .IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Mail).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Created).ValueGeneratedOnAdd();

                entity.Property(e => e.IdPost).HasColumnName("post_id");

                entity.HasOne(e => e.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(e => e.IdPost)
                      .IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Slug).IsRequired().HasMaxLength(50);

                entity.Property(e => e.PostCount).IsRequired().HasColumnName("post_count");
            });

        }
    }
}
