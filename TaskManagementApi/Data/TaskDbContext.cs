// Data/TaskDbContext.cs
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed some initial data
            modelBuilder.Entity<Models.Task>().HasData(
                new Models.Task 
                { 
                    Id = 1, 
                    Title = "Complete API Project", 
                    Description = "Finish the task management API implementation", 
                    DueDate = DateTime.Now.AddDays(7), 
                    IsCompleted = false,
                    Priority = TaskPriority.High
                },
                new Models.Task 
                { 
                    Id = 2, 
                    Title = "Write Tests", 
                    Description = "Create unit tests for the API endpoints", 
                    DueDate = DateTime.Now.AddDays(14), 
                    IsCompleted = false,
                    Priority = TaskPriority.Medium
                }
            );
        }
    }
}