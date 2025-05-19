
// Controllers/TasksController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{

    [ApiController] //Marks the class as a Web API controller.
    // //automatic model validation from body,route,qs//automatically determines where to bind parameters
    [Route("api/[controller]")] // defines route templates for the controller endpoints
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;
        public TasksController(TaskDbContext context)
        {
            _context = context;

        }

        //Get : api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetTasks()
        //Action result - wraps the response to return different error codes
        //representss collection of task objects from model task
        {
            //return await _context.Tasks.ToListAsync();
            
            return await _context.Tasks
                .Select(t => new TaskReadDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    Priority = t.Priority
                })
                .ToListAsync();
        
            
            //_context.Task - refers to table in the db created in taskcontextdb class
            //ToListAsync(): 
            // Converts the query result into a list asynchronously.
            // Executes the query against the database and retrieves all rows from the Tasks table.

            //           How It Works
            // A client sends a GET request to api/tasks.
            // The GetTasks method is invoked.
            // The method queries the database for all tasks using Entity Framework Core.
            // The result (a list of tasks) is returned to the client as a 200 OK response.


        }

       // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadDto>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return new TaskReadDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                Priority = task.Priority
            };
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> CreateTask(TaskCreateDto taskDto)
        {
            var task = new Models.Task
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                IsCompleted = false, // New tasks are not completed by default
                Priority = taskDto.Priority
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var readDto = new TaskReadDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                Priority = task.Priority
            };

            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.Id },
                readDto);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskUpdateDto taskDto)
        {
            var task = await _context.Tasks.FindAsync(id);
            
            if (task == null)
            {
                return NotFound();
            }

            // Update the task properties
            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.DueDate = taskDto.DueDate?? task.DueDate;
            task.IsCompleted = taskDto.IsCompleted;
            task.Priority = taskDto.Priority;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}