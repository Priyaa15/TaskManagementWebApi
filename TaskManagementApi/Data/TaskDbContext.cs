// Data/TaskDbContext.cs
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    //TaskDBCOntextr class is a custom implementation of the DbContext class from Entity Framework Core. It serves as the bridge between your application and the database, allowing you to query and save data. 
    public class TaskDbContext : DbContext
    {
        //The constructor accepts DbContextOptions<TaskDbContext> as a parameter, which is used to configure the database connection (e.g., connection string, provider).
        //  It passes these options to the base DbContext class.
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
//DB connection configured in the program class 
//Tasks property maps to DB tabloe
//Method to see initial data
//configure DB connectivity in the dependency injection