// DTOs/TaskDto.cs
using System;
using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models;

namespace TaskManagementApi.DTOs
{
    public class TaskCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    }

    public class TaskUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public TaskPriority Priority { get; set; }
    }

    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public TaskPriority Priority { get; set; }
        public string PriorityString => Priority.ToString();
    }
}