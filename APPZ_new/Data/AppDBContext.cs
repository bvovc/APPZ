﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using APPZ_new.Models;
using Task = System.Threading.Tasks.Task;

namespace APPZ_new.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Answer>()
                .HasOne(e => e.Question)
                .WithMany(i => i.Answers)
                .HasForeignKey(e => e.QuestionId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Models.Task>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Tasks)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<UserTask>()
                .HasOne(e => e.User)
                .WithMany(e => e.Tasks)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserTask>()
                .HasOne(e => e.Task)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Title = "None", Description = "None" });
        }

    }
}
