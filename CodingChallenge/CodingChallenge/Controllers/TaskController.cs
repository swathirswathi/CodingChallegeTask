using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class TaskController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITaskServices _taskService;
        private readonly ILogger<TaskController> _logger; // Add logger

        public TaskController(ITaskServices taskService, IUserService userService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Taskk>>> GetAllTasksAsync()
        {
            try
            {
                var members = await _taskService.GetAllTasksAsync();
                return Ok(members);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all tasks");
                return StatusCode(500, "Internal server error occurred");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Taskk>> GetTaskByIdAsync(int id)
        {
            try
            {
                var member = await _taskService.GetTaskByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching task with ID {id}");
                return StatusCode(500, $"Internal server error occurred");
            }
        }

        [HttpPost]
        [Route("AddTask")]
        public async Task<ActionResult<Taskk>> AddTask(Taskk task)
        {
            try
            {

                Taskk taskk = new Taskk();
                taskk.Title = task.Title;
                taskk.Description = task.Description;
                taskk.DueDate = task.DueDate;
                taskk.Status = task.Status;
                taskk.CompletedDate = task.CompletedDate;
                taskk.UserId = task.UserId;

                return await _taskService.AddTask(task);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return NotFound(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Taskk>> UpdateTaskAsync(int id, Taskk updatedTask)
        {
            try
            {
                var member = await _taskService.UpdateTaskAsync(id, updatedTask);
                return Ok(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating task with ID {id}");
                return StatusCode(500, $"Internal server error occurred");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaskAsync(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting task with ID {id}");
                return StatusCode(500, $"Internal server error occurred");
            }
        }

    }
}
