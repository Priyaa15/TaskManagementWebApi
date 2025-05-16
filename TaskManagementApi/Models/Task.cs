using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class Task 
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public TaskPriority Priority { get; set; }

    }

    public enum TaskPriority 
    {
        Low,
        Medium,
        High,
        Urgent

    }

}