
// Controllers/TasksController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
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
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        //Action result - wraps the response to return different error codes
        //representss collection of task objects from model task
        {
            return await _context.Tasks.ToListAsync();
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

        //Get :api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        //Post :api/tasks
        [HttpPost]
        public async Task<ActionResult<Models.Task>> CreateTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.Id },
                task);
        }

        //Put: api/tasks/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            _context.Entry(task).State = EntityState.Modified;

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
