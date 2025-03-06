using Todo_List_API.DTOs;
using Todo_List_API.Entities;
using Todo_List_API.Repositories.Interfaces;
using Todo_List_API.Services.Interfaces;

namespace Todo_List_API.Services
{
    public class TodoService(ITodoRepository todoRepository) : ITodoService
    {
        private readonly ITodoRepository _todoRepository = todoRepository;

        public async Task<TodoDTO> CreateTodo(CreateUpdateTodoDTO todoDTO)
        {
            return await _todoRepository.CreateAsync(todoDTO);
        }

        public async Task<bool> DeleteTodo(int id, int userid)
        {
            return await _todoRepository.DeleteTodo(id, userid);
        }

        public async Task<TodoDTO> EditTodo(CreateUpdateTodoDTO todoDTO)
        {
            return await _todoRepository.EditTodo(todoDTO);
        }

        public async Task<ListTodoDTO<TodoDTO>> ListTodo(int userId, int page, int limit)
        {
            var totalItems = await _todoRepository.CountTodosByUser(userId);
            var items = await _todoRepository.GetAllAsync(userId, page, limit);
            var totalPages = (int)Math.Ceiling(totalItems / (double)limit);
            
            return new ListTodoDTO<TodoDTO>
            {
                Data = items,
                Limit = limit,
                Page = page,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
    }
}
