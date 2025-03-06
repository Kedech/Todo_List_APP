using Microsoft.EntityFrameworkCore;
using Todo_List_API.Contexts;
using Todo_List_API.DTOs;
using Todo_List_API.Entities;
using Todo_List_API.Repositories.Interfaces;

namespace Todo_List_API.Repositories
{
    public class TodoRepository(DataContextTodo context) : ITodoRepository
    {
        private readonly DataContextTodo _context = context;

        public async Task<int> CountTodosByUser(int userId)
        {
            return await _context.Todo.CountAsync(t => t.UserId == userId);
        }

        public async Task<TodoDTO> CreateAsync(CreateUpdateTodoDTO todoDto)
        {
            var todo = new Todo
            {
                UserId = todoDto.UserId,
                Description = todoDto.Description,
                Title = todoDto.Title
            };
            _context.Todo?.Add(todo);
            var id = await _context.SaveChangesAsync();
            return new TodoDTO
            {
                Id = id,
                Description = todoDto.Description,
                Title = todoDto.Title
            };
        }

        public async Task<bool> DeleteTodo(int id, int userid)
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo != null && todo.UserId == userid)
            {
                _context.Todo.Remove(todo);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<TodoDTO> EditTodo(CreateUpdateTodoDTO todoDto)
        {
            var todobd = await _context.Todo.FindAsync(todoDto.Id);
            if(todobd == null || todobd.UserId != todoDto.UserId)
                return new TodoDTO();
            todobd.Title = todoDto.Title;
            todobd.Description = todoDto.Description;
            await _context.SaveChangesAsync();

            return new TodoDTO
            {
                UserId = todoDto.UserId,
                Description = todoDto.Description,
                Title = todoDto.Title,
                Id = todoDto.Id
            };
        }

        public async Task<List<TodoDTO>> GetAllAsync(int userId, int page, int limit)
        {
            var todoList =  await (_context.Todo?.Where(t => t.UserId == userId)
                .Skip((page - 1) * limit)
                .Take(limit).ToListAsync() ?? Task.FromResult(new List<Todo>()));
            List<TodoDTO> result = [];
            foreach (var todo in todoList) {
                var dto = new TodoDTO
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description
                };
                result.Add(dto);
            }
            return result;
        }
    }
}
