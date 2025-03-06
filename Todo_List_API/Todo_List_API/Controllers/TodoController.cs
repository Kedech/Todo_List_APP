using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Todo_List_API.DTOs;
using Todo_List_API.Services.Interfaces;

namespace Todo_List_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly static string nameAssembly = Assembly.GetExecutingAssembly().GetName().Name!;
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoService _todoService;

        public TodoController(ILogger<TodoController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateTodoAsync([FromBody] CreateUpdateTodoDTO todoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            todoDTO.UserId = userId;
            var todo = await _todoService.CreateTodo(todoDTO);
            return Ok(todo);
        }

        [HttpGet("listAll")]
        public async Task<IActionResult> ListAllTodoByUser([FromQuery] int page, [FromQuery] int limit)
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            var todo = await _todoService.ListTodo(userId, page, limit);
            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] int id, [FromBody] CreateUpdateTodoDTO todoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userId = int.Parse(User.FindFirst("UserId")!.Value);
                todoDTO.UserId = userId;
                todoDTO.Id = id;
                var todo = await _todoService.EditTodo(todoDTO);
                if (todo.Id == 0) return StatusCode(403, new { message = "Forbidden" });
                return Ok(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameAssembly} - {MethodBase.GetCurrentMethod()} - {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while deleting the item" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")!.Value);
                var result = await _todoService.DeleteTodo(id, userId);
                if (!result)
                    return NotFound(new { message = "Item not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameAssembly} - {MethodBase.GetCurrentMethod()} - {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while deleting the item" });
            }
        }
    }
}
